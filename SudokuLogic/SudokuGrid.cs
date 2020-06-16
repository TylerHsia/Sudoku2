using System.Text;

namespace SudokuLogic
{
    
    class SudokuGrid
    {
        sudokCell[,] grid;
        public SudokuGrid()
        {
            grid = new sudokCell[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    grid[row, column] = new sudokCell();
                }
            }
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
                        sb.Append(grid[row, column].toString());
                    }
                    else
                    {
                        //sb.Append(grid[row, column].toStringWithoutCands());
                        sb.Append("0");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}