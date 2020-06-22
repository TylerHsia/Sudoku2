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

        public Hint()
        { 
            text = "";
        }

        public override string ToString()
        {
            return text;
        }

        
    }
}
