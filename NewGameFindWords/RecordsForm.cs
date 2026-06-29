using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NewGameFindWords
{
    public partial class RecordsForm : Form
    {
        public RecordsForm()
        {
            InitializeComponent();
            string filePath = "records.json";

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(filePath);

                    var records = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonString);

                    if (records != null)
                    {
                        foreach (var game in records)
                        {
                            string line = $"Игрок: {game["Имя"]} | Время: {game["Время"]} | Подсказок: {game["Подсказки"]} [Тема: {game["Тема"]}, {game["Сложность"]}]";

                            listBoxRecords.Items.Add(line);
                        }
                    }
                }
                catch
                {
                    listBoxRecords.Items.Add("Ошибка: не удалось прочитать или расшифровать файл рекордов.");
                }
            }
            else
            {
                listBoxRecords.Items.Add("История игр пока пуста. Сыграйте и победите в первом раунде!");
            }
        }
    }
}
