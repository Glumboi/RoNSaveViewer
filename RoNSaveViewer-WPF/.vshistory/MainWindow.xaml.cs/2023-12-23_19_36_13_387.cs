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
            var textBox = (TextBox)e.EditingElement;
            DataGrid dataGrid = sender as DataGrid;

            if (dataGrid != null && e.EditAction == DataGridEditAction.Commit)
            {
                int selectedIndex = dataGrid.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < vm.SaveObjects.Count)
                {
                    Type valueType = vm.SaveObjects[selectedIndex].Value.GetType();

                    Dictionary<Type, int> typeDict = new Dictionary<Type, int>
            {
                { typeof(int), 0 },
                { typeof(FString), 1 },
                { typeof(bool), 2 },
            };

                    if (typeDict.TryGetValue(valueType, out int typeIndex))
                    {
                        switch (typeIndex)
                        {
                            case 0:
                                // Handle Int32
                                int intValue;
                                if (int.TryParse(((TextBox)e.EditingElement).Text, out intValue))
                                {
                                    vm.SaveObjects[selectedIndex].Value = intValue;
                                }
                                break;

                            case 1:
                                // Handle FString
                                vm.SaveObjects[selectedIndex].Value = new FString(((TextBox)e.EditingElement).Text);
                                break;

                            case 2:
                                // Handle bool
                                bool boolValue;
                                if (bool.TryParse(((TextBox)e.EditingElement).Text, out boolValue))
                                {
                                    vm.SaveObjects[selectedIndex].Value = boolValue;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // Handle unknown type
                        Debug.WriteLine("Unknown type");
                    }
                }
            }
        }
    }
}