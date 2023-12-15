using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using RoNSaveViewer_WPF.CustomObjects;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

                RoNSaveObjects.Clear();
                foreach (UProperty prop in _roNSaveGame.Properties)
                {
                    RoNSaveObjects.Add(new RoNSaveObject(prop.Name, prop));
                }
            }
        }

        public RoNSaveObject _selectedRoNObject;

        public RoNSaveObject SelectedRoNObject
        {
            get => _selectedRoNObject;
            set
            {
                SetProperty(ref _selectedRoNObject, value);
                EditableRoNSaveObjects.Add(new EditableRoNSaveObject(value.Name, value.OBJUProperty));
            }
        }

        public ObservableCollection<RoNSaveObject> _roNSaveObjects = new();

        public ObservableCollection<RoNSaveObject> RoNSaveObjects
        {
            get => _roNSaveObjects;
            set => SetProperty(ref _roNSaveObjects, value);
        }

        public ObservableCollection<RoNSaveObject> _editableRoNSaveObjects = new();

        public ObservableCollection<RoNSaveObject> EditableRoNSaveObjects
        {
            get => _editableRoNSaveObjects;
            set => SetProperty(ref _editableRoNSaveObjects, value);
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