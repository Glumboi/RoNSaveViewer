﻿using RoNSaveViewer_WPF.CustomObjects;
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
            DataGrid dataGrid = sender as DataGrid;
            Type type = vm.SaveObjects[dataGrid.SelectedIndex].Value.GetType();
            Dictionary<Type, int> typeDict = new Dictionary<Type, int>
            {
                {typeof(Int32),0},
                {typeof(string),1},
                {typeof(FString),2},
                {typeof(bool),3},
            };
            switch (typeDict[o.GetType()])
            {
                case 0:
                    break;

                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                default:
                    break;
            }

            if ( == typeof(FString))
            {
                vm.SaveObjects[dataGrid.SelectedIndex].Value = new FString("1212");
            }
            else
            {
                vm.SaveObjects[dataGrid.SelectedIndex].Value = "232323";
            }
        }
    }
}