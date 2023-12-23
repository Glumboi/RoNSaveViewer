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
using UeSaveGame;

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
            UProperty currentObj = vm.SaveObjects[dataGrid.SelectedIndex];
            var currentColumn = e.Column as DataGridTextColumn;

            if (currentColumn == null || currentObj == null)
            {
                return;
            }

            var editedTextBox = e.EditingElement as TextBox;

            switch (currentColumn.Header)
            {
                case "Value":
                    Type valueType = currentObj.Value.GetType();
                    object convertedValue;

                    try
                    {
                        convertedValue = Convert.ChangeType(editedTextBox.Text, Nullable.GetUnderlyingType(valueType) ?? valueType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Conversion error: {ex.Message}");
                    }

                    currentObj.Value = editedTextBox.Text;
                    break;

                default:
                    MessageBox.Show($"{currentColumn.Header} cannot be changed, mostlikely readonly!", $"Cannot change {currentColumn.Header}!");
                    return;
            }
        }
    }
}