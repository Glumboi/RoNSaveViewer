namespace RoNSaveViewer;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        tableLayoutPanel1 = new TableLayoutPanel();
        saveContentsTree = new TreeView();
        listBoxValuesOfUProperty = new ListBox();
        menuStrip1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Size = new Size(103, 22);
        openToolStripMenuItem.Text = "Open";
        openToolStripMenuItem.Click += openToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Size = new Size(103, 22);
        saveToolStripMenuItem.Text = "Save";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.125F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.875F));
        tableLayoutPanel1.Controls.Add(saveContentsTree, 0, 0);
        tableLayoutPanel1.Controls.Add(listBoxValuesOfUProperty, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 24);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90.61033F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.389671F));
        tableLayoutPanel1.Size = new Size(800, 426);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // saveContentsTree
        // 
        saveContentsTree.Dock = DockStyle.Fill;
        saveContentsTree.Location = new Point(3, 3);
        saveContentsTree.Name = "saveContentsTree";
        saveContentsTree.Size = new Size(211, 380);
        saveContentsTree.TabIndex = 0;
        saveContentsTree.AfterSelect += saveContentsTree_AfterSelect;
        // 
        // listBoxValuesOfUProperty
        // 
        listBoxValuesOfUProperty.Dock = DockStyle.Fill;
        listBoxValuesOfUProperty.FormattingEnabled = true;
        listBoxValuesOfUProperty.ItemHeight = 15;
        listBoxValuesOfUProperty.Location = new Point(220, 3);
        listBoxValuesOfUProperty.Name = "listBoxValuesOfUProperty";
        listBoxValuesOfUProperty.Size = new Size(577, 380);
        listBoxValuesOfUProperty.TabIndex = 1;
        listBoxValuesOfUProperty.DoubleClick += listBoxValuesOfUProperty_DoubleClick;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(tableLayoutPanel1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Form1";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private TableLayoutPanel tableLayoutPanel1;
    private TreeView saveContentsTree;
    private ListBox listBoxValuesOfUProperty;
}