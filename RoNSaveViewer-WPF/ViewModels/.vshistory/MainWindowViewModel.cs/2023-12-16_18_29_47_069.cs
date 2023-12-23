using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using RoNSaveViewer_WPF.CustomObjects;
using RoNSaveViewer_WPF.RoNSaveToolSuit;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Xml.Linq;
using UeSaveGame;
using UeSaveGame.PropertyTypes;

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
                    UProperty[] arr = new UProperty[RoNSaveGame.Properties.Count];
                    RoNSaveGame.Properties.CopyTo(arr, 0);

                    using (BinaryWriter bw = new BinaryWriter(File.Open(
                      TempFile, FileMode.Create)))
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arr[i].Serialize(bw, false);
                        }
                    }
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

        private string _tempFile = string.Empty;

        public string TempFile { get => _tempFile; set => SetProperty(ref _tempFile, value); }

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
                    RoNSaveObjects.Add(new RoNSaveObject(prop));
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
                EditableRoNSaveObjects.Clear();
                if (value != null)
                {
                    switch (value.OBJUProperty.Type)
                    {
                        case "StructProperty":
                            var val = value.OBJUProperty.Value;
                            var props = val.GetType().GetProperties();

                            foreach (var item in props)
                            {
                                if (item.Name == "Properties")
                                {
                                    IEnumerable itemEnum = item.GetValue(val) as IEnumerable;
                                    foreach (UProperty item2 in itemEnum)
                                    {
                                        EditableRoNSaveObjects.Add(new EditableRoNSaveObject(item2));
                                    }
                                }
                            }
                            EditableRoNSaveObjects.Add(new EditableRoNSaveObject(value.OBJUProperty));
                            break;

                        default:
                            EditableRoNSaveObjects.Add(new EditableRoNSaveObject(value.OBJUProperty));
                            break;
                    }
                }
            }
        }

        public ObservableCollection<RoNSaveObject> _roNSaveObjects = new();

        public ObservableCollection<RoNSaveObject> RoNSaveObjects
        {
            get => _roNSaveObjects;
            set => SetProperty(ref _roNSaveObjects, value);
        }

        public ObservableCollection<EditableRoNSaveObject> _editableRoNSaveObjects = new();

        public ObservableCollection<EditableRoNSaveObject> EditableRoNSaveObjects
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

        public ICommand EditFileCommand { get; set; }

        public void CreateEditFileCommand()
        {
            EditFileCommand = new RelayCommand(EditFile);
        }

        private void EditFile()
        {
        }

        public MainWindowViewModel()
        {
            CreateOpenFileCommand();
            CreateEditFileCommand();
        }
    }
}