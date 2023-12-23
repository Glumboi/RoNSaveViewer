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
        public string OBJName { get; set; }

        public RoNSaveObject(UProperty uProperty) : base()
        {
            base.Header = uProperty.Name == string.Empty ? uProperty.Value : uProperty.Name;
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
                    var cm = base.ContextMenu = new ContextMenu()
                    {

                    };
                    var addHeader = new MenuItem().Header = "Add Item";
                    cm.Items.Add(addHeader);
                    base.MouseRightButtonDown += (o, e) => 
                    { 
                        cm.Visibility = System.Windows.Visibility.Visible;
                    };
                    break;
            }
        }

        public bool Equals(RoNSaveObject other)
        {
            throw new NotImplementedException();
        }
    }

    internal class EditableRoNSaveObject : ListBoxItem
    {
        public UProperty OBJUProperty { get; set; }
        public string OBJName { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public EditableRoNSaveObject(UProperty uProperty) : base()
        {
            base.Content = uProperty.Value;
            OBJName = uProperty.Name;
            Value = uProperty.Value?.ToString() ?? "[READ ERROR]";
            Type = uProperty.Value?.GetType().ToString() ?? "[READ ERROR]";
            OBJUProperty = uProperty;
        }
    }
}