using System.IO;
using UeSaveGame;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public class RoNSave : SaveGame
    {
        public RoNSave(string path) : base()
        {
            using (FileStream fs = File.OpenRead(path))
            {
                return LoadFrom(fs);
            }
        }
    }
}