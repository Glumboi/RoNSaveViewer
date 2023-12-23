using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using UeSaveGame;
using UeSaveGame.Util;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public class RoNSave : SaveGame
    {
        public RoNSave() : base()
        {
        }

        public static RoNSave LoadFrom(Stream stream)
        {
            RoNSave instance = new RoNSave();

            byte[] id = new byte[sHeader.Length];
            stream.Read(id, 0, sHeader.Length);

            using (var reader = new BinaryReader(stream, Encoding.ASCII, true))
            {
                instance.Header = SaveGameHeader.Deserialize(reader);
                if (instance.Header.SaveGameVersion != 2) throw new NotSupportedException($"Save game version {instance.Header.SaveGameVersion} cannot be read. Only version 2 is supported at this time");

                instance.CustomFormats = CustomFormatData.Deserialize(reader);

                instance.SaveClass = reader.ReadUnrealString();

                instance.Properties = new List<UProperty>(PropertySerializationHelper.ReadProperties(reader, true));

                if (reader.BaseStream.Position != reader.BaseStream.Length) throw new FormatException("Did not reach the end of the file when reading.");
            }

            return instance;
        }
    }
}