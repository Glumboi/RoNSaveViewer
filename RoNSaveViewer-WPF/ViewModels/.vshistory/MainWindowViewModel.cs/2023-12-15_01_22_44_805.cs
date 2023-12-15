using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoNSaveViewer_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _filePath;

        public string FilePath{ get => _filePath; set => SetProperty(ref _filePath, value); }

        public ICommand OpenFileCommand{ get; set; }

        public void CreateOpenFileCommand()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        void OpenFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
        }

        public MainWindowViewModel() 
        {
            CreateOpenFileCommand();
        }
    }
}
