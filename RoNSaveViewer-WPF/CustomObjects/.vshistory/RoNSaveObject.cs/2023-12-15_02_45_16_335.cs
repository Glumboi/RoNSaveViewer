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
    internal class RoNSaveObjectBase
    {
        public UProperty OBJUProperty { get; set; }
        public string OBJName { get; set; }
    }

    internal class RoNSaveObject : TreeViewItem, RoNSaveObjectBase
    {
        

        public RoNSaveObject(UProperty uProperty) : base()
        {
            base.Header = uProperty.Name;
            OBJName = uProperty.Name;
            OBJUProperty = uProperty;

            switch (uProperty.Type)
            {
                case "ArrayProperty":

                    var array = uProperty.Value as UProperty[];
                    foreach (var item in array)
                    {
                        base.AddChild(new RoNSaveObject(item));
                    }
                    break;
            }
        }
    }

    internal class EditableRoNSaveObject : ListBoxItem
    {
        public UProperty OBJUProperty { get; set; }
        public string OBJName { get; set; }
        public string Value { get; set; }

        public EditableRoNSaveObject(UProperty uProperty) : base()
        {
            base.Content = uProperty.Value;
            OBJName = uProperty.Name;
            Value = uProperty.Value?.ToString() ?? "NULL";
            OBJUProperty = uProperty;
        }
    }
}