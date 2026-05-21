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
    }
}
