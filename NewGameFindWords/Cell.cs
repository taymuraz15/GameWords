using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameFindWords
{
    public class Cell
    {
        public char Letter { 
            get; 
            set; 
        }

        public int X { 
            get; 
        }
        public int Y { 
            get; 
        }

        public bool isSelectedByUser { 
            get; 
            set; 
        }

        public bool notFree { 
            get; 
            set; 
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Letter = ' ';
            isSelectedByUser = false;
        }
    }
}
