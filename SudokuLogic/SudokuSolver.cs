using java.lang;
using System;
using System.Collections.Generic;

namespace SudokuLogic
{

    public class SudokuSolver
    {
        public string ReturnsHello()
        {
            return "hello";
        }

        //eliminates by rook method
        public bool rookChecker(sudokCell[,] mySudoku)
        {
            bool checkerMethodOneWorks = false;

            //two for loops to go through each element in mySudoku
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    //if that element is solved
                    if (mySudoku[row,column].getSolved())
                    {
                        //for each other element in the row
                        for (int row2 = 0; row2 < 9; row2++)
                        {
                            //if the other element is not solved
                            if (!mySudoku[row2,column].getSolved())
                            {
                                //index is index of the solved value in not solved element 
                                int index = mySudoku[row2,column].indexOf(mySudoku[row,column].getVal());

                                //if not solved element has solved value 
                                if (index != -1)
                                {
                                    mySudoku[row2,column].remove(index);
                                    checkerMethodOneWorks = true;
                                    checkerMethodOneWorks = rookChecker(mySudoku);
                                    checkerMethodOneWorks = boxChecker(mySudoku);
                                }
                            }
                        }
                        //for each other element in the column
                        for (int column2 = 0; column2 < 9; column2++)
                        {
                            //if the other element is not solved
                            if (!mySudoku[row,column2].getSolved())
                            {
                                //index is index of the solved value in not solved element 
                                int index = mySudoku[row,column2].indexOf(mySudoku[row,column].getVal());

                                //if not solved element has solved value 
                                if (index != -1)
                                {
                                    mySudoku[row,column2].remove(index);

                                    checkerMethodOneWorks = true;
                                    checkerMethodOneWorks = rookChecker(mySudoku);
                                    checkerMethodOneWorks = boxChecker(mySudoku);
                                }
                            }
                        }
                    }
                }
            }
            return checkerMethodOneWorks;
        }

        //eliminates by checking 3 by 3 boxes
        public  bool boxChecker(sudokCell[,] mySudoku)
        {
            bool boxCheckerWorks = false;

            //for each solved cell in main array
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    //if that element is solved
                    if (mySudoku[row,column].getSolved())
                    {
                        int boxColumn = column / 3;
                        int boxRow = row / 3;

                        //for each row in the small box
                        for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                        {
                            //for each column in the small box
                            for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                            {
                                //if the other element is not solved
                                if (!mySudoku[row2,column2].getSolved())
                                {
                                    //index is index of the solved value in not solved element 
                                    int index = mySudoku[row2,column2].indexOf(mySudoku[row,column].getVal());

                                    if (index != -1)
                                    {
                                        mySudoku[row2,column2].remove(index);
                                        boxCheckerWorks = true;
                                        boxCheckerWorks = boxChecker(mySudoku);
                                        boxCheckerWorks = rookChecker(mySudoku);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return boxCheckerWorks;
        }

        //check if candidate is only candidate in one spot in a row or column
        public  bool onlyCandidateLeftRookChecker(sudokCell[,] mySudoku)
        {
            bool onlyCandidateLeftRookCheckerWorks = false;
            //check each column
            for (int column = 0; column < 9; column++)
            {
                //for each possible integer
                for (int i = 1; i <= 9; i++)
                {
                    int num = 0;
                    int index = -1;
                    //for each row
                    for (int row = 0; row < 9; row++)
                    {
                        //if not solved
                        if (!mySudoku[row,column].getSolved())
                        {

                            if (mySudoku[row,column].contains(i))
                            {
                                num++;
                                index = row;
                            }
                        }
                    }
                    if (num == 1)
                    {
                        mySudoku[index,column].solve(i);
                        rookChecker(mySudoku);
                        boxChecker(mySudoku);
                        onlyCandidateLeftRookCheckerWorks = true;
                        onlyCandidateLeftRookCheckerWorks = onlyCandidateLeftBoxChecker(mySudoku);
                    }
                }
            }

            //check each row 
            for (int row = 0; row < 9; row++)
            {
                //for each possible integer
                for (int i = 1; i <= 9; i++)
                {
                    int num = 0;
                    int index = -1;
                    //for each column
                    for (int column = 0; column < 9; column++)
                    {
                        //if not solved
                        if (!mySudoku[row,column].getSolved())
                        {
                            if (mySudoku[row,column].contains(i))
                            {
                                num++;
                                index = column;
                            }
                        }
                    }
                    if (num == 1)
                    {
                        mySudoku[row,index].solve(i);
                        onlyCandidateLeftRookCheckerWorks = true;
                        rookChecker(mySudoku);
                        boxChecker(mySudoku);
                        onlyCandidateLeftRookCheckerWorks = onlyCandidateLeftBoxChecker(mySudoku);
                    }
                }
            }
            return onlyCandidateLeftRookCheckerWorks;
        }



        //check if candidate is only candidate in one spot in a box
        public  bool onlyCandidateLeftBoxChecker(sudokCell[,] mySudoku)
        {
            bool onlyCandidateLeftBoxCheckerWorks = false;
            //for each box row
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                //for each box column
                for (int boxColumn = 0; boxColumn < 3; boxColumn++)
                {
                    //for each integer possible
                    for (int i = 1; i <= 9; i++)
                    {
                        int num = 0;
                        int rowIndex = -1;
                        int columnIndex = -1;
                        //for each row in the small box
                        for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                        {
                            //for each column in the small box
                            for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                            {
                                //if the element is not solved
                                if (!mySudoku[row2,column2].getSolved())
                                {
                                    //if it contains i
                                    if (mySudoku[row2,column2].contains(i))
                                    {
                                        num++;
                                        rowIndex = row2;
                                        columnIndex = column2;
                                    }
                                }
                            }
                        }
                        //if only one cell in the box has that candidate, solve it 
                        if (num == 1)
                        {
                            mySudoku[rowIndex,columnIndex].solve(i);
                            rookChecker(mySudoku);
                            boxChecker(mySudoku);
                            onlyCandidateLeftBoxCheckerWorks = true;
                            onlyCandidateLeftBoxCheckerWorks = onlyCandidateLeftBoxChecker(mySudoku);
                        }
                    }
                }
            }
            return onlyCandidateLeftBoxCheckerWorks;
        }

        //checks for 2 boxes that have only 2 candidates in a column or row, eliminates those candidates from that column OR row 
        public  bool nakedCandidateRookChecker(sudokCell[,] mySudoku)
        {
            bool candidatePairRookCheckerWorks = false;
            //two for loops to go through each element in mySudoku
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    //if that element is unsolved 
                    if (!mySudoku[row,column].getSolved())
                    {
                        //for each other element in the column
                        int numSame = 0;
                        List<int> rowVals = new List<int>();

                        for (int row2 = 0; row2 < 9; row2++)
                        {
                            //if the other element has same candidates
                            if (mySudoku[row2,column].samePossible(mySudoku[row,column]))
                            {
                                numSame++;
                                rowVals.Add(row2);
                            }
                        }
                        //if the number of cells with same possibles equals number of possibles per cell
                        if (numSame == mySudoku[row,column].size())
                        {
                            //for each other element in the column
                            for (int row2 = 0; row2 < 9; row2++)
                            {
                                if (!rowVals.Contains(row2))
                                {
                                    for (int possibleIndex = 0; possibleIndex < mySudoku[row,column].size(); possibleIndex++)
                                    {
                                        if (mySudoku[row2,column].indexOf(mySudoku[row,column].getVal(possibleIndex)) != -1)
                                        {
                                            mySudoku[row2,column].remove(mySudoku[row2,column].indexOf(mySudoku[row,column].getVal(possibleIndex)));
                                        }
                                    }
                                }
                            }
                            candidatePairRookCheckerWorks = true;
                            candidatePairRookCheckerWorks = rookChecker(mySudoku);
                            candidatePairRookCheckerWorks = boxChecker(mySudoku);
                        }


                        //for each other element in the row
                        List<int> columnVals = new List<int>();
                        numSame = 0;
                        for (int column2 = 0; column2 < 9; column2++)
                        {
                            //if the other element is not solved 
                            if (mySudoku[row,column2].samePossible(mySudoku[row,column2]))
                            {
                                numSame++;
                                columnVals.Add(column2);
                            }
                        }
                        //if the number of cells with same possibles equals number of possibles per cell
                        if (numSame == mySudoku[row,column].size())
                        {
                            //for each other element in that row
                            for (int column2 = 0; column2 < 9; column2++)
                            {
                                if (!columnVals.Contains(column2))
                                {
                                    for (int possibleIndex = 0; possibleIndex < mySudoku[row,column].size(); possibleIndex++)
                                    {
                                        if (mySudoku[row,column2].indexOf(mySudoku[row,column].getVal(possibleIndex)) != -1)
                                        {
                                            mySudoku[row,column2].remove(mySudoku[row,column2].indexOf(mySudoku[row,column].getVal(possibleIndex)));
                                        }
                                    }
                                }
                            }
                            candidatePairRookCheckerWorks = true;
                            candidatePairRookCheckerWorks = rookChecker(mySudoku);
                            candidatePairRookCheckerWorks = boxChecker(mySudoku);
                        }
                    }
                }
            }
            return candidatePairRookCheckerWorks;
        }

        //checks for 2 boxes that have only 2 candidates in a box, eliminates those candidates from that box 
        public bool nakedCandidateBoxChecker(sudokCell[,] mySudoku)
        {
            bool candidatePairBoxCheckerWorks = false;
            //two for loops to go through each element in mySudoku
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    //if that element is unsolved 
                    if (!mySudoku[row,column].getSolved())
                    {
                        int numSame = 0;
                        List<int> rowVals = new List<int>();
                        List<int> columnVals = new List<int>();

                        int boxRow = row / 3;
                        int boxColumn = column / 3;


                        //for each row in the small box
                        for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                        {
                            //for each column in the small box
                            for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                            {
                                //if the element is not solved
                                if (!mySudoku[row2,column2].getSolved())
                                {
                                    //if it has same possibles
                                    if (mySudoku[row2,column2].samePossible(mySudoku[row,column]))
                                    {
                                        numSame++;
                                        rowVals.Add(row2);
                                        columnVals.Add(column2);
                                    }
                                }
                            }
                        }

                        //if the number of cells with same possibles equals number of possibles per cell
                        if (numSame == mySudoku[row,column].size())
                        {

                            //for each other element in that box
                            //for each row in the small box
                            for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                            {
                                //for each column in the small box
                                for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                                {
                                    //if the box was not one of the ones that had same pair
                                    if (!columnVals.Contains(column2) || !rowVals.Contains(row2))
                                    {

                                        for (int possibleIndex = 0; possibleIndex < mySudoku[row,column].size(); possibleIndex++)
                                        {
                                            //if the other cell Contains that possibility
                                            if (mySudoku[row2,column2].contains(mySudoku[row,column].getVal(possibleIndex)))
                                            {
                                                //remove that possibility from the other cell
                                                //printBoard(mySudoku, true);
                                                mySudoku[row2,column2].remove(mySudoku[row2,column2].indexOf(mySudoku[row,column].getVal(possibleIndex)));
                                            }
                                        }
                                    }
                                }
                            }
                            candidatePairBoxCheckerWorks = true;
                            candidatePairBoxCheckerWorks = rookChecker(mySudoku);
                            candidatePairBoxCheckerWorks = boxChecker(mySudoku);
                        }
                    }
                }
            }
            return candidatePairBoxCheckerWorks;
        }

        //checks for hidden candidate sets and removes candidates from those 
        public bool hiddenCandidatePairChecker(sudokCell[,] mySudoku)
        {
            bool hiddenCandidatePairCheckerWorks = false;
            //find in a row
            for (int row = 0; row < 9; row++)
            {
                //for each candidate i
                for (int i = 1; i <= 9; i++)
                {
                    //num is number of appearances of that candidate
                    int num = 0;
                    int iColumnCoord1 = -1;
                    int iColumnCoord2 = -1;

                    for (int column = 0; column < 9; column++)
                    {
                        //if that cell contains the candidate
                        if (mySudoku[row,column].contains(i))
                        {
                            iColumnCoord2 = iColumnCoord1;
                            iColumnCoord1 = column;
                            num++;
                        }
                    }
                    //if 2 possibles for the first candidate
                    if (num == 2)
                    {
                        //find second candidate
                        for (int k = i + 1; k <= 9; k++)
                        {
                            //num for second pair
                            int numK = 0;
                            int kColumnCoord1 = -1;
                            int kColumnCoord2 = -1;
                            for (int column = 0; column < 9; column++)
                            {
                                //if that cell contains the candidate
                                if (mySudoku[row,column].contains(k))
                                {
                                    kColumnCoord2 = kColumnCoord1;
                                    kColumnCoord1 = column;
                                    numK++;
                                }
                            }
                            //if pair for second candidate
                            if (numK == 2)
                            {
                                //if coord of both pairs are same
                                if (kColumnCoord1 == iColumnCoord1 && kColumnCoord2 == iColumnCoord2)
                                {
                                    //remove all other candidates from both cells
                                    for (int j = 1; j <= 9; j++)
                                    {
                                        //i and k should be the two candidates
                                        if (j != i && j != k)
                                        {
                                            //removal
                                            if (mySudoku[row,kColumnCoord1].contains(j))
                                            {
                                                mySudoku[row,kColumnCoord1].remove(mySudoku[row,kColumnCoord1].indexOf(j));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                            if (mySudoku[row,kColumnCoord2].contains(j))
                                            {
                                                mySudoku[row,kColumnCoord2].remove(mySudoku[row,kColumnCoord2].indexOf(j));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //find in a column
            for (int column = 0; column < 9; column++)
            {
                //for each candidate i
                for (int i = 1; i <= 9; i++)
                {
                    //num is number of appearances of that candidate
                    int num = 0;
                    int iRowCoord1 = -1;
                    int iRowCoord2 = -1;

                    for (int row = 0; row < 9; row++)
                    {
                        //if that cell contains the candidate
                        if (mySudoku[row,column].contains(i))
                        {
                            iRowCoord2 = iRowCoord1;
                            iRowCoord1 = row;
                            num++;
                        }
                    }
                    //if 2 possibles for the first candidate
                    if (num == 2)
                    {
                        //find second candidate
                        for (int k = i + 1; k <= 9; k++)
                        {
                            //num for second pair
                            int numK = 0;
                            int kRowCoord1 = -1;
                            int kRowCoord2 = -1;
                            for (int row = 0; row < 9; row++)
                            {
                                //if that cell contains the candidate
                                if (mySudoku[row,column].contains(k))
                                {
                                    kRowCoord2 = kRowCoord1;
                                    kRowCoord1 = row;
                                    numK++;
                                }
                            }
                            //if pair for second candidate
                            if (numK == 2)
                            {
                                //if coord of both pairs are same
                                if (kRowCoord1 == iRowCoord1 && kRowCoord2 == iRowCoord2)
                                {
                                    //remove all other candidates from both cells
                                    for (int j = 1; j <= 9; j++)
                                    {
                                        //i and k should be the two candidates remaining
                                        if (j != i && j != k)
                                        {
                                            //removal
                                            if (mySudoku[kRowCoord1,column].contains(j))
                                            {
                                                mySudoku[kRowCoord1,column].remove(mySudoku[kRowCoord1,column].indexOf(j));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                            if (mySudoku[kRowCoord2,column].contains(j))
                                            {
                                                mySudoku[kRowCoord2,column].remove(mySudoku[kRowCoord2,column].indexOf(j));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //find in box
            //for each big box
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxColumn = 0; boxColumn < 3; boxColumn++)
                {
                    //for each candidate i
                    for (int i = 1; i <= 9; i++)
                    {
                        //num is number of appearances of that candidate
                        int num = 0;
                        int iRowCoord1 = -1;
                        int iRowCoord2 = -1;
                        int iColumnCoord1 = -1;
                        int iColumnCoord2 = -1;

                        //for each row in the small box
                        for (int row = boxRow * 3; row < boxRow * 3 + 3; row++)
                        {
                            //for each column in the small box
                            for (int column = boxColumn * 3; column < boxColumn * 3 + 3; column++)
                            {
                                //if that cell contains the candidate
                                if (mySudoku[row,column].contains(i))
                                {
                                    iRowCoord2 = iRowCoord1;
                                    iRowCoord1 = row;
                                    iColumnCoord2 = iColumnCoord1;
                                    iColumnCoord1 = column;
                                    num++;
                                }
                            }
                        }
                        //if 2 possibles for the first candidate
                        if (num == 2)
                        {
                            //find second candidate
                            for (int k = i + 1; k <= 9; k++)
                            {
                                //num for second pair
                                int numK = 0;
                                int kRowCoord1 = -1;
                                int kRowCoord2 = -1;
                                int kColumnCoord1 = -1;
                                int kColumnCoord2 = -1;

                                //for each row in the small box
                                for (int row = boxRow * 3; row < boxRow * 3 + 3; row++)
                                {
                                    //for each column in the small box
                                    for (int column = boxColumn * 3; column < boxColumn * 3 + 3; column++)
                                    {
                                        //if that cell contains the candidate
                                        if (mySudoku[row,column].contains(k))
                                        {
                                            kRowCoord2 = iRowCoord1;
                                            kRowCoord1 = row;
                                            kColumnCoord2 = iColumnCoord1;
                                            kColumnCoord1 = column;
                                            numK++;
                                        }
                                    }
                                }
                                //if pair for second candidate
                                if (numK == 2)
                                {
                                    //if coord of both pairs are same
                                    if (kRowCoord1 == iRowCoord1 && kRowCoord2 == iRowCoord2 && kColumnCoord1 == iColumnCoord1 && kColumnCoord2 == iColumnCoord2)
                                    {
                                        //remove all other candidates from both cells
                                        for (int j = 1; j <= 9; j++)
                                        {
                                            //i and k should be the two candidates
                                            if (j != i && j != k)
                                            {
                                                //removal
                                                if (mySudoku[kRowCoord1,kColumnCoord1].contains(j))
                                                {
                                                    mySudoku[kRowCoord1,kColumnCoord1].remove(mySudoku[kRowCoord1,kColumnCoord1].indexOf(j));
                                                    rookChecker(mySudoku);
                                                    boxChecker(mySudoku);
                                                }
                                                if (mySudoku[kRowCoord2,kColumnCoord2].contains(j))
                                                {
                                                    mySudoku[kRowCoord2,kColumnCoord2].remove(mySudoku[kRowCoord2,kColumnCoord2].indexOf(j));
                                                    rookChecker(mySudoku);
                                                    boxChecker(mySudoku);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return hiddenCandidatePairCheckerWorks;
        }


        //method for hiddenCandidateChecker, takes in array list, finds the hidden candidates, returns them in an arraylist
        public List<int> findHiddenCandidatesPair(List<sudokCell> candidateSet)
        {
            return new List<int>();
        }

        //method for candidate lines (only place in a box where candidate must go is in a line, eliminate candidate from that line outside the box)
        public bool candidateLinesChecker(sudokCell[,] mySudoku)
        {
            bool candidateLinesCheckerWorks = false;
            //for each big box
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxColumn = 0; boxColumn < 3; boxColumn++)
                {
                    //for each candidate
                    for (int i = 1; i <= 9; i++)
                    {
                        List<int> rowVals = new List<int>();
                        List<int> columnVals = new List<int>();
                        int numHasCandidate = 0;
                        bool removedACandidate = false;

                        //for each row in the small box
                        for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                        {
                            //for each column in the small box
                            for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                            {
                                //if the element is not solved
                                if (!mySudoku[row2,column2].getSolved())
                                {
                                    //if it contains the candidate integer
                                    if (mySudoku[row2,column2].contains(i))
                                    {
                                        numHasCandidate++;
                                        rowVals.Add(row2);
                                        columnVals.Add(column2);
                                    }
                                }
                            }
                        }
                        //if number of cells that has that candidate in a box is 3 or lower
                        if (numHasCandidate <= 3)
                        {
                            //if all in same row
                            bool allInSameRow = false;
                            //if 2 in same row
                            if (rowVals.Count == 2)
                            {
                                if (rowVals[0] == rowVals[1])
                                {
                                    allInSameRow = true;
                                }
                            }
                            //if 3 in same row
                            if (rowVals.Count == 3)
                            {
                                if (rowVals[0] == rowVals[1] && rowVals[1] == rowVals[2])
                                {
                                    allInSameRow = true;
                                }
                            }
                            if (allInSameRow)
                            {
                                //eliminate values along that row
                                for (int column2 = 0; column2 < 9; column2++)
                                {
                                    //if outside of the box (not one of the previously selected)
                                    if (!columnVals.Contains(column2))
                                    {
                                        //if it contains the candidate
                                        if (mySudoku[rowVals[0],column2].contains(i))
                                        {
                                            //remove that candidate
                                            mySudoku[rowVals[0],column2].remove(mySudoku[rowVals[0],column2].indexOf(i));
                                            removedACandidate = true;
                                        }
                                    }
                                }
                            }



                            //if all in same column
                            bool allInSameColumn = false;
                            //if 2 in same column
                            if (columnVals.Count == 2)
                            {
                                if (columnVals[0] == columnVals[1])
                                {
                                    allInSameColumn = true;
                                }
                            }
                            //if 3 in same column
                            if (columnVals.Count == 3)
                            {
                                if (columnVals[0] == columnVals[1] && columnVals[1] == columnVals[2])
                                {
                                    allInSameColumn = true;
                                }
                            }
                            if (allInSameColumn)
                            {
                                //eliminate values along that column
                                for (int row2 = 0; row2 < 9; row2++)
                                {
                                    //if outside of the box (not one of the previously selected)
                                    if (!rowVals.Contains(row2))
                                    {
                                        //if it contains the candidate
                                        if (mySudoku[row2,columnVals[0]].contains(i))
                                        {
                                            //remove that candidate
                                            mySudoku[row2,columnVals[0]].remove(mySudoku[row2,columnVals[0]].indexOf(i));
                                            removedACandidate = true;
                                        }
                                    }
                                }
                            }
                            //if a candidate was removed, run through box and rook checker
                            if (removedACandidate)
                            {
                                candidateLinesCheckerWorks = true;
                                candidateLinesCheckerWorks = boxChecker(mySudoku);
                                candidateLinesCheckerWorks = rookChecker(mySudoku);
                            }
                        }
                    }
                }
            }
            return candidateLinesCheckerWorks;
        }

        public bool pointingPairRookToBoxChecker(sudokCell[,] mySudoku)
        {
            bool pointingPairRookToBoxWorks = false;
            //check rows
            for (int row = 0; row < 9; row++)
            {
                //for candidate i
                for (int i = 1; i <= 9; i++)
                {
                    int num = 0;
                    int columnCoord1 = -1;
                    int columnCoord2 = -1;
                    int columnCoord3 = -1;

                    for (int column = 0; column < 9; column++)
                    {
                        if (mySudoku[row,column].contains(i))
                        {
                            columnCoord1 = columnCoord2;
                            columnCoord2 = columnCoord3;
                            columnCoord3 = column;
                            num++;
                        }
                    }
                    //if the number of cells in that line with that candidate is 3
                    if (num == 3)
                    {
                        //if cells in the same box
                        if (columnCoord1 / 3 == columnCoord2 / 3 && columnCoord2 / 3 == columnCoord3 / 3)
                        {
                            //remove i from rest of box
                            int boxRow = row / 3;
                            int boxColumn = columnCoord1 / 3;
                            //for each row in the small box
                            for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                            {
                                //for each column in the small box
                                for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                                {
                                    //if none of the three 
                                    if (!(columnCoord1 == column2 && row == row2) && !(columnCoord2 == column2 && row == row2) && !(columnCoord3 == column2 && row == row2))
                                    {
                                        if (!mySudoku[row2,column2].getSolved())
                                        {
                                            //remove i
                                            if (mySudoku[row2,column2].contains(i))
                                            {
                                                mySudoku[row2,column2].remove(mySudoku[row2,column2].indexOf(i));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //if the number of cells in that line with that candidate is 2
                    if (num == 2)
                    {
                        //if cells in the same box
                        if (columnCoord2 / 3 == columnCoord3 / 3)
                        {
                            //remove i from rest of box
                            int boxRow = row / 3;
                            int boxColumn = columnCoord2 / 3;
                            //for each row in the small box
                            for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                            {
                                //for each column in the small box
                                for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                                {
                                    //if none of the two
                                    if (!(columnCoord2 == column2 && row == row2) && !(columnCoord3 == column2 && row == row2))
                                    {
                                        //if other cell is not solved
                                        if (!mySudoku[row2,column2].getSolved())
                                        {
                                            //remove i
                                            if (mySudoku[row2,column2].contains(i))
                                            {
                                                mySudoku[row2,column2].remove(mySudoku[row2,column2].indexOf(i));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //check columns
            for (int column = 0; column < 9; column++)
            {
                //for candidate i
                for (int i = 1; i <= 9; i++)
                {
                    int num = 0;
                    int rowCoord1 = -1;
                    int rowCoord2 = -1;
                    int rowCoord3 = -1;

                    for (int row = 0; row < 9; row++)
                    {
                        if (mySudoku[row,column].contains(i))
                        {
                            rowCoord1 = rowCoord2;
                            rowCoord2 = rowCoord3;
                            rowCoord3 = row;
                            num++;
                        }
                    }
                    //if the number of cells in that line with that candidate is 3
                    if (num == 3)
                    {
                        //if cells in the same box
                        if (rowCoord1 / 3 == rowCoord2 / 3 && rowCoord2 / 3 == rowCoord3 / 3)
                        {
                            //remove i from rest of box
                            int boxRow = rowCoord1 / 3;
                            int boxColumn = column / 3;
                            //for each row in the small box
                            for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                            {
                                //for each column in the small box
                                for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                                {
                                    //if none of the three 
                                    if (!(rowCoord1 == row2 && column == column2) && !(rowCoord2 == row2 && column == column2) && !(rowCoord3 == row2 && column == column2))
                                    {
                                        if (!mySudoku[row2,column2].getSolved())
                                        {
                                            //remove i
                                            if (mySudoku[row2,column2].contains(i))
                                            {
                                                mySudoku[row2,column2].remove(mySudoku[row2,column2].indexOf(i));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //if the number of cells in that line with that candidate is 2
                    if (num == 2)
                    {
                        //if cells in the same box
                        if (rowCoord2 / 3 == rowCoord3 / 3)
                        {
                            //remove i from rest of box
                            int boxRow = rowCoord2 / 3;
                            int boxColumn = column / 3;
                            //for each row in the small box
                            for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                            {
                                //for each column in the small box
                                for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                                {
                                    //if none of the two
                                    if (!(rowCoord2 == row2 && column == column2) && !(rowCoord3 == row2 && column == column2))
                                    {
                                        //if other cell is not solved
                                        if (!mySudoku[row2,column2].getSolved())
                                        {
                                            //remove i
                                            if (mySudoku[row2,column2].contains(i))
                                            {
                                                mySudoku[row2,column2].remove(mySudoku[row2,column2].indexOf(i));
                                                rookChecker(mySudoku);
                                                boxChecker(mySudoku);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            return pointingPairRookToBoxWorks;
        }

        //forcing chains checker
        public bool forcingChainsChecker(sudokCell[,] mySudoku)
        {
            //System.out.println("I was called");
            bool forcingChainsCheckerWorks = false;
            //for each unsolved cell
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (!mySudoku[row,column].getSolved())
                    {
                        //setup 
                        int numCands = mySudoku[row,column].getPossibles().Count;
                        bool[,] sameSolved = new bool[9,9];
                        for (int solvedRow = 0; solvedRow < 9; solvedRow++)
                        {
                            for (int solvedColumn = 0; solvedColumn < 9; solvedColumn++)
                            {
                                sameSolved[solvedRow,solvedColumn] = true;
                                if (mySudoku[solvedRow,solvedColumn].getSolved())
                                {
                                    sameSolved[solvedRow,solvedColumn] = false;
                                }
                            }
                        }

                        //first guess
                        sudokCell[,] copy1 = Copy(mySudoku);
                        copy1[row,column].solve(mySudoku[row,column].getPossibles()[0]);
                        if (solveForForcingChains(copy1))
                        {
                            mySudoku[row,column].remove(mySudoku[row,column].indexOf(mySudoku[row,column].getPossibles()[0]));
                            return true;
                        }
                        if (numUnsolved(copy1) == 0 && !solved(copy1, false))
                        {
                            mySudoku[row,column].remove(mySudoku[row,column].indexOf(mySudoku[row,column].getPossibles()[0]));
                            return true;
                        }
                        for (int row2 = 0; row2 < 9; row2++)
                        {
                            for (int column2 = 0; column2 < 9; column2++)
                            {
                                if (!copy1[row2,column2].getSolved())
                                {
                                    sameSolved[row2,column2] = false;
                                }
                            }
                        }


                        //all other guesses 
                        for (int candidateIndex = 1; candidateIndex < numCands; candidateIndex++)
                        {
                            sudokCell[,] copy = Copy(mySudoku);
                            copy[row,column].solve(mySudoku[row,column].getPossibles()[candidateIndex]);
                            if (solveForForcingChains(copy))
                            {
                                mySudoku[row,column].remove(mySudoku[row,column].indexOf(mySudoku[row,column].getPossibles()[candidateIndex]));
                                return true;
                            }
                            if (numUnsolved(copy) == 0 && !solved(copy, false))
                            {
                                mySudoku[row,column].remove(mySudoku[row,column].indexOf(mySudoku[row,column].getPossibles()[candidateIndex]));
                                return true;
                            }

                            for (int row2 = 0; row2 < 9; row2++)
                            {
                                for (int column2 = 0; column2 < 9; column2++)
                                {
                                    if (!mySudoku[row2,column2].getSolved())
                                    {
                                        if (copy[row2,column2].getSolved())
                                        {
                                            if (!copy[row2,column2].samePossible(copy1[row2,column2]))
                                            {
                                                sameSolved[row2,column2] = false;
                                            }
                                        }
                                    }
                                    if (!copy[row2,column2].getSolved())
                                    {
                                        sameSolved[row2,column2] = false;
                                    }
                                }
                            }
                        }
                        for (int row2 = 0; row2 < 9; row2++)
                        {
                            for (int column2 = 0; column2 < 9; column2++)
                            {
                                if (!mySudoku[row2,column2].getSolved())
                                {
                                    if (sameSolved[row2,column2])
                                    {
                                        if (copy1[row2,column2].getSolved())
                                        {
                                            mySudoku[row2,column2].solve(copy1[row2,column2].getVal());
                                            forcingChainsCheckerWorks = true;
                                            //System.out.println("I did it");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return forcingChainsCheckerWorks;
        }

        //solve method for hidden candidate copies 
        public bool solveForForcingChains(sudokCell[,] mySudoku)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    rookChecker(mySudoku);
                    boxChecker(mySudoku);
                    onlyCandidateLeftRookChecker(mySudoku);
                    onlyCandidateLeftBoxChecker(mySudoku);
                    nakedCandidateRookChecker(mySudoku);
                    nakedCandidateBoxChecker(mySudoku);
                    candidateLinesChecker(mySudoku);
                    hiddenCandidatePairChecker(mySudoku);
                    pointingPairRookToBoxChecker(mySudoku);

                    //System.out.println("HeHe");
                }
            }
            //return if made a remove all error
            catch (System.Exception)
            {
                return true;
            }
            //return false;

            bool forcingChainsCheckerWorks = false;

            forcingChainsCheckerWorks = forcingChainsChecker(mySudoku);

            if (forcingChainsCheckerWorks)
            {
                solveForForcingChains(mySudoku);
            }
            return false;
        }

        //make a copy of values
        public sudokCell[,] Copy(sudokCell[,] mySudoku)
        {
            sudokCell[,] mySudoku2 = new sudokCell[9,9];
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    sudokCell x = mySudoku[row,column];
                    mySudoku2[row,column] = new sudokCell(x);
                }
            }
            return mySudoku2;
        }


        //check if the sudoku is solved
        public bool solved(sudokCell[,] mySudoku, bool printChecks)
        {
            //two for loops to go through each row, check adds to 45
            for (int row = 0; row < 9; row++)
            {
                int numTotal = 0;
                for (int column = 0; column < 9; column++)
                {
                    numTotal += mySudoku[row,column].getVal();
                }
                if (numTotal != 45)
                {
                    return false;
                }
            }
            //if(printChecks) System.out.println("Rows add up");        
            //two for loops to go through each column, check adds to 45
            for (int column = 0; column < 9; column++)
            {
                int numTotal = 0;
                for (int row = 0; row < 9; row++)
                {
                    numTotal += mySudoku[row,column].getVal();
                }
                if (numTotal != 45)
                {
                    return false;
                }
            }
            //if(printChecks) System.out.println("Columns add up");

            //check each box, check adds to 45
            //for each box row
            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                //for each box column
                for (int boxColumn = 0; boxColumn < 3; boxColumn++)
                {
                    int numTotal = 0;
                    //for each row in the small box
                    for (int row2 = boxRow * 3; row2 < boxRow * 3 + 3; row2++)
                    {
                        //for each column in the small box
                        for (int column2 = boxColumn * 3; column2 < boxColumn * 3 + 3; column2++)
                        {
                            numTotal += mySudoku[row2,column2].getVal();
                        }
                    }
                    if (numTotal != 45)
                    {
                        return false;
                    }
                }
            }
            //if(printChecks) System.out.println("Boxes add up");
            return true;
        }

        //check how many boxes remain unsolved
        public int numUnsolved(sudokCell[,] mySudoku)
        {
            int numUnsolvedB = 81;
            //for each solved cell in main array
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    //if that element is solved
                    if (mySudoku[row,column].getSolved())
                    {
                        numUnsolvedB--;
                    }
                }
            }
            return numUnsolvedB;
        }


    }


    public class sudokCell
    {
        //constructors 
        private List<int> possibles = new List<int>();
        private bool solved = false;
        public sudokCell(int value)
        {
            if (value == -1 || value == 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    possibles.Add(i);
                }
            }
            else
            {
                possibles.Add(value);
                solved = true;
            }


            


        }






        public sudokCell()
        {
            for (int i = 1; i <= 9; i++)
            {
                possibles.Add(i);
            }
        }
        public sudokCell(sudokCell obj)
        {
            obj = new sudokCell(obj.getPossibles(), obj.getSolved());
        }
        public sudokCell(List<int> possibles, bool solved)
        {
            for (int i = 0; i < possibles.Count; i++)
            {
                this.possibles.Add(possibles[i]);
            }
            this.solved = solved;
        }

        public int indexOf(int val)
        {
            return possibles.IndexOf(val);
        }
        //removes a candidate 
        public bool remove(int toRemove)
        {

            //to be commented out
            bool toPrint = false;




            possibles.RemoveAt(toRemove);
            if (possibles.Count == 1)
            {
                solved = true;
            }

            //to be commented out
            if (possibles.Count == 0)
            {
                //System.out.println("Removed all possibilities, my null pointer exception");
                toPrint = true;
                
                throw new NullPointerException("Cell has zero candidates");
            }
            return toPrint;
        }

        //removes all other candidates
        public void solve(int solution)
        {
            while (possibles.Count > 0)
            {
                possibles.RemoveAt(0);
            }
            possibles.Add(solution);
            solved = true;
        }

        //checks two cells for the same possibilities
        public bool samePossible(sudokCell other)
        {
            if (this.possibles.Count != other.possibles.Count)
            {
                return false;
            }
            for (int i = 0; i < this.possibles.Count; i++)
            {
                if (this.possibles[i] != other.possibles[i])
                {
                    return false;
                }
            }
            return true;
        }

        //toString
        public string toStringWithoutCands()
        {
            string toReturn = "";
            if (solved)
            {
                return toReturn + possibles[0] + " ";
            }
            else
            {
                return "  ";
            }
        }

        public string toString()
        {
            string toReturn = "";
            if (solved)
            {
                return toReturn + possibles[0] + "\t";
            }
            else
            {
                for (int i = 0; i < possibles.Count; i++)
                {
                    toReturn = toReturn + possibles[i];
                }
                return toReturn + "\t";
            }
        }

        //contains
        public bool contains(int x)
        {
            return possibles.Contains(x);
        }
        //accessors
        public bool getSolved()
        {
            return solved;
        }

        public int getVal()
        {
            if (solved)
            {
                return possibles[0];
            }
            else
            {
                return -1;
            }
        }

        public int getVal(int index)
        {
            return possibles[index];
        }

        public List<int> getPossibles()
        {
            return possibles;
        }
        public List<int> getPossibles(sudokCell obj)
        {
            return obj.getPossibles();
        }

        public int size()
        {
            return possibles.Count;
        }
    }

    public class Class1
    {
        public string HelloTest()
        {
            return $"the date is {DateTime.Now}";
        }
    }


    
}
