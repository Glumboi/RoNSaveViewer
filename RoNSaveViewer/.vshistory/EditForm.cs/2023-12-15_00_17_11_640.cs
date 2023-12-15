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

                valueRtb.TextChanged += ValueRtb_TextChanged;
            }
        }

        public EditForm(UProperty prop)
        {
            InitializeComponent();
        }

        private void ValueRtb_TextChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}