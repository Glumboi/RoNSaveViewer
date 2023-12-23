using System.IO;
using UeSaveGame;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public class RoNSave : SaveGame
    {
        public RoNSave() : base()
        {
        }

        public static RoNSave LoadFrom(Stream str)
        {
            return base.LoadFrom(str) as RoNSave;
        }
    }
}