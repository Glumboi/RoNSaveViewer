using UeSaveGame;

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
        foreach (var prop in SaveGameFile?.Properties)
        {
            saveContentsTree.Nodes.Add(prop.Name);
        }
    }
}