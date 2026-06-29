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
        List<(int X, int Y)> userSelection = new List<(int X, int Y)>();

        string currentTheme = "Животные";
        string currentDifficulty = "Легко";

        public Form1()
        {
            InitializeComponent();
            StartNewGame();
        }

        void StartNewGame()
        {
            userSelection.Clear();
            int boardSize = 5;
            int wordsCountToPlace = 2;

            if (currentDifficulty == "Нормально")
            {
                boardSize = 7;
                wordsCountToPlace = 4; // На большом поле ищем 4 слова!
            }
            gameBoard = new GameBoard(boardSize);

            List<string> themeWordsBank = new List<string>();

            if (currentTheme == "Животные")
            {
                themeWordsBank.AddRange(new string[] {
                    "ГÆДЫ", "КУЫДЗ", "РУВАС", "БÆХ",
                    "ГАЛ", "ЦÆГÆР", "АРС", "БИРÆГ", "СТЫР"
                });
            }
            else if (currentTheme == "Еда")
            {
                themeWordsBank.AddRange(new string[] {
                    "НУРР", "СЫРХ", "ÆХСЫР", "ЦÆХХ",
                    "ДУР", "КАША", "СУПП", "БÆРÆГ", "ФЫД"
                });
            }

            Random rand = new Random();
            for (int i = themeWordsBank.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                string temp = themeWordsBank[i];
                themeWordsBank[i] = themeWordsBank[j];
                themeWordsBank[j] = temp;
            }


            int wordsPlaced = 0;
            foreach (var wordText in themeWordsBank)
            {
                if (wordsPlaced >= wordsCountToPlace) break;

                bool success = gameBoard.TryAutoAddWord(wordText);
                if (success)
                {
                    wordsPlaced++;
                }
            }

            gameBoard.FillEmptyCells();

            FillGameTable();

            int countNeedToFindWords = gameBoard.GetCountNotFoundedWords();
            labelCountFindWord.Text = countNeedToFindWords.ToString();

            this.Text = $"Поиск слов | Тема: {currentTheme} | Сложность: {currentDifficulty}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FillGameTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            int size = gameBoard.Size;

            // 1. Создаем колонки динамически (их ширину как Fill настроит сам дизайнер)
            for (int i = 0; i < size; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(column);
            }

            // 2. Создаем строки
            dataGridView1.Rows.Add(size);

            // 3. Вычисляем точную высоту одной строки на основе размера таблицы на экране
            // Вычитаем 2 пикселя, чтобы избежать появления вертикального скролла
            int calculatedRowHeight = (dataGridView1.ClientSize.Height - 2) / size;

            // 4. Задаем высоту строк и заполняем буквами
            for (int y = 0; y < size; y++)
            {
                dataGridView1.Rows[y].Height = calculatedRowHeight; // Вот эта магия!

                for (int x = 0; x < size; x++)
                {
                    dataGridView1.Rows[y].Cells[x].Value = gameBoard.GameTable[x, y].Letter;
                }
            }
        }



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
                int countNeedToFindWords = gameBoard.GetCountNotFoundedWords();

                labelCountFindWord.Text = countNeedToFindWords.ToString();

                if (countNeedToFindWords == 0)
                {
                    MessageBox.Show("Поздравляем! Вы нашли все слова и выиграли!");
                    StartNewGame();
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

      
        private void едаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            currentTheme = "Еда";
            StartNewGame();
        }

        

        private void животныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTheme = "Животные";
            StartNewGame();
        }

        private void подсказкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Запрашиваем у мозга игры одно ненайденное слово
            string hintWord = gameBoard.GetRandomNotFoundedWord();

            if (hintWord != null)
            {
                MessageBox.Show($"Подсказка: Попробуй найти слово «{hintWord}»!", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Все слова уже найдены!", "Ой", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void легко5Х5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficulty = "Легко";
            StartNewGame();
        }

        private void нормально7Х7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficulty = "Нормально";
            StartNewGame();
        }
    }
}
