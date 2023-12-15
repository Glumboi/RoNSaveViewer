using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using RoNSaveViewer_WPF.CustomObjects;
using System.IO;
using System.Windows.Input;
using UeSaveGame;

namespace RoNSaveViewer_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _filePath = string.Empty;

        public string FilePath
        {
            get => _filePath;
            set
            {
                SetProperty(ref _filePath, value);
                int indexStart = _filePath.LastIndexOf('\\');
                int length = _filePath.Length - indexStart;
                FileName = _filePath?.Substring(indexStart + 1, length - 1);

                using (FileStream fs = File.OpenRead(value))
                {
                    RoNSaveGame = SaveGame.LoadFrom(fs);
                }
            }
        }

        private string _fileName = "RoNSaveViewer ";

        public string FileName
        {
            get => _fileName;
            set
            {
                SetProperty(ref _fileName, "RoNSaveViewer, editing: " + value);
            }
        }

        public SaveGame _roNSaveGame;

        public SaveGame RoNSaveGame
        {
            get => _roNSaveGame;
            set
            {
                SetProperty(ref _roNSaveGame, value);

                foreach (UProperty prop in _roNSaveGame.Properties)
                {
                    RoNSaveObjects.Add(new RoNSaveObject(prop.Name, prop));
                }
            }
        }

        public IObservable<RoNSaveObject> _roNSaveObjects = new();

        public ICollectionView<RoNSaveObject> RoNSaveObjects
        {
            get => _roNSaveObjects;
            set => SetProperty(ref _roNSaveObjects, value);
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