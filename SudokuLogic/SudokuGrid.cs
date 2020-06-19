using System;
using System.Data.Common;
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
            sb.AppendLine();
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

        public bool IsValid()
        {
            SudokuSolver sudokuSolver = new SudokuSolver();
            
            if(this.NumSolved() < 16)
            {
                //return false;
            }

            SolveForIsValid();
            //if simple solve, return is valid
            if (sudokuSolver.solved(this))
            {
                return true;
            }

            bool solvedOne = false;

            SudokuGrid copy1 = sudokuSolver.Copy(this);
            sudokuSolver.bruteForceSolver(copy1);

            if (!sudokuSolver.solved(copy1))
            {
                return false;
            }


            //else, guess all possibles and brute force solve. if multiple solutions, return false
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    //if unsolved
                    if (!this[row, column].getSolved())
                    {
                        
                        for(int i = 0; i < this[row, column].getPossibles().Count; i++)
                        {
                            SudokuGrid copy = sudokuSolver.Copy(this);
                            //solve to the index of the guess
                            copy[row, column].solve(this[row, column].getPossibles()[i]);

                            //brute force it
                            sudokuSolver.bruteForceSolver(copy);
                            bool solvedThisOne = sudokuSolver.solved(copy);
                            //if this one and another were solved, invalid for too many
                            if(solvedThisOne && solvedOne)
                            {
                                return false;
                            }
                            //if this one is solved, solved one
                            if (solvedThisOne && !solvedOne)
                            {
                                solvedOne = true;
                            }
                            
                        }
                    }
                }
            }
            //if none of the guesses worked, return false
            return false;
        }

        private int NumSolved()
        {
            int numSolved = 0;

            for(int row = 0; row < 9; row++)
            {
                for(int column = 0; column < 9; column++)
                {
                    if(this[row, column].getSolved())
                    {
                        numSolved++;
                    }
                }
            }
            return numSolved;
        }

        public void SolveForIsValid()
        {
            SudokuSolver sudokuSolver = new SudokuSolver();
            for (int i = 0; i < 10; i++)
            {
                sudokuSolver.RookChecker(this);
                sudokuSolver.BoxChecker(this);
                sudokuSolver.OnlyCandidateLeftRookChecker(this);
                sudokuSolver.OnlyCandidateLeftBoxChecker(this);
                sudokuSolver.NakedCandidateRookChecker(this);
                sudokuSolver.NakedCandidateBoxChecker(this);
                sudokuSolver.CandidateLinesChecker(this);
            }
            

        }
    }
}