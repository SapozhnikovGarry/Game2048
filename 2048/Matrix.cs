using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Matrix
    {
        int[,] Mx;

        public delegate void endGameHandler();
        public event endGameHandler endGame;

        public int[,] _Mx
        {
            get { return Mx; }
            set { Mx = value; }
        }

        public Matrix()
        {
            Mx = new int[4, 4];
        }

        public void InitByZero()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Mx[i, j] = 0;
        }

        public void Init(int[,] a)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Mx[i, j] = a[i, j];
        }

        public void GetRandom(out int indexI, out int indexJ)
        {
            indexI = indexJ = -1;
            Random rnd = new Random();
            int rezRandom = rnd.Next(0, 10) != 1 ? 2 : 4;

            if (MatrixIsNotFull())
            {
                bool flag = true;
                while (flag)
                {
                    int i = rnd.Next(0, 16) / 4;
                    int j = rnd.Next(0, 16) % 4;

                    if (Mx[i, j] == 0)
                    {
                        Mx[i, j] = rezRandom;
                        indexI = i;
                        indexJ = j;
                        flag = false;
                    }

                }
            }


        }

        public void Transform(System.Windows.Forms.Keys k)
        {
            if (!MatrixIsEmpty())
            {
                switch (k)
                {
                    case System.Windows.Forms.Keys.Right:
                        MoveToRight();
                        break;

                    case System.Windows.Forms.Keys.Left:
                        MoveToLeft();
                        break;

                    case System.Windows.Forms.Keys.Up:
                        MoveToUp();
                        break;

                    case System.Windows.Forms.Keys.Down:
                        MoveToDown();
                        break;
                }
            }

        }


        private void MoveToRight()
        {
            for (int i = 0; i < 4; i++)
            {
                int j = 0;

                if (!RowIsFull(i)) //если строка не полная
                {
                    while (j < 3)
                    {

                        if (Mx[i, j] != 0)
                        {
                            if (Mx[i, j + 1] != 0)
                            {
                                if (Mx[i, j] == Mx[i, j + 1])
                                {
                                    Mx[i, j] = 0;
                                    Game.addTotalScore(Mx[i, j + 1]);
                                    Mx[i, j + 1] *= 2;

                                }
                            }
                            else
                            {
                                Mx[i, j + 1] = Mx[i, j];
                                Mx[i, j] = 0;
                            }
                        }
                        j++;
                    }
                }

            }
        }


        private void MoveToLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                int j = 3;

                if (!RowIsFull(i)) //если строка не полная
                {
                    while (j > 0)
                    {
                        if (Mx[i, j] != 0)
                        {
                            if (Mx[i, j - 1] != 0)
                            {
                                if (Mx[i, j] == Mx[i, j - 1])
                                {
                                    Mx[i, j] = 0;
                                    Game.addTotalScore(Mx[i, j - 1]);
                                    Mx[i, j - 1] *= 2;

                                }
                            }
                            else
                            {
                                Mx[i, j - 1] = Mx[i, j];
                                Mx[i, j] = 0;
                            }
                        }
                        j--;
                    }
                }
            }
        }

        private void MoveToUp()
        {
            for (int i = 0; i < 4; i++)
            {
                int j = 3;

                if (!ColumnIsFull(i))
                {
                    while (j > 0)
                    {
                        if (Mx[j, i] != 0)
                        {
                            if (Mx[j - 1, i] != 0)
                            {
                                if (Mx[j, i] == Mx[j - 1, i])
                                {
                                    Mx[j, i] = 0;
                                    Game.addTotalScore(Mx[j - 1, i]);
                                    Mx[j - 1, i] *= 2;

                                }
                            }
                            else
                            {
                                Mx[j - 1, i] = Mx[j, i];
                                Mx[j, i] = 0;
                            }
                        }
                        j--;
                    }
                }
            }
        }

        private void MoveToDown()
        {
            for (int i = 0; i < 4; i++)
            {
                int j = 0;

                if (!ColumnIsFull(i))
                {
                    while (j < 3)
                    {
                        if (Mx[j, i] != 0)
                        {
                            if (Mx[j + 1, i] != 0)
                            {
                                if (Mx[j, i] == Mx[j + 1, i])
                                {
                                    Mx[j, i] = 0;
                                    Game.addTotalScore(Mx[j + 1, i]);
                                    Mx[j + 1, i] *= 2;

                                }
                            }
                            else
                            {
                                Mx[j + 1, i] = Mx[j, i];
                                Mx[j, i] = 0;
                            }
                        }
                        j++;
                    }
                }
            }
        }


        /// <summary>
        /// проверка на пустую матрицу
        /// </summary>
        /// <returns></returns>
        private bool MatrixIsEmpty()
        {
            bool flag = true;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (Mx[i, j] != 0)
                        return false;
            return flag;

        }

        private bool MatrixIsNotFull()
        {
            bool flag = true;
            int cnt = 0;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (Mx[i, j] != 0)
                        cnt++;

            if (cnt == 16)
            {
                flag = false;
                endGame();
            }

            return flag;
        }


        /// <summary>
        /// проверка на полную строку
        /// </summary>
        /// <param name="i">номер строки</param>
        /// <returns></returns>
        bool RowIsFull(int i)
        {
            bool flag = true;

            for (int j = 0; j < 4; j++)
            {
                if (Mx[i, j] == 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// проверка на полный столбец
        /// </summary>
        /// <param name="i">номер столбца</param>
        /// <returns></returns>
        bool ColumnIsFull(int i)
        {
            bool flag = true;

            for (int j = 0; j < 4; j++)
            {
                if (Mx[j, i] == 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }


    }
}
