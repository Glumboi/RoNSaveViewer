using RoNSaveViewer_WPF.CustomObjects;
using RoNSaveViewer_WPF.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
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
            var textBox = (System.Windows.Controls.TextBox)e.EditingElement;
            int selectedIndex = dataGrid.SelectedIndex;
            Type valueType = vm.SaveObjects[selectedIndex].Value.GetType();

            if (valueType == null)
            {
                System.Windows.MessageBox.Show("Cannot be empty!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
                return;
            }

            if (valueType == typeof(FString))
            {
                vm.SaveObjects[selectedIndex].Value = new FString(textBox.Text);
                return;
            }

            var convertedText = Convert.ChangeType(textBox.Text, valueType);
            vm.SaveObjects[selectedIndex].Value = convertedText;
        }
    }
}