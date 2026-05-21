using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
