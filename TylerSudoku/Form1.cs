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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Tyler's Sudoku Helper";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

           // this.GetNextControl(this.cur, true).Select();
        }


        
    }


    public class MyCellBox : TextBox
    {
        private Form1 form1;
        private int x;
        private int y;

        public MyCellBox(Form1 form1, int i, int j)
        {
            this.form1 = form1;
            this.x = i;
            this.y = j;
            this.MaxLength = 1;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            
            base.OnKeyUp(e);
            if(e.KeyCode > Keys.D0 && e.KeyCode < Keys.D9)
            {
                if (this.Text.Length > 0 && isDigit(this.Text))
                {
                    if (x == 8 && y == 8)
                    {
                        var array = form1.Controls.OfType<MyCellBox>().ToArray();
                        array[0].Select();
                        this.SelectAll();
                    }
                    else
                    {
                        form1.GetNextControl(this, true).Select();
                        this.SelectAll();
                    }
                }
            }
            if (!isDigit(this.Text) && this.Text.Length > 0)
            {
                this.BackColor = Color.Red;
                this.Text = "";
            }
            if(isDigit(this.Text) || this.Text.Length == 0)
            {
                this.BackColor = Color.White;
            }
        }

        public bool isDigit(String input)
        {
            if(input.Length == 1)
            {
                if(input.Equals("1") || input.Equals("2") || input.Equals("3") || input.Equals("4") || input.Equals("5") || input.Equals("6") || input.Equals("7") || input.Equals("8") || input.Equals("9"))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.SelectAll();
        }
    }
}
