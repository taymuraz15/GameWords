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

            // Показываем его модально (пока игрок не закроет его, меню будет недоступно)
            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                // Получаем имя из формы
                string playerName = nameForm.PlayerName;

                // 2. Запускаем игру и передаем туда имя (а тему пока оставим по умолчанию)
                Form1 game = new Form1(playerName);

                game.Show();
                this.Hide(); // Прячем меню

                game.FormClosed += (s, args) => this.Close();
            }
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
