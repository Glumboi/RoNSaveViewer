using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UeSaveGame;

namespace RoNSaveViewer_WPF.CustomObjects
{
    internal class RoNSaveObject
    {
        public UProperty OBJUProperty { get; set; }
        public string Name { get; set; }

        public RoNSaveObject(string name, UProperty uProperty)
        {
            Name = name;
            OBJUProperty = uProperty;
        }
    }
}