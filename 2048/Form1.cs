using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        Matrix matrix;


        public Form1()
        {
            InitializeComponent();


            matrix = new Matrix();
            matrix.endGame += matrix_endGame;

            ControlInit(matrix._Mx, -1, -1);
        }

        void matrix_endGame()
        {
            MessageBox.Show(string.Format("End Game! Your score is {0}", 
                                        Game.TotalScore.ToString()));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int indexI, indexJ;
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    matrix.Transform(e.KeyCode);
                    matrix.GetRandom(out indexI, out indexJ);
                    ControlInit(matrix._Mx, indexI, indexJ);
                    CalculateTotalScore();
                    break;

            }

            if (e.Alt && e.KeyCode == Keys.F4)                  //при Alt F4 закрываем форму
                this.Close();
        }

        private void CalculateTotalScore()
        {
            lblTotalScore.Text = Game.TotalScore.ToString();
        }

        private void ControlInit(int[,] a, int randomI, int randomJ)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    foreach (Control ctrl in this.Controls)
                        if (ctrl is Label)
                            if (((Label)ctrl).Name == "c" + i + j)
                            {
                                ctrl.Text = a[i, j].ToString();
                                if (ctrl.Text != "0")
                                    ctrl.BackColor = Color.Green;
                                else
                                    ctrl.BackColor = Color.Black;

                                if (randomI != -1 && randomJ != -1 && i == randomI && j == randomJ)
                                    ctrl.BackColor = Color.Red;

                                break;
                            }
        }

    }
}
