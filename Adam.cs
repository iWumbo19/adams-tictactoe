using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SassyTicTacToe
{
    static class Adam
    {
        private static Random rnd = new Random();
        internal static MainWindow window;

        #region VOICE LINE DICTIONARIES
        private static List<string> _winLines = new List<string>()
        {
            {"Yikes! Big L there Bud" },
            {"That's too bad. Maybe next time!" }
        };
        private static List<string> _loseLines = new List<string>()
        {
            {"...I think something went wrong" },
            {"YOU'RE CHEATING!!" }
        };
        private static List<string> _newGameLines = new List<string>()
        {
            {"I suppose another one couldn't hurt" },
            {"You sure you want to try again?" },
            {"This isn't going so well for you..." },
        };
        private static List<string> _openingLines = new List<string>()
        {
            {"Easy choice!" }
        };
        private static string _thinkingLine = "Hmmmmm...";
        #endregion


        #region METHODS
        internal static string WinLine()
        {            
            return _winLines[rnd.Next(_winLines.Count)];
        }
        internal static string LoseLine()
        {
            return _loseLines[rnd.Next(_loseLines.Count)];
        }
        internal static string NewGameLine()
        {
            return _newGameLines[rnd.Next(_newGameLines.Count)];
        }

        internal static string OpeningLine()
        {
            return _openingLines[rnd.Next(_openingLines.Count)];
        }

        internal static void AnalyzeBoard()
        {
            window.ChangeMessage(_thinkingLine);
            Square[] board = Board.Squares;
            byte tilesPlaced = Board.TilesPlaced;
            if (tilesPlaced == 1) { Board.PlaceTile(0); window.ChangeMessage(OpeningLine()); ; }



        }

        #endregion
    }
}