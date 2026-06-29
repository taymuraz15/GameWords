using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace NewGameFindWords
{
    
    public class GameBoard
    {
        Random rand = new Random();
        const int Size = 5;
        public Cell[,] GameTable { get; }
        public List<Word> Words { get; }

        public GameBoard()
        {
            GameTable = new Cell[Size, Size];
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    GameTable[x, y] = new Cell(x, y);
                }
            }
            Words = new List<Word>();
        }

        public void AddWord(string text, List<(int X, int Y)> coords)
        {
            Word newWord = new Word(text, coords);
            Words.Add(newWord);
            for (int i = 0; i < text.Length; i++)
            {
                var c = coords[i];
                GameTable[c.X, c.Y].Letter = text[i];
                GameTable[c.X, c.Y].NotFree = true;
            }
        }

        public void FillEmptyCells()
        {
            string alphabet = "ÆАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (!GameTable[x, y].NotFree)
                    {
                        GameTable[x, y].Letter = alphabet[rand.Next(alphabet.Length)];
                    }
                }
            }
        }

        public Word FindWordInDictionaryByCoords(List<(int X, int Y)> selectedCoords)
        {
            foreach (var word in Words)
            {
                if (word.isFound || word.busyCells.Count != selectedCoords.Count)
                    continue;

                int countCurrectCoords = 0;
                foreach (var playerCoord in selectedCoords)
                {
                    foreach (var wordCoord in word.busyCells)
                    {
                        if (playerCoord.X == wordCoord.X && playerCoord.Y == wordCoord.Y)
                        {
                            countCurrectCoords++;
                            break;
                        }
                    }
                }
                if (countCurrectCoords == word.busyCells.Count)
                {
                    word.isFound = true;
                    return word;
                }
            }
            return null;
        }

        public int GetCountNotFoundedWords()
        {
            int count = 0;
            foreach (var word in Words)
            {
                if (!word.isFound) // Если слово еще не найдено
                {
                    count++;
                }
            }
            return count;
        }

        public bool TryAutoAddWord(string text)
        {
            text = text.ToUpper();
            int wordLength = text.Length;

            if (wordLength > Size) return false;

            var validPositions = new List<(int X, int Y, bool IsHorizontal)>();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x <= Size - wordLength; x++)
                {
                    bool canPlace = true;
                    for (int i = 0; i < wordLength; i++)
                    {
                        if (GameTable[x + i, y].NotFree)
                        {
                            canPlace = false;
                            break;
                        }
                    }
                    if (canPlace)
                    {
                        validPositions.Add((x, y, true));
                    }
                }
            }
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y <= Size - wordLength; y++)
                {
                    bool canPlace = true;
                    for (int i = 0; i < wordLength; i++)
                    {
                        if (GameTable[x, y + i].NotFree)
                        {
                            canPlace = false;
                            break;
                        }
                    }
                    if (canPlace)
                    {
                        validPositions.Add((x, y, false));
                    }
                }
            }
            if (validPositions.Count == 0)
            {
                return false;
            }

            Random rand = new Random();
            var chosenPosition = validPositions[rand.Next(validPositions.Count)];

            List<(int X, int Y)> finalCoords = new List<(int X, int Y)>();
            for (int i = 0; i < wordLength; i++)
            {
                int currentX;
                int currentY;

                if (chosenPosition.IsHorizontal)
                {
                    currentX = chosenPosition.X + i;
                    currentY = chosenPosition.Y;
                }
                else
                {
                    currentX = chosenPosition.X;
                    currentY = chosenPosition.Y + i;
                }

                finalCoords.Add((currentX, currentY));
            }

            AddWord(text, finalCoords);
            return true;
        }



    }
}
