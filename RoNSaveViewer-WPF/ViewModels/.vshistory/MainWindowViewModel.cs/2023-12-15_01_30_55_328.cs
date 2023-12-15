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
        private string _filePath = null;

        public string FilePath { get => _filePath; set => SetProperty(ref _filePath, value); }

        private string _fileName = null;

        public string FileName
        {
            get
            {
            }"RoNSaveViewer " + FilePath?.Substring(FilePath.LastIndexOf('\\'), FilePath.LastIndexOf('.'));
        }

    public ICommand OpenFileCommand { get; set; }

    public void CreateOpenFileCommand()
    {
        OpenFileCommand = new RelayCommand(OpenFile);
    }

    private void OpenFile()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Title = "Select a save file";
        if (ofd.ShowDialog() == true)
        {
            FilePath = ofd.FileName;
        }
    }

    public MainWindowViewModel()
    {
        CreateOpenFileCommand();
    }
}
}