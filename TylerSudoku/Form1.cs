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
        public Form1()
        {
            InitializeComponent();
            var x = new SudokuLogic.Class1();
            var y = x.HelloTest();
            var sudokuSolver = new SudokuSolver();

            this.textBox1.Text = sudokuSolver.ReturnsHello();

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
    }


    public class MyCellBox : TextBox
    {
        private Form1 form1;
        private int xCoord;
        private int yCoord;
        //constructor 
        public MyCellBox(Form1 form1, int i, int j)
        {
            this.form1 = form1;
            this.xCoord = j;
            this.yCoord = i;
            this.MaxLength = 1;
        }

        //key up event
        protected override void OnKeyUp(KeyEventArgs e)
        {
            var cellArray = form1.Controls.OfType<MyCellBox>().ToList();

            //tab for key up
            base.OnKeyUp(e);
            //if num 1 - 9
            if(e.KeyCode > Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode > Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                //if text is digit
                if (isDigit(this.Text))
                {
                    //if last cell, tab to top
                    if (xCoord == 8 && yCoord == 8)
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
            if (!isDigit(this.Text) && this.Text.Length > 0)
            {
                //this.BackColor = Color.Red;
                this.Text = "";
            }
            if(isDigit(this.Text) || this.Text.Length == 0)
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

        //is a sudoku digit helper method
        public bool isDigit(String input)
        {
            for(int i = 1; i<= 9; i++)
            {
                if(input.Equals("" + i))
                {
                    return true;
                }
            }            
            return false;
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
