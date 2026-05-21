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
        const int sizeOfBoard = 5;
        public Cell[,] allCells { get; }

        public List<Word> allWordsOnBoard { get; }

        public GameBoard()
        {
            allCells = new Cell[sizeOfBoard, sizeOfBoard];

            for (int y = 0; y < sizeOfBoard; y++)
            {
                for (int x = 0; x < sizeOfBoard; x++)
                {
                    allCells[x, y] = new Cell(x, y);
                }
            }
            allWordsOnBoard = new List<Word>();
        }

        public void addWord(string text, List<(int X, int Y)> coords)
        {
            Word word = new Word(text, coords);
            allWordsOnBoard.Add(word);

            for (int i = 0; i < text.Length; i++)
            {
                var c = coords[i];
                allCells[c.X, c.Y].Letter = text[i];
                allCells[c.X, c.Y].notFree = true;
            }
        }

        public void fillEmptyCells()
        {
            
            string alph = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

            for (int y = 0; y < sizeOfBoard; y++)
            {
                for (int x = 0; x < sizeOfBoard; x++)
                {
                    if (!allCells[x, y].notFree)
                    {
                        allCells[x, y].Letter = alph[rand.Next(alph.Length)];
                    }
                }
            }
        }
    }
}
