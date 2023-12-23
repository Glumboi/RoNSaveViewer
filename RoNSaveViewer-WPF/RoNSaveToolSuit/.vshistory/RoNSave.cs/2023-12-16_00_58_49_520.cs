using System.IO;
using UeSaveGame;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public class RoNSave : SaveGame
    {
        public RoNSave() : base()
        {
        }
    }

    internal class SaveGameExt
    {
        public static RoNSave ToRoNSave(this SaveGame save)
        {
            return new RoNSave();
        }
    }
}