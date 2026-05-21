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

        public bool IsSelectedByUser { 
            get; 
            set; 
        }

        public bool NotFree { 
            get; 
            set; 
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Letter = ' ';
            IsSelectedByUser = false;
            NotFree = false;
        }
    }
}
