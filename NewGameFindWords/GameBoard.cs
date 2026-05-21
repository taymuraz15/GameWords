using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameFindWords
{
    public class GameBoard
    {
        // Константа размера поля
        private const int Size = 5;

        // Двумерный массив всех ячеек поля
        public Cell[,] Grid { get; }

        // Список слов, которые спрятаны на поле
        public List<Word> Words { get; }

        public GameBoard()
        {
            // 1. Инициализируем массив
            Grid = new Cell[Size, Size];

            // 2. Заполняем массив объектами Cell
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Grid[x, y] = new Cell(x, y);
                }
            }

            // 3. Создаем пустой список для будущих слов
            Words = new List<Word>();
        }

        // Метод для добавления слова на поле (упрощенная версия)
        public void AddWord(string text, List<(int X, int Y)> coords)
        {
            // Создаем новый объект Word
            Word newWord = new Word(text, coords);
            Words.Add(newWord);

            // Помечаем ячейки на поле как занятые и вписываем буквы
            for (int i = 0; i < text.Length; i++)
            {
                var c = coords[i];
                Grid[c.X, c.Y].Letter = text[i];
                Grid[c.X, c.Y].NotFree = true;
            }
        }

        // Метод для заполнения пустых мест случайными буквами
        public void FillEmptyCells()
        {
            Random rand = new Random();
            string alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    // Если ячейка не занята словом, ставим случайную букву
                    if (!Grid[x, y].NotFree)
                    {
                        Grid[x, y].Letter = alphabet[rand.Next(alphabet.Length)];
                    }
                }
            }
        }

        public Word CheckSelection(List<(int X, int Y)> selectedCoords)
        {
            foreach (var word in Words)
            {
                // 1. Если слово уже найдено или длины не совпадают — сразу мимо
                if (word.isFound || word.busyCells.Count != selectedCoords.Count)
                    continue;

                // 2. Проверяем, что каждая координата игрока есть в списке слова
                int matchCount = 0;
                foreach (var playerCoord in selectedCoords)
                {
                    foreach (var wordCoord in word.busyCells)
                    {
                        if (playerCoord.X == wordCoord.X && playerCoord.Y == wordCoord.Y)
                        {
                            matchCount++;
                            break;
                        }
                    }
                }

                // 3. Если количество совпавших координат равно длине слова — победа!
                if (matchCount == word.busyCells.Count)
                {
                    word.isFound = true;
                    return word;
                }
            }
            return null;
        }

    }
}
