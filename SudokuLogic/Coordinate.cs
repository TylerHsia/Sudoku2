using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuLogic
{
    public class Coordinate
    {
        private int row;
        private int column;

        public Coordinate(int rowInput, int columnInput)
        {
            row = rowInput;
            column = columnInput;
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;
            }
        }

        public override string ToString()
        {
            return "(" + IntToLetter(row) + ", " + (column + 1) + ")";
        }

        public string IntToLetter(int x)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabet.Substring(x, 1);
        }
    }
}
