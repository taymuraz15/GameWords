using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NewGameFindWords
{
    public class Word
    {
        public string text { 
            get; 
        }
        public bool isFound {
            get; 
            set; 
        }
        public List<(int X, int Y)> busyCells { 
            get; 
        }
        public Word(string s, List<(int X, int Y)> coordinates)
        {
            text = s.ToUpper();
            busyCells = coordinates;
            isFound = false;
        }
        public bool ContainsCell(int x, int y)
        {
            foreach (var cell in busyCells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
