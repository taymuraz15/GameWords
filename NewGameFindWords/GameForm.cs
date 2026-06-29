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
    
    public partial class GameForm : Form
    {
        Random rand = new Random();
        GameBoard gameBoard;
        List<(int X, int Y)> userSelection = new List<(int X, int Y)>();
        
        string currentPlayerName = "Игрок";
        int countPodskazok = 0;
        string currentTheme = "Животные";
        string currentDifficulty = "Легко";
        int secondsPassed = 0;
        int currentRoundTimeLimit = 90; 

        public GameForm(string name)
        {
            InitializeComponent();
            currentPlayerName = name; 
            StartNewGame();
        }

        void StartNewGame()
        {
            countPodskazok = 0;
            userSelection.Clear();
            int boardSize = 5;
            int wordsCountToPlace = 2;

            if (currentDifficulty == "Легко")
            {
                boardSize = 5;
                wordsCountToPlace = 2;
                currentRoundTimeLimit = 90; 
            }
            else if (currentDifficulty == "Нормально")
            {
                boardSize = 7;
                wordsCountToPlace = 4;
                currentRoundTimeLimit = 180; 
            }

            gameBoard = new GameBoard(boardSize);

            List<string> themeWordsBank = new List<string>();

            if (currentTheme == "Животные")
            {
                themeWordsBank.AddRange(new string[] {
                    "КУЫДЗ", "ГÆДЫ", "РУВАС", "ХЪУГ", "ПЫЛ", "БАБЫЗ", "КАРК", "БÆХ", "АРС", "УАСÆГ"
                });
            }
            else if (currentTheme == "Люди")
            {
                themeWordsBank.AddRange(new string[] {
                    "МАД", "ФЫД", "ХО", "НАНА", "ДАДА", "ФЫРТ", "ЧЫЗГ", "СЫХАГ"
                });
            }

            
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
            secondsPassed = 0;
            labelTimer.Text = FormatTime(currentRoundTimeLimit);
            wordTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FillGameTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            int size = gameBoard.Size;

            for (int i = 0; i < size; i++)
            {
                var column = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(column);
            }

            dataGridView1.Rows.Add(size);

            int calculatedRowHeight = (dataGridView1.ClientSize.Height - 2) / size;

            for (int y = 0; y < size; y++)
            {
                dataGridView1.Rows[y].Height = calculatedRowHeight; 

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
                    wordTimer.Stop();
                    string timeSpent = FormatTime(secondsPassed);

                    string filePath = "records.json";
                    List<Dictionary<string, string>> allRecords = new List<Dictionary<string, string>>();

                    if (System.IO.File.Exists(filePath))
                    {
                        try
                        {
                            string jsonString = System.IO.File.ReadAllText(filePath);
                            allRecords = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);
                        }
                        catch {  }
                    }

                    Dictionary<string, string> newGameData = new Dictionary<string, string>();
                    newGameData["Имя"] = currentPlayerName;
                    newGameData["Время"] = timeSpent;
                    newGameData["Подсказки"] = countPodskazok.ToString();
                    newGameData["Тема"] = currentTheme;
                    newGameData["Сложность"] = currentDifficulty;

                    allRecords.Add(newGameData);

                    var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true }; 
                    string updatedJson = System.Text.Json.JsonSerializer.Serialize(allRecords, options);
                    System.IO.File.WriteAllText(filePath, updatedJson);

                    MessageBox.Show($"Поздравляем! Вы нашли все слова и выиграли! Штрафных баллов: {countPodskazok}");
                    StartNewGame();
                    return;
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
            currentTheme = "Люди";
            StartNewGame();
        }

        

        private void животныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTheme = "Животные";
            StartNewGame();
        }

        private void подсказкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string hintWord = gameBoard.GetRandomNotFoundedWord();

            if (hintWord != null)
            {
                countPodskazok++;
                MessageBox.Show($"Подсказка: Попробуй найти слово «{hintWord}»!", "Подсказка", MessageBoxButtons.OK);
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

        private void wordTimer_Tick(object sender, EventArgs e)
        {
            secondsPassed++; 

            int timeLeft = currentRoundTimeLimit - secondsPassed; 

            labelTimer.Text = FormatTime(timeLeft);

            if (timeLeft <= 0)
            {
                wordTimer.Stop();
                MessageBox.Show("Время раунда вышло! Вы проиграли. Игра начнется заново.", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Error);

                StartNewGame(); 
            }
        }
        
        private string FormatTime(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }

        private void сохранитьРезультатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordTimer.Stop();

            NameInputForm nameForm = new NameInputForm();

            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                currentPlayerName = nameForm.PlayerName;
                StartNewGame();
            }
            else
            {
                wordTimer.Start();
            }
        }

        private void результатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordTimer.Stop();

            RecordsForm recordsWindow = new RecordsForm();

            recordsWindow.ShowDialog();

            wordTimer.Start();
        }
    }
}
