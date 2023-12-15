namespace RoNSaveViewer;

public partial class Form1 : Form
{
    public string SaveFile { get; set; }

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
            }
        }
    }
}