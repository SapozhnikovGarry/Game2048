using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    static class Game
    {
        static int totalScore;

        public static int TotalScore
        {
            get { return Game.totalScore; }
        }

        public static void addTotalScore(int value)
        {
            totalScore += value;
        }

        public static void resetTotalScore()
        {
            totalScore = 0;
        }


    }
}
