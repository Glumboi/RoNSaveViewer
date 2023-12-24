using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using RoNSaveViewer_WPF.CustomObjects;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using UeSaveGame;
using UeSaveGame.PropertyTypes;

namespace RoNSaveViewer_WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public Visibility FileOpenVisibility
        {
            get { return FilePath != string.Empty ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility SplashVisibility
        {
            get { return FilePath != string.Empty ? Visibility.Visible : Visibility.Collapsed; }
        }

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

        private string _fileName = "No File";

        public string FileName
        {
            get => _fileName;
            set
            {
                SetProperty(ref _fileName, value);
            }
        }

        public string FileNameWindowTitle
        {
            get => FileName;
        }

        public SaveGame _roNSaveGame;

        public SaveGame RoNSaveGame
        {
            get => _roNSaveGame;
            set
            {
                SetProperty(ref _roNSaveGame, value);

                RoNSaveObjects.Clear();
                SaveObjectProperties.Clear();
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

                SaveObjects.Clear();
                SaveObjectProperties.Clear();
                if (value != null)
                {
                    try
                    {
                        IdentifyProp(value);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
        }

        private void IdentifyProp(UProperty uProp)
        {
            switch (uProp.Type)
            {
                case "StructProperty":
                    AddStructPropertyToEditField(uProp);
                    break;

                //Partial implementation
                case "MapProperty":
                    AddMapProperty(uProp.Value);
                    break;

                case "ObjectProperty":
                    AddObjectProperty(uProp as ObjectProperty);
                    break;

                case "ArrayProperty":
                    AddArrayProperty(uProp as ArrayProperty);
                    break;

                default:
                    SaveObjects.Add(uProp);
                    break;
            }
        }

        private void IdentifyProp(RoNSaveObject roNSaveObject)
        {
            switch (roNSaveObject.OBJUProperty.Type)
            {
                case "StructProperty":
                    AddStructPropertyToEditField(roNSaveObject.OBJUProperty);
                    break;

                //Partial implementation
                case "MapProperty":
                    AddMapProperty(roNSaveObject.OBJUProperty.Value);
                    break;

                case "ObjectProperty":
                    AddObjectProperty(roNSaveObject.OBJUProperty as ObjectProperty);
                    break;

                case "ArrayProperty":
                    AddArrayProperty(roNSaveObject.OBJUProperty as ArrayProperty);
                    break;

                case "SetProperty":
                    AddSetProperty(roNSaveObject.OBJUProperty as SetProperty);
                    break;

                default:
                    SaveObjects.Add(roNSaveObject.OBJUProperty);
                    break;
            }
        }

        private void AddArrayProperty(ArrayProperty prop)
        {
            foreach (UProperty item in prop.Value)
            {
                IdentifyProp(item);
            }
        }

        private void AddSetProperty(SetProperty prop)
        {
            foreach (UProperty item in prop.Value)
            {
                IdentifyProp(item);
            }
        }

        private void AddObjectProperty(ObjectProperty prop)
        {
            SaveObjectProperties.Add(prop);
        }

        private void AddMapProperty(object sourceObj)
        {
            if (sourceObj is List<KeyValuePair<UProperty, UProperty>>)
            {
                List<KeyValuePair<UProperty, UProperty>> originalList = sourceObj as List<KeyValuePair<UProperty, UProperty>>;

                foreach (var pair in originalList)
                {
                    IdentifyProp(pair.Value);

                    IdentifyProp(pair.Key);

                    //AddObjectProperty(pair.Key as ObjectProperty);
                }
            }
            else
            {
                Console.WriteLine("The object is not of type List<KeyValuePair<UProperty, UProperty>>");
            }
        }

        private void AddStructPropertyToEditField(UProperty prop)
        {
            var val = prop.Value;
            var props = val.GetType().GetProperties();

            foreach (var item in props)
            {
                if (item.Name == "Properties")
                {
                    IEnumerable itemEnum = item.GetValue(val) as IEnumerable;
                    foreach (UProperty item2 in itemEnum)
                    {
                        IdentifyProp(item2);
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

        public ObservableCollection<UProperty> _saveObjectUproperties = new();

        public ObservableCollection<UProperty> SaveObjects
        {
            get => _saveObjectUproperties;
            set => SetProperty(ref _saveObjectUproperties, value);
        }

        public ObservableCollection<ObjectProperty> _saveObjectProperties = new();

        public ObservableCollection<ObjectProperty> SaveObjectProperties
        {
            get => _saveObjectProperties;
            set => SetProperty(ref _saveObjectProperties, value);
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

        public ICommand SaveFileCommand { get; set; }

        public void CreateSaveFileCommand()
        {
            SaveFileCommand = new RelayCommand(SaveFile);
        }

        private void SaveFile()
        {
            Parallel.For(0, EditableRoNSaveObjects.Count, i =>
            {
                RoNSaveGame.Properties[i] = EditableRoNSaveObjects[i].OBJUProperty;
            });

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultDirectory = FilePath;
            saveFileDialog.Title = $"Save the new File";
            saveFileDialog.FileName = FileName;
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = File.Create(saveFileDialog.FileName))
                {
                    RoNSaveGame.WriteTo(fs);
                }
            }

            Console.WriteLine(RoNSaveGame.ToString());
        }

        public MainWindowViewModel()
        {
            CreateOpenFileCommand();
            CreateSaveFileCommand();
        }
    }
}