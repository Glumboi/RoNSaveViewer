using RoNSaveViewer_WPF.CustomObjects;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using UeSaveGame;
using UeSaveGame.Util;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public class RoNSave
    {
        public SaveGame GameSave { get; private set; }

        private List<object> allProperties;

        public RoNSave(SaveGame gameSave)
        {
            GameSave = gameSave;

            foreach (UProperty item in GameSave.Properties)
            {
                allProperties.Add(item);
            }
        }
    }
}