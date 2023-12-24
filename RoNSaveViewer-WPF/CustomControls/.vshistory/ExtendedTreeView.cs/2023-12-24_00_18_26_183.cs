using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace RoNSaveViewer_WPF.CustomControls
{
    public class ExtendedTreeView : Wpf.Ui.Controls.TreeViewItem
    {
        public ExtendedTreeView()
            : base()
        {
            //this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH);
        }

        private void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /*if (SelectedItem != null)
            {
                SetValue(SelectedItem_Property, SelectedItem);
            }*/
        }

        public object SelectedItem_
        {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }

        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register("SelectedItem_", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null));
    }
}