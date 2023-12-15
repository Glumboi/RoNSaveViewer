using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UeSaveGame;

namespace RoNSaveViewer
{
    public partial class EditForm : Form
    {
        private UProperty _gameProperty;

        private UProperty GameProperty
        {
            get => _gameProperty;
            set
            {
                _gameProperty = value;
                valueRtb.Text = _gameProperty.Value.ToString();
            }
        }

        public EditForm(UProperty prop)
        {
            InitializeComponent();
            GameProperty = prop;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            GameProperty.Value = valueRtb.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public UProperty ShowWithResult(ref EditForm form)
        {
            form.ShowDialog();
            return GameProperty;
        }
    }
}