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
using Wpf.Ui.Controls;
using DataGrid = Wpf.Ui.Controls.DataGrid;

namespace RoNSaveViewer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        private MainWindowViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = this.DataContext as MainWindowViewModel;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Wpf.Ui.Controls.DataGrid dataGrid = sender as Wpf.Ui.Controls.DataGrid;
            var textBox = (System.Windows.Controls.TextBox)e.EditingElement;

            if (dataGrid != null && e.EditAction == DataGridEditAction.Commit)
            {
                int selectedIndex = dataGrid.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < vm.SaveObjects.Count)
                {
                    Type valueType = vm.SaveObjects[selectedIndex].Value.GetType();

                    if (valueType == null || string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (valueType == typeof(FString))
                    {
                        vm.SaveObjects[selectedIndex].Value = new FString(textBox.Text);
                        return;
                    }

                    try
                    {
                        var convertedText = Convert.ChangeType(textBox.Text, valueType);
                        vm.SaveObjects[selectedIndex].Value = convertedText;
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Wrong Type!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}