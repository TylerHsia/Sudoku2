using System;
using System.Text;

namespace SudokuLogic
{
    
    public class SudokuGrid
    {
        //sudokCell[,] grid;
        //public SudokuGrid()
        //{
        //    grid = new sudokCell[9, 9];
        //    for (int row = 0; row < 9; row++)
        //    {
        //        for (int column = 0; column < 9; column++)
        //        {
        //            grid[row, column] = new sudokCell();
        //        }
        //    }


        //}

        
        
        public sudokCell[,] cells;
        public sudokCell this[int row, int col]
        {
            get
            {
                return cells[row, col];
            }
            set
            {
                cells[row, col] = value;
            }
        }
        public SudokuGrid()
        {
            cells = new sudokCell[9, 9];
        }

        
        



        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (false)
                    {
                        sb.Append(this[row, column].ToString());
                    }
                    else
                    {
                        sb.Append(this[row, column].toStringWithoutCands());
                        //sb.Append("0");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}