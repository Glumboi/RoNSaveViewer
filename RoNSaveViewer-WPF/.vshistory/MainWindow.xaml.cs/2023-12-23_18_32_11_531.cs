using RoNSaveViewer_WPF.CustomObjects;
using RoNSaveViewer_WPF.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoNSaveViewer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = this.DataContext as MainWindowViewModel;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            RoNSaveObject currentObj = vm.RoNSaveObjects[dataGrid.SelectedIndex];
            var currentColumn = e.Column as DataGridTextColumn;

            if (currentColumn == null || currentObj == null)
            {
                return;
            }

            var editedTextBox = e.EditingElement as TextBox;

            switch (currentColumn.Header)
            {
                case "Name":
                    currentObj.OBJUProperty.Name = editedTextBox.Text;
                    break;
                case "OBJName":
                    currentObj.OBJName = editedTextBox.Text;
                    break;
                case "Value":
                    currentObj.OBJUProperty.Value = editedTextBox.Text;

                    break;
                case "Type":
                    break; 
                default:
                    Debug.WriteLine("Not a known Column");
                    return;
            }

        }
    }
}