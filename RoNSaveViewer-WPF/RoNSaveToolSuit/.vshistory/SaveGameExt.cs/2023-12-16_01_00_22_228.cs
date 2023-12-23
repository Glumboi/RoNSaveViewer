using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UeSaveGame;

namespace RoNSaveViewer_WPF.RoNSaveToolSuit
{
    public static class SaveGameExt
    {
        public static RoNSave ToRoNSave(this SaveGame save)
        {
            return new RoNSave(save);
        }
    }
}