using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using UeSaveGame;
using RoNSaveViewer_WPF.CustomControls;
using Wpf.Ui.Controls;
using System.Xml;
using System.Drawing;
using System.Windows.Media;

namespace RoNSaveViewer_WPF.CustomObjects
{
    public class RoNSaveObject : Wpf.Ui.Controls.TreeViewItem
    {
        public UProperty OBJUProperty { get; set; }
        public string OBJName { get; set; }

            public RoNSaveObject(UProperty uProperty, bool ignoreType = false) : base()
        {
            base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            base.Header = $"{(uProperty.Name == string.Empty ? uProperty.Value : uProperty.Name)} : {uProperty.Type}";
            OBJName = uProperty.Name;
            OBJUProperty = uProperty;

            if (ignoreType) return;

            switch (uProperty.Type)
            {
                case "ArrayProperty":

                    var array = uProperty.Value as UProperty[];
                    foreach (var item in array)
                    {
                        base.AddChild(new RoNSaveObject(item));
                    }
                    break;

                case "SetProperty":

                    var array2 = uProperty.Value as UProperty[];
                    foreach (var item in array2)
                    {
                        base.AddChild(new RoNSaveObject(item));
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
                                base.AddChild(new RoNSaveObject(item2));
                            }
                        }
                        else
                        {
                            base.AddChild();
                        }
                    }
                    break;
            }
        }
    }

    internal class EditableRoNSaveObject : DumpableListEntry
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