using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SudokuLogic;

namespace TylerSudoku
{
    public partial class Form1 : Form
    {
        private TextBox infoBox;
        private TextBox HintDisplay = new TextBox();

        public Form1()
        {
            InitializeComponent();
            var sudokuSolver = new SudokuSolver();

            

            //initialize cells
            for(var i = 0; i < 9; i++)
            {
                for(var j = 0; j < 9; j++)
                {
                    var newCell = new MyCellBox(this, i, j);
                    newCell.Width = 30;
                    newCell.Height = 30;
                    newCell.Left = 100 + j * 33;
                    newCell.Top = 100 + i * 33;
                    this.Controls.Add(newCell);
                }
            }

            //initialize displays
            for(var i = 0; i < 9; i++)
            {
                var newCell = new MyDisplayBox(this);
                newCell.Width = 20;
                newCell.Height = 30;
                newCell.Left = 100 + i * 33 + 5;
                newCell.Top = 67 + 5;
                this.Controls.Add(newCell);
                newCell.Text = "" + (i + 1);

                String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                var newCell2 = new MyDisplayBox(this);
                newCell2.Width = 20;
                newCell2.Height = 30;
                newCell2.Top = 100 + i * 33;
                newCell2.Left = 67 + 5;
                this.Controls.Add(newCell2);
                newCell2.Text = alphabet.Substring(i, 1);
            }
            

            this.Height = 550;


            var newButton = new Button();
            this.Controls.Add(newButton);
            newButton.Text = "helloWorld";


            infoBox = new TextBox();
            this.Controls.Add(infoBox);
            infoBox.ReadOnly = true;
            infoBox.TabStop = false;
            infoBox.Left = 100;

            this.Controls.Add(HintDisplay);
            HintDisplay.Left = this.Width - 200;
            HintDisplay.Width = 180;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Tyler's Sudoku Helper";
            
            
        }   

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 100, 193, 392, 193);
            e.Graphics.DrawLine(pen, 20, 10, 300, 100);
            e.Graphics.DrawLine(pen, 20, 10, 300, 100);
            e.Graphics.DrawLine(pen, 20, 10, 300, 100);


            pen.Dispose();
        }

        

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        //clears all cells
        private void ClearAll_Click(object sender, EventArgs e)
        {
            var cellArray = this.Controls.OfType<MyCellBox>().ToList();
            for(int i = 0; i < cellArray.Count; i++)
            {
                cellArray[i].Text = "";
            }
        }

        private void solveGrid_Click(object sender, EventArgs e)
        {
            SudokuSolver sudokuSolver = new SudokuSolver();
            var cellArray = this.Controls.OfType<MyCellBox>().ToList();
            int[,] intSudokuGrid = new int[9, 9];
            for(int row = 0; row < 9; row++)
            {
                for(int column = 0; column < 9; column++)
                {
                    
                    
                    if(sudokuSolver.isDigit(cellArray[row * 9 + column].Text))
                    {
                        intSudokuGrid[row, column] = int.Parse(cellArray[row * 9 + column].Text);
                    }
                    //intSudokuGrid[row, column] = int.Parse(cellArray[row * 9 + column].Text);
                }
            }


            SudokuGrid mySudoku = new SudokuGrid();
            
            mySudoku = sudokuSolver.FromIntArray(intSudokuGrid);

            if (mySudoku.IsValid())
            {
                sudokuSolver.Solve(mySudoku, true);

                for (int i = 0; i < cellArray.Count; i++)
                {
                    cellArray[i].Text = "" + mySudoku[i / 9, i % 9].toStringWithoutCands();
                }
            }
            else
            {
                infoBox.Text = "the sudoku inputted was invalid";
            }
        }

        public SudokuGrid GetGrid()
        {
            var cellArray = this.Controls.OfType<MyCellBox>().ToList();
            SudokuSolver sudokuSolver = new SudokuSolver();
            int[,] intSudokuGrid = new int[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (sudokuSolver.isDigit(cellArray[row * 9 + column].Text))
                    {
                        intSudokuGrid[row, column] = int.Parse(cellArray[row * 9 + column].Text);
                    }
                    //intSudokuGrid[row, column] = int.Parse(cellArray[row * 9 + column].Text);
                }
            }
            SudokuGrid mySudoku = new SudokuGrid();
            mySudoku = sudokuSolver.FromIntArray(intSudokuGrid);
            return mySudoku;
        }

        private void Hint_Click(object sender, EventArgs e)
        {
            SudokuHinter sudokuHinter = new SudokuHinter();
            Hint myHint = new Hint();
            myHint.Text = "invalid";
            HintDisplay.Text = sudokuHinter.GetHint(GetGrid()).Text;
        }

        private void populate_Click(object sender, EventArgs e)
        {
            SudokuSolver sudokuSolver = new SudokuSolver();
            SudokuGrid mySudoku = sudokuSolver.FromIntArray(sudokuSolver.input(1));
            var cellArray = this.Controls.OfType<MyCellBox>().ToList();
            for(int i = 0; i < cellArray.Count; i++)
            {
                cellArray[i].Text = mySudoku[i / 9, i % 9].toStringWithoutCands();
            }
        }
    }


    public class MyCellBox : TextBox
    {
        private Form1 form1;
        private int column;
        private int row;
        //constructor 
        public MyCellBox(Form1 form1, int i, int j)
        {
            this.form1 = form1;
            this.column = j;
            this.row = i;
            this.MaxLength = 1;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            SudokuSolver sudokuSolver = new SudokuSolver();
            base.OnTextChanged(e);
            SudokuGrid mySudoku = CurrentGrid();
            
            if (sudokuSolver.InvalidCell(mySudoku, this.row, this.column))
            {
                this.BackColor = Color.Red;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }

        //returns current sudokuGrid
        public SudokuGrid CurrentGrid()
        {
            var cellArray = form1.Controls.OfType<MyCellBox>().ToList();
            //if invalid cell, make cell red
            //SudokuSolver sudokuSolver = new SudokuSolver();
            //var cellArray = this.Controls.OfType<MyCellBox>().ToList();
            SudokuSolver sudokuSolver = new SudokuSolver();
            int[,] intSudokuGrid = new int[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {


                    if (sudokuSolver.isDigit(cellArray[row * 9 + column].Text))
                    {
                        intSudokuGrid[row, column] = int.Parse(cellArray[row * 9 + column].Text);
                    }
                    //intSudokuGrid[row, column] = int.Parse(cellArray[row * 9 + column].Text);
                }
            }
            SudokuGrid mySudoku = new SudokuGrid();

            mySudoku = sudokuSolver.FromIntArray(intSudokuGrid);
            return mySudoku;
        }
        //key up event
        protected override void OnKeyUp(KeyEventArgs e)
        {
            var cellArray = form1.Controls.OfType<MyCellBox>().ToList();
            SudokuSolver sudokuSolver = new SudokuSolver();
            //tab for key up
            base.OnKeyUp(e);
            //if num 1 - 9
            if(e.KeyCode > Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode > Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                
                //if text is digit
                if (sudokuSolver.isDigit(this.Text))
                {
                    //if last cell, tab to top
                    if (column == 8 && row == 8)
                    {
                        //var array = form1.Controls.OfType<MyCellBox>().ToArray();
                        cellArray[0].Select();
                        this.SelectAll();
                    }
                    else
                    {
                        //tab forward
                        form1.GetNextControl(this, true).Select();
                        this.SelectAll();
                    }
                }
            }
            
            //if not a digit, delete
            if (!sudokuSolver.isDigit(this.Text) && this.Text.Length > 0)
            {
                //this.BackColor = Color.Red;
                this.Text = "";
            }
            if(sudokuSolver.isDigit(this.Text) || this.Text.Length == 0)
            {
                //this.BackColor = Color.White;
            }

            //arrow key
            //right
            if(e.KeyCode == Keys.Right)
            {
                if (cellArray.IndexOf(this) != 80)
                {
                    form1.GetNextControl(this, true).Select();
                    this.SelectAll();
                }
            }
            //left
            if(e.KeyCode == Keys.Left)
            {
                if(cellArray.IndexOf(this) != 0)
                {
                    form1.GetNextControl(this, false).Select();
                    this.SelectAll();
                }
                
            }
            //up
            if(e.KeyCode == Keys.Up)
            {
                var array = form1.Controls.OfType<MyCellBox>().ToList();
                int nextLocation = array.IndexOf(this) - 9;
                if(nextLocation > 0)
                {
                    array[nextLocation].Select();
                }
                this.SelectAll();
            }
            //down
            if (e.KeyCode == Keys.Down)
            {
                
                int nextLocation = cellArray.IndexOf(this) + 9;
                if (nextLocation < 81)
                {
                    cellArray[nextLocation].Select();
                }
                this.SelectAll();
            }
            
        }

        

        //select all text when click my textbox
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.SelectAll();
        }
    }

    public class MyDisplayBox : TextBox
    {
        private Form1 form1;
        //constructor
        public MyDisplayBox(Form1 form1)
        {
            this.form1 = form1;
            //this.ForeColor = Color.Red; //System.Drawing.SystemColors.MenuHighlight;
            this.ReadOnly = true;
            this.TabStop = false;
            this.HideSelection = true;
            this.Enabled = false;
            this.BackColor = Color.FromArgb(87, 160, 211);
            
        }
    }
}
