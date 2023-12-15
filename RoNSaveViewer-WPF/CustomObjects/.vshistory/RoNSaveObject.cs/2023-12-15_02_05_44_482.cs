using System;
using System.Collections;
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

                    var array = uProperty.Value as UProperty[];
                    foreach (var item in array)
                    {
                        base.AddChild(item);
                    }
                    break;

                case "StructProperty":

                    var val = uProperty.Value;
                    var props = val.GetType().GetProperties();

                    foreach (var item in props)
                    {
                        if (item.Name == "Properties")
                        {
                            IEnumerable itemEnum = item.GetValue(val) as IEnumerable;
                            foreach (UProperty item2 in itemEnum)
                            {
                                base.AddChild(item2);
                            }
                        }
                    }
                    listBoxValuesOfUProperty.Items.Add(node.UPropertyOfNode.Value);
                    break;
            }
        }
    }
}