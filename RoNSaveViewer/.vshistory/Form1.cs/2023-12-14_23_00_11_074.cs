using UeSaveGame;

namespace RoNSaveViewer;

public partial class Form1 : Form
{
    private string _saveFile;

    public string SaveFile
    {
        get =>
        set
        {
            ReadSaveFile();
        }
    }

    public SaveGame SaveGameFile
    {
        get;
        set;
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
                ReadSaveFile();
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
}