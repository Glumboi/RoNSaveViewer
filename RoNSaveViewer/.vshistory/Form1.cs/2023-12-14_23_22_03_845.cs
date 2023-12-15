using RoNSaveViewer.CustomControls;
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
        RonObjectTreeNode selectedObj = listBoxValuesOfUProperty.SelectedItems[0] as RonObjectTreeNode;
        listBoxValuesOfUProperty.Items.Clear();
        listBoxValuesOfUProperty.Items.Add(selectedObj.UPropertyOfNode.Value);
    }
}