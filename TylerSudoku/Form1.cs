using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TylerSudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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


            this.Height = 550;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Tyler's Sudoku Helper";
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
            if(e.KeyCode > Keys.D0 && e.KeyCode <= Keys.D9)
            {
                if (this.Text.Length > 0 && isDigit(this.Text))
                {
                    if (xCoord == 8 && yCoord == 8)
                    {
                        //var array = form1.Controls.OfType<MyCellBox>().ToArray();
                        cellArray[0].Select();
                        this.SelectAll();
                    }
                    else
                    {
                        form1.GetNextControl(this, true).Select();
                        this.SelectAll();
                    }
                }
            }

            //if not a digit, delete
            if (!isDigit(this.Text) && this.Text.Length > 0)
            {
                this.BackColor = Color.Red;
                this.Text = "";
            }
            if(isDigit(this.Text) || this.Text.Length == 0)
            {
                this.BackColor = Color.White;
            }

            //arrow keys
            if(e.KeyCode == Keys.Right)
            {
                if (cellArray.IndexOf(this) != 80)
                {
                    form1.GetNextControl(this, true).Select();
                    this.SelectAll();
                }
            }
            if(e.KeyCode == Keys.Left)
            {
                if(cellArray.IndexOf(this) != 0)
                {
                    form1.GetNextControl(this, false).Select();
                    this.SelectAll();
                }
                
            }
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
}
