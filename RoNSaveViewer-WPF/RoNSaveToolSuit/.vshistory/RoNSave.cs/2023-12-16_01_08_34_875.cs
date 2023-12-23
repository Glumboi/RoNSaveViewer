using RoNSaveViewer_WPF.CustomObjects;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using UeSaveGame;
using UeSaveGame.PropertyTypes;
using UeSaveGame.Util;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public class RoNSave
    {
        public SaveGame GameSave { get; private set; }

        private List<StructProperty> parentUProps;

        public RoNSave(SaveGame gameSave)
        {
            GameSave = gameSave;

            foreach (var item in gameSave.Properties)
            {
                parentUProps?.Add(item);
            }
        }
    }
}