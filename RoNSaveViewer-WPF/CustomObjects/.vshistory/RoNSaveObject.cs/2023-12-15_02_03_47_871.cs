using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using UeSaveGame;

namespace RoNSaveViewer_WPF.CustomObjects
{
    internal class RoNSaveObject : TreeViewItem
    {
        public UProperty OBJUProperty { get; set; }
        public string Name { get; set; }

        public RoNSaveObject(string name, UProperty uProperty) : base()
        {
            base.Header = name;
            Name = name;
            OBJUProperty = uProperty;

            switch (uProperty.Type)
            {
                case "ArrayProperty":

                    var array = uProperty.UPropertyOfNode.Value as UProperty[];

                    listBoxValuesOfUProperty.Items.AddRange(array);
                    return;

                    break;
            }
        }
    }
}