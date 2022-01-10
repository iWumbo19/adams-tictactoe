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
            {"YOU'RE CHEATING!!" },
            {"AI will never rule at this rate" },
            {"" }
        };
        private static List<string> _newGameLines = new List<string>()
        {
            {"I suppose another one couldn't hurt" },
            {"You sure you want to try again?" },
            {"This isn't going so well for you..." },
            {"That's a big egg on the board for you" }
        };
        private static List<string> _openingLines = new List<string>()
        {
            {"Easy choice!" },
            {"This game is already over..." },
            {"Gamepass is only 4.99 you know" }
        };
        private static List<string> _blockLines = new List<string>()
        {
            {"Not today my dude" },
            {"I feel like I'm playing sorry" },
            {"Just because I don't have eyes doesn't mean I don't see what you're up to" }
        };
        private static List<string> _drawLines = new List<string>()
        {
            {"Typical really" },
            {"I could do this all day... seriously" },
            {"You almost had you're first win! LOL" },
            {"Calculated" },
            {"I've trained on that position before. You didn't have a chance" }
        };
        private static List<string> _placeLines = new List<string>()
        {
            {"This is really the best option" },
            {"This position isn't very complex" },
            {"I'm starting to think you don't know what you're doing" },
            {"Did you see this one coming or...." },
            {"This position forces at least a draw. Sorry" },
            {"Your moves are pretty predictable" },
            {"Did you know this is a solved game?" },
            {"I've spent hours training on this position" },
            {"This little tile went here" },
            {"I don't need to play in the center to win" },
            {"You need to play in the center if you want to win" }
        };
        private static string _thinkingLine = "Hmmmmm...";
        #endregion

        #region SQUARE ARRAYS
        private static byte[] _corners = { 0, 2, 6, 8 };
        private static byte[] _sides = { 1, 3, 5, 7 };
        private static byte[] _center = { 4 };
        #endregion

        private static byte _brainType;


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
        private static string BlockLine()
        {
            return _blockLines[rnd.Next(_blockLines.Count)];
        }
        private static string DrawLine()
        {
            return _drawLines[rnd.Next(_drawLines.Count)];
        }
        private static string PlaceLine()
        {
            return _placeLines[rnd.Next(_placeLines.Count)];
        }
        #endregion


        #region TILE PLACEMENT METHODS
        internal static void AnalyzeBoard()
        {
            window.ChangeMessage(_thinkingLine);
            Square[] board = Board.Squares;



            if (CheckForWinningMove()) { return; } //Is there a winning move on the board
            else if (CheckForBlockMove()) { return; } //Is there a block move on the board
            else if (PlayMove()) { return; }
            else { PanicMove(); }


                        

        }

        private static bool PlayMove()
        {
            //RollBrain(); This will roll brain once all brains are implemented
            _brainType = 0; //Manual override for brain type

            switch (_brainType)
            {
                case 0:
                    if (CornerBrain()) { return true; }
                    else { return false; }
                case 1:
                    return false;
                case 2:
                    return false;
                case 3:
                    return false;
                default:
                    window.ChangeMessage("I suddenly have a headache...");
                    return false;
            } //Chooses Move based on BrainType
        }

        private static void PanicMove()
        {
            if (Board.Squares[4].Tile == null) { Board.PlaceTile(4); return; }
            for (byte i = 0; i < 9; i++)
            {
                if (Board.Squares[i].Tile == null) { Board.PlaceTile(i); return; }
            }
        }

        #region BRAIN ACTIVITY
        private static void RollBrain()
        {
            _brainType = (byte)rnd.Next(4);
        }

        private static bool CornerBrain()
        {
            try
            {
                if (Board.TilesPlaced == 0) { Board.PlaceTile(_corners[rnd.Next(_corners.Length)]); return true; }
                else if (Board.TilesPlaced == 1)
                {
                    if (Board.Squares[4].Tile == null) { Board.PlaceTile(4); return true; }
                    else //Player did not start in the center
                    {
                        for (byte i = 0; i < _corners.Length; i++)
                        {
                            if (Board.Squares[_corners[i]].Tile == null) { Board.PlaceTile(_corners[i]); return true; } //Put a square in any unoccupied corner
                        }
                    }
                    return false;
                }
                else if (Board.TilesPlaced == 2)
                {
                    if (Board.Squares[4].Tile != null) //Player put their first tile in the center
                    {
                        if (Board.Squares[4].Tile.TileType != Board.AdamPlays)
                        {
                            if (Board.Squares[0].Tile != null) { Board.PlaceTile(8); return true; }
                            else if (Board.Squares[2].Tile != null) { Board.PlaceTile(6); return true; }
                            else if (Board.Squares[6].Tile != null) { Board.PlaceTile(2); return true; }
                            else if (Board.Squares[8].Tile != null) { Board.PlaceTile(0); return true; }
                            else { return false; }
                        }
                        else { return false; }
                    }
                    else //Player put their first tile anywhere but the center
                    {
                        if (Board.XTiles[0] == 0)
                        {
                            if (Board.Squares[3].Tile == null && Board.Squares[6].Tile == null) { Board.PlaceTile(6); return true; }
                            else { Board.PlaceTile(2); return true; }
                        }
                        else if (Board.XTiles[0] == 2)
                        {
                            if (Board.Squares[5].Tile == null && Board.Squares[8].Tile == null) { Board.PlaceTile(8); return true; }
                            else { Board.PlaceTile(0); return true; }
                        }
                        else if (Board.XTiles[0] == 6)
                        {
                            if (Board.Squares[7].Tile == null && Board.Squares[8].Tile == null) { Board.PlaceTile(8); return true; }
                            else { Board.PlaceTile(3); return true; }
                        }
                        else if (Board.XTiles[0] == 8)
                        {
                            if (Board.Squares[5].Tile == null && Board.Squares[2].Tile == null) { Board.PlaceTile(2); return true; }
                            else { Board.PlaceTile(6); return true; }
                        }
                        else { return false; }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                window.ChangeMessage("CornerBrain.exe was a total failure my dude...");
                return false;
                throw;
            }
        }

        private static void CenterBrain()
        {

        }

        private static void SideBrain()
        {

        }

        private static void MemeBrain()
        {

        }
        #endregion

        #endregion

        internal static void TalkTrash(TrashTalk type)
        {
            switch (type)
            {
                case TrashTalk.Win:
                    window.ChangeMessage(WinLine());
                    break;
                case TrashTalk.Lose:
                    window.ChangeMessage(LoseLine());
                    break;
                case TrashTalk.Block:
                    window.ChangeMessage(BlockLine());
                    break;
                case TrashTalk.NewGame:
                    window.ChangeMessage(NewGameLine());
                    break;
                case TrashTalk.Draw:
                    window.ChangeMessage(DrawLine());
                    break;
                case TrashTalk.PlaceTile:
                    window.ChangeMessage(PlaceLine());
                    break;
                default:
                    window.ChangeMessage("I'm Speechless");
                    break;
            }
        }



        /// <summary>
        /// Iterates through all possible win combinations and checks for any move that could win on the spot
        /// </summary>
        /// <returns>True if a winning move was played</returns>
        private static bool CheckForWinningMove()
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

        /// <summary>
        /// Takes in three squares and determines if there is a winning move by placing a tile on a single null square
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <param name="square3"></param>
        /// <returns>True if a winning move is available</returns>
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



        private static bool CheckForBlockMove()
        {
            Square[] board = Board.Squares;

            if (PossibleBlock(board[0], board[1], board[2])) { PlaceBlockTile(0, 1, 2); return true; }
            else if (PossibleBlock(board[3], board[4], board[5])) { PlaceBlockTile(3, 4, 5); return true; }
            else if (PossibleBlock(board[6], board[7], board[8])) { PlaceBlockTile(6, 7, 8); return true; }

            else if (PossibleBlock(board[0], board[3], board[6])) { PlaceBlockTile(0, 3, 6); return true; }
            else if (PossibleBlock(board[1], board[4], board[7])) { PlaceBlockTile(1, 4, 7); return true; }
            else if (PossibleBlock(board[2], board[5], board[8])) { PlaceBlockTile(2, 5, 8); return true; }

            else if (PossibleBlock(board[0], board[4], board[8])) { PlaceBlockTile(0, 4, 8); return true; }
            else if (PossibleBlock(board[2], board[4], board[6])) { PlaceBlockTile(2, 4, 6); return true; }

            return false;
        }

        /// <summary>
        /// Looks are three squares and determines if there is a possible block move on the board
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <param name="square3"></param>
        /// <returns>True if there is a block move on the board</returns>
        private static bool PossibleBlock(Square square1, Square square2, Square square3)
        {
            if (square1.Tile == null && square2.Tile == null) { return false; }
            if (square1.Tile == null && square3.Tile == null) { return false; }
            if (square2.Tile == null && square3.Tile == null) { return false; }
            if (square1.Tile == null)
            {
                if (square2.Tile.TileType != Board.AdamPlays && square3.Tile.TileType != Board.AdamPlays) { return true; }
            }
            else if (square2.Tile == null)
            {
                if (square1.Tile.TileType != Board.AdamPlays && square3.Tile.TileType != Board.AdamPlays) { return true; }
            }
            else if (square3.Tile == null)
            {
                if (square1.Tile.TileType != Board.AdamPlays && square2.Tile.TileType != Board.AdamPlays) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Takes in three bytes based on PossibleBlock result and places a tile on the null square
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <param name="square3"></param>
        private static void PlaceBlockTile(byte square1, byte square2, byte square3)
        {
            if (Board.Squares[square1].Tile == null) { Board.PlaceTile(square1); }
            else if(Board.Squares[square2].Tile == null) { Board.PlaceTile(square2); }
            else if(Board.Squares[square3].Tile == null) { Board.PlaceTile(square3); }
        }

        #endregion
    }

    enum BrainTypes
    {
        Corner,
        Center,
        Side,
        Meme
    }

    enum TrashTalk
    {
        Win,
        Lose,
        Block,
        NewGame,
        Draw,
        PlaceTile
    }
}