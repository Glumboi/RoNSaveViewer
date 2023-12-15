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
                        base.AddChild(new RoNSaveObject(item.Name, item));
                    }
                    break;
            }
        }
    }

    internal class EditableRoNSaveObject : ListBoxItem
    {
        public UProperty OBJUProperty { get; set; }
        public string Name { get; set; }

        public EditableRoNSaveObject(string name, UProperty uProperty) : base()
        {
            base.Content = name;
            Name = name;
            OBJUProperty = uProperty;

            switch (uProperty.Type)
            {
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
                    base.AddChild(uProperty.Value);
                    break;
            }
        }
    }
}