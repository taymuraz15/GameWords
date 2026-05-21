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
    public partial class Form1 : Form
    {
        GameBoard gameBoard;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(5);
            dataGridView1.Rows[0].Cells[0].Value = "А";
            gameBoard = new GameBoard();
            gameBoard.AddWord("ГÆДЫ", new List<(int X, int Y)> { (0, 0), (1, 0), (2, 0), (3,0) });
            gameBoard.AddWord("КУЫДЗ", new List<(int X, int Y)> { (0, 2), (1, 2), (2, 2), (3, 2), (4, 2) });
            gameBoard.FillEmptyCells();
            FillGameTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FillGameTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(5);

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    dataGridView1.Rows[y].Cells[x].Value = gameBoard.GameTable[x, y].Letter;
                }
            }
        }
        List<(int X, int Y)> userSelection = new List<(int X, int Y)>();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int x = e.ColumnIndex;
            int y = e.RowIndex;

            bool isCellSelectedBefore = false;
            foreach (var p in userSelection)
            {
                if (p.X == x && p.Y == y)
                {
                    isCellSelectedBefore = true;
                    break;
                }
            }

            if (!isCellSelectedBefore)
            {
                userSelection.Add((x, y));
                dataGridView1.Rows[y].Cells[x].Style.BackColor = Color.Yellow;
            }
            else
            {
                userSelection.Remove((x, y));
                dataGridView1.Rows[y].Cells[x].Style.BackColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var foundWord = gameBoard.FindWordInDictionaryByCoords(userSelection);

            if (foundWord != null)
            {
                MessageBox.Show($"Вы нашли слово: {foundWord.text}");
                foreach (var coord in userSelection)
                {
                    dataGridView1.Rows[coord.Y].Cells[coord.X].Style.BackColor = Color.LightGreen;
                }
            }
            else
            {
                MessageBox.Show("Такого слова нет или выбраны не все буквы");
                foreach (var coord in userSelection)
                {
                    dataGridView1.Rows[coord.Y].Cells[coord.X].Style.BackColor = Color.White;
                }
            }
            userSelection.Clear();
        }
    }
}
