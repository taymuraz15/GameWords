using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NewGameFindWords
{
    public partial class NameInputForm : Form
    {
        public string PlayerName { get; private set; }
        public NameInputForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPlayerName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PlayerName = textBoxPlayerName.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxPlayerName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
