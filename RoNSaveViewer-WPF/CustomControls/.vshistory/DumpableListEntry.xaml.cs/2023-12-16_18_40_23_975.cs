using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoNSaveViewer_WPF.CustomControls
{
    /// <summary>
    /// Interaktionslogik für DumpableListEntry.xaml
    /// </summary>
    public partial class DumpableListEntry : ListBoxItem
    {
        public DumpableListEntry()
        {
            InitializeComponent();
        }

        private void ListBoxItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DumpContextMenu.Visibility = Visibility.Visible;
        }
    }
}