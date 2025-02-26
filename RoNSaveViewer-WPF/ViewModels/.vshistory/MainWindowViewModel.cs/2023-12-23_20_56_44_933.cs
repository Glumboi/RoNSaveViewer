﻿using GalaSoft.MvvmLight.Command;
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
                if (value != null)
                {
                    switch (value.OBJUProperty.Type)
                    {
                        case "StructProperty":
                            AddStructPropertyToEditField(value.OBJUProperty);
                            break;

                        //Partial implementation
                        case "MapProperty":
                            AddMapProperty(value.OBJUProperty.Value);
                            break;

                        case "SetProperty":
                            var val = value;
                            break;

                        default:
                            SaveObjects.Add(value.OBJUProperty);
                            break;
                    }
                }
            }
        }

        private void AddMapProperty(object sourceObj)
        {
            if (sourceObj is List<KeyValuePair<UProperty, UProperty>>)
            {
                List<KeyValuePair<UProperty, UProperty>> originalList = sourceObj as List<KeyValuePair<UProperty, UProperty>>;

                foreach (var pair in originalList)
                {
                    if (pair.Value.Type == "StructProperty")
                    {
                        AddStructPropertyToEditField(pair.Value);
                    }

                    SaveObjects.Add(pair.Key);
                    SaveObjects.Add(pair.Value);
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
                        SaveObjects.Add(item2);
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

        public ObservableCollection<UProperty> _saveObjects = new();

        public ObservableCollection<UProperty> SaveObjects
        {
            get => _saveObjects;
            set => SetProperty(ref _saveObjects, value);
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