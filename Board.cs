using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace SassyTicTacToe
{
    internal static class Board
    {
        #region FIELDS
        internal static Square[] Squares;
        internal static bool GameComplete;
        internal static TileType WhoseMove;
        internal static int XWins;
        internal static int OWins;
        internal static int PlayerWins;
        internal static int AdamWins;
        internal static TileType AdamPlays;
        internal static byte TilesPlaced;
        #endregion


        #region METHODS
        /// <summary>
        /// Runs through all combinations of Tic-Tac-Toe to check if a winner is present
        /// </summary>
        /// <returns>Tuple containg Item1:Boolean of true if there is a winner and Item2:A TileType enum of the winning tile. 
        /// Will return TileType.X if there is no winner present</returns>
        internal static Tuple<bool, TileType> CheckForWin()
        {
            bool win = false;
            TileType winner = TileType.X;

            if (IsWin(Board.Squares[0], Board.Squares[3], Board.Squares[6])) { win = true; winner = Board.Squares[0].Tile.TileType; }
            if (IsWin(Board.Squares[1], Board.Squares[4], Board.Squares[7])) { win = true; winner = Board.Squares[1].Tile.TileType; }
            if (IsWin(Board.Squares[2], Board.Squares[5], Board.Squares[8])) { win = true; winner = Board.Squares[2].Tile.TileType; }
            if (IsWin(Board.Squares[0], Board.Squares[1], Board.Squares[2])) { win = true; winner = Board.Squares[0].Tile.TileType; }
            if (IsWin(Board.Squares[3], Board.Squares[4], Board.Squares[5])) { win = true; winner = Board.Squares[3].Tile.TileType; }
            if (IsWin(Board.Squares[6], Board.Squares[6], Board.Squares[8])) { win = true; winner = Board.Squares[6].Tile.TileType; }
            if (IsWin(Board.Squares[0], Board.Squares[4], Board.Squares[8])) { win = true; winner = Board.Squares[0].Tile.TileType; }
            if (IsWin(Board.Squares[2], Board.Squares[4], Board.Squares[6])) { win = true; winner = Board.Squares[2].Tile.TileType; }

            return new Tuple<bool, TileType>(win, winner);
        }


        /// <summary>
        /// Helper method of CheckForWin. Evaluates if the three passed in squares are not valid and are of the same type
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <param name="square3"></param>
        /// <returns>True if all three squares are of the same type and none are null</returns>
        private static bool IsWin(Square square1, Square square2, Square square3)
        {
            if (square1.Tile == null) { return false; }
            if (square2.Tile == null) { return false; }
            if (square3.Tile == null) { return false; }

            if (square2.Tile.TileType == square1.Tile.TileType &&
                square3.Tile.TileType == square2.Tile.TileType)
                return true;
            else { return false; }
        }


        /// <summary>
        /// Creates empty Squares array. Sets GameComplete to false. Sets WhoseMove to X. Toggles tile Adam plays with.
        /// </summary>
        internal static void SetupBoard()
        {
            Squares = new Square[16];
            GameComplete = false;
            WhoseMove = TileType.X;
            ToggleAdamPlays();
            TilesPlaced = 0;
        }


        /// <summary>
        /// Sets XWins, OWins, PlayerWins, and AdamWins to 0
        /// </summary>
        internal static void ClearStats()
        {
            XWins = 0;
            OWins = 0;
            PlayerWins = 0;
            AdamWins = 0;
        }


        /// <summary>
        /// Checks to see if there is a tile placed on a square
        /// </summary>
        /// <param name="index">Position of the square in the grid</param>
        /// <returns>True if there is a tile placed on the passed in index</returns>
        internal static bool IsSquareNull(byte index)
        {
            return Squares[index].Tile == null ? true : false;
        }


        /// <summary>
        /// Places tile based on whose turn it is at the specified index
        /// </summary>
        /// <param name="index">Place tile at this index</param>
        internal static void PlaceTile(byte index)
        {
            if (WhoseMove == TileType.X) { Squares[index].Tile = new Tile(TileType.X); }
            else { Squares[index].Tile = new Tile(TileType.O); }
            TilesPlaced++;
        }


        internal static void ToggleTurn()
        {
            WhoseMove = WhoseMove == TileType.X ? TileType.O : TileType.X;
        }

        internal static void ToggleAdamPlays()
        {
            AdamPlays = AdamPlays == TileType.X ? TileType.O : TileType.O;
        }
        #endregion
    }

    enum GameOutcome
    {
        X,
        O,
        Tie
    }
}
