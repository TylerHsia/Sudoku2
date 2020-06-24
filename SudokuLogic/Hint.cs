using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuLogic
{
    public class Hint
    {
        private  string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        private List<sudokCell> RelevantCells;
        //hint types: naked single, naked pair...

        private Coordinate hintCoord;

        public Hint()
        {
            hintCoord = new Coordinate(-1, -1);
            //hintCoord.Row = -1;
            //hintCoord.Column = -1;
            text = "";
        }

        public Hint(Coordinate newHintCoord)
        {
            hintCoord = newHintCoord;
        }

        public override string ToString()
        {
            return text;
        }

        public bool VoidCoord()
        {
            if (hintCoord.Column != -1 && hintCoord.Row != -1)
            {
                return false;
            }
            return true;
        }
        
    }
}
