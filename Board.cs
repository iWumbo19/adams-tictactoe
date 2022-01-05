using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyTicTacToe
{
    static class Board
    {
        internal static Square[] Squares;
        internal static bool GameComplete;
        internal static int Score;
        internal static WhoseMove WhoseMove;
        internal static int XWins;
        internal static int OWins;
        internal static int PlayerWins;
        internal static int AdamWins;

        internal static void SetupBoard()
        {
            Squares = new Square[16];
            GameComplete = false;
            WhoseMove = WhoseMove.X;
        }

        internal static void ClearStats()
        {
            XWins = 0;
            OWins = 0;
            PlayerWins = 0;
            AdamWins = 0;
        }
    }

    enum WhoseMove
    {
        X,
        O
    }

}
