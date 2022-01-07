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

        #region SQUARE ARRAYS
        internal static byte[] _centerSquare = { 4 };
        internal static byte[] _cornerSquare = { 0, 2, 6, 8 };
        internal static byte[] _sideSquare = { 1, 3, 5, 7 };
        #endregion


        #region METHODS

        #region VOICE LINES
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
        #endregion

        internal static void AnalyzeBoard()
        {
            window.ChangeMessage(_thinkingLine);
            Square[] board = Board.Squares;
            byte tilesPlaced = Board.TilesPlaced;

            //TESTPLAY(tilesPlaced);

            if (CheckForWinningMove(Board.AdamPlays)) { return; } //Is there a winning move on the board
            if
            if (Board.AdamPlays == TileType.X)
            {
                if (tilesPlaced == 0) { Board.PlaceTile(0); window.ChangeMessage(OpeningLine()); ; }
                if (tilesPlaced == 2)
                {
                    if
                }

            }

        }

        internal static void TESTPLAY(byte tiles)
        {
            if (tiles == 0) { Board.PlaceTile(0); window.ChangeMessage(OpeningLine()); ; }
            else if (tiles == 1) { Board.PlaceTile(0); window.ChangeMessage(OpeningLine()); ; }
            else if (tiles == 2) { Board.PlaceTile(1); window.ChangeMessage(OpeningLine()); ; }
            else if (tiles == 3) { Board.PlaceTile(1); window.ChangeMessage(OpeningLine()); ; }
            else if (tiles == 4) { Board.PlaceTile(2); window.ChangeMessage(OpeningLine()); ; }
            else if (tiles == 5) { Board.PlaceTile(2); window.ChangeMessage(OpeningLine()); ; }
        }

        private static bool CheckForWinningMove(TileType type)
        {
            Square[] board = Board.Squares;

            if (PossibleWin(board[0], board[1], board[2])) { Board.PlaceTile(0); Board.PlaceTile(1); Board.PlaceTile(2); return true; }
            else if (PossibleWin(board[3], board[4], board[5])) { Board.PlaceTile(3); Board.PlaceTile(4); Board.PlaceTile(5); return true; }
            else if (PossibleWin(board[6], board[7], board[8])) { Board.PlaceTile(6); Board.PlaceTile(7); Board.PlaceTile(8); return true; }

            else if (PossibleWin(board[0], board[3], board[6])) { Board.PlaceTile(0); Board.PlaceTile(3); Board.PlaceTile(6); return true; }
            else if (PossibleWin(board[1], board[4], board[7])) { Board.PlaceTile(1); Board.PlaceTile(4); Board.PlaceTile(7); return true; }
            else if (PossibleWin(board[2], board[5], board[8])) { Board.PlaceTile(2); Board.PlaceTile(5); Board.PlaceTile(8); return true; }

            else if (PossibleWin(board[0], board[4], board[8])) { Board.PlaceTile(0); Board.PlaceTile(4); Board.PlaceTile(8); return true; }
            else if (PossibleWin(board[2], board[4], board[6])) { Board.PlaceTile(2); Board.PlaceTile(4); Board.PlaceTile(6); return true; }

            else { return false; }
        }

        private static bool PossibleWin(Square square1, Square square2, Square square3)
        {
            if (square1.Tile == null && square2.Tile == null) { return false; }
            if (square1.Tile == null && square3.Tile == null) { return false; }
            if (square2.Tile == null && square3.Tile == null) { return false; }
            if (square1.Tile == null)
            {
                if(square2.Tile.TileType == Board.AdamPlays && square3.Tile.TileType == Board.AdamPlays) { return true; }
            }
            else if (square2.Tile == null)
            {
                if (square1.Tile.TileType == Board.AdamPlays && square3.Tile.TileType == Board.AdamPlays) { return true; }
            }
            else if (square3.Tile == null)
            {
                if (square1.Tile.TileType == Board.AdamPlays && square2.Tile.TileType == Board.AdamPlays) { return true; }
            }
            return false;
        }

        private static bool CheckForBlockMove(TileType type)
        {
            
        }

        #endregion
    }
}