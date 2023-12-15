using GalaSoft.MvvmLight.Command;
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

        public string FilePath{ get; set; }

        public ICommand OpenFileCommand{ get; set; }

        public void CreateOpenFileCommand()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        void OpenFile()
        {

        }

        public MainWindowViewModel() 
        {
            CreateOpenFileCommand();
        }
    }
}
