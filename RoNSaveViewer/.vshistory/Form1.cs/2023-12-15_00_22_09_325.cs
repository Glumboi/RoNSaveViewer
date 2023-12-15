using RoNSaveViewer.CustomControls;
using System.Collections;
using System.Diagnostics;
using UeSaveGame;
using UeSaveGame.PropertyTypes;

namespace RoNSaveViewer;

public partial class Form1 : Form
{
    private string _saveFile;

    public string SaveFile
    {
        get => _saveFile;
        set
        {
            _saveFile = value;
            binaryReader = new BinaryReader(File.OpenRead(SaveFile));
            ReadSaveFile();
        }
    }

    private SaveGame _saveGameFile;

    public SaveGame SaveGameFile
    {
        get => _saveGameFile;
        set
        {
            _saveGameFile = value;
            PopulateTreeView();
        }
    }

    private BinaryReader binaryReader;

    public Form1()
    {
        InitializeComponent();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SaveFile = ofd.FileName;
            }
        }
    }

    private void ReadSaveFile()
    {
        using (FileStream fs = File.OpenRead(SaveFile))
        {
            SaveGameFile = SaveGame.LoadFrom(fs);
        }
    }

    private void PopulateTreeView()
    {
        saveContentsTree.Nodes.Clear();

        foreach (var prop in SaveGameFile.Properties)
        {
            RonObjectTreeNode node = new RonObjectTreeNode(prop.Name, prop);
            saveContentsTree.Nodes.Add(node);
            if (prop.Type == "ArrayProperty")
            {
                UProperty[] array = prop.Value as UProperty[];
                foreach (var item in array)
                {
                    node.Nodes.Add(new RonObjectTreeNode(item.Value.ToString(), item));
                }
            }
        }
    }

    private void saveContentsTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
        RonObjectTreeNode node = e.Node as RonObjectTreeNode;

        if (node != null)
        {
            listBoxValuesOfUProperty.Items.Clear();

            if (node.UPropertyOfNode.Type == "ArrayProperty")
            {
                var array = node.UPropertyOfNode.Value as UProperty[];

                listBoxValuesOfUProperty.Items.AddRange(array);
                return;
            }

            if (node.UPropertyOfNode.Type == "StructProperty")
            {
                var val = node.UPropertyOfNode.Value;
                var props = val.GetType().GetProperties();

                foreach (var item in props)
                {
                    if (item.Name == "Properties")
                    {
                        IEnumerable itemEnum = item.GetValue(val) as IEnumerable;
                        foreach (UProperty item2 in itemEnum)
                        {
                            listBoxValuesOfUProperty.Items.Add(item2);
                        }
                    }
                }
                listBoxValuesOfUProperty.Items.Add(node.UPropertyOfNode.Value);
            }
        }
    }

    private async void listBoxValuesOfUProperty_DoubleClick(object sender, EventArgs e)
    {
        var ls = (ListBox)sender;
        var selected = ls.SelectedItem;
        var editForm = new EditForm(selected as UProperty);
        selected = editForm.ShowWithResult(ref editForm);
    }
}