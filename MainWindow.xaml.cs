using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SassyTicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameGridImage.Source = ImageControls.ByteToImage(Properties.Resources.GameGrid);
            Board.SetupBoard();
            UpdateBoard();
        }

        private void SquareClicked(object sender, MouseButtonEventArgs e)
        {
            byte index = Convert.ToByte(int.Parse((string)((Image)sender).Tag));
            if (!Board.GameComplete)
            {
                if (Board.Squares[index].Tile == null)
                {
                    if (Board.WhoseMove == TileType.X) { Board.Squares[index].Tile = new Tile(TileType.X); }
                    else { Board.Squares[index].Tile = new Tile(TileType.O); }
                    UpdateBoard();
                    Board.WhoseMove = Board.WhoseMove == TileType.X ? TileType.O : TileType.X;
                    var status = CheckForWin();
                    if (status.Item1)
                    {
                        StatusText.Text = status.Item2 == TileType.X ? "Xs Win!" : "Os Win!";
                        Board.GameComplete = true;
                    }
                }
                else
                {
                    StatusText.Text = @"Try This... https://en.wikipedia.org/wiki/Tic-tac-toe";
                } 
            }
        }

        internal void UpdateBoard()
        {
            Square0.Source = ImageControls.RetrieveImage(Board.Squares[0]);
            Square1.Source = ImageControls.RetrieveImage(Board.Squares[1]);
            Square2.Source = ImageControls.RetrieveImage(Board.Squares[2]);
            Square3.Source = ImageControls.RetrieveImage(Board.Squares[3]);
            Square4.Source = ImageControls.RetrieveImage(Board.Squares[4]);
            Square5.Source = ImageControls.RetrieveImage(Board.Squares[5]);
            Square6.Source = ImageControls.RetrieveImage(Board.Squares[6]);
            Square7.Source = ImageControls.RetrieveImage(Board.Squares[7]);
            Square8.Source = ImageControls.RetrieveImage(Board.Squares[8]);
        }

        internal Tuple<bool,TileType> CheckForWin()
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

        internal bool IsWin(Square square1, Square square2, Square square3)
        {
            if (square1.Tile == null) { return false; }
            if (square2.Tile == null) { return false; }
            if (square3.Tile == null) { return false; }

            if (square2.Tile.TileType == square1.Tile.TileType &&
                square3.Tile.TileType == square2.Tile.TileType)
                return true;
            else { return false; }
        }

        private void NewGame_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Board.GameComplete) { NewGameButton.Content = "This Time for Sure!"; }
            else { NewGameButton.Content = "Im a quitter"; }
        }

        private void NewGame_MouseLeave(object sender, MouseEventArgs e)
        {
            NewGameButton.Content = "New Game";
        }
    }
}
