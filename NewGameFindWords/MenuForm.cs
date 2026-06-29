using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewGameFindWords
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            NameInputForm nameForm = new NameInputForm();

            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                string playerName = nameForm.PlayerName;
                GameForm game = new GameForm(playerName);

                this.Hide();        
                game.ShowDialog();  
                this.Close();
            }
        }
        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
