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
using System.Threading;

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
            InitializeAssets();
            Board.AdamPlays = TileType.X;
            Board.SetupBoard();
            UpdateBoard();
            Adam.window = this;
            Adam.AnalyzeBoard();
            Board.ToggleTurn();
            UpdateBoard();
        }


        /// <summary>
        /// Main event for when a user clicks on the image box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SquareClicked(object sender, MouseButtonEventArgs e)
        {
            byte index = RetrieveTag(sender);
            if (!Board.GameComplete)
            {
                if (Board.IsSquareNull(index))
                {
                    Board.PlaceTile(index);
                    UpdateBoard();
                    Board.ToggleTurn();
                    if (Board.CheckForWin().Item1) //If there is a winner
                    {
                        StatusText.Text = WinMessage();
                        Board.GameComplete = true;
                    }
                    else if (Board.CheckForTie())
                    {
                        ChangeMessage("It's a tie...");
                        Board.GameComplete = true;
                    }
                    else if (Board.AdamPlays == Board.WhoseMove) //If it's the AIs turn to play
                    {
                        Adam.AnalyzeBoard(); //Also Plays Move
                        Board.ToggleTurn();
                        UpdateBoard();
                        if (Board.CheckForWin().Item1) //If there is a winner
                        {
                            StatusText.Text = WinMessage();
                            Board.GameComplete = true;
                        }
                        else if (Board.CheckForTie())
                        {
                            ChangeMessage("It's a tie...");
                            Board.GameComplete = true;
                        }
                    }
                }
                else //Player clicked on space with a tile
                {
                    StatusText.Text = @"Try This... https://en.wikipedia.org/wiki/Tic-tac-toe";
                } 
            }
        }



        /// <summary>
        /// Changes the ImageSource of image boxes if their values have changed
        /// </summary>
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


        private void NewGame_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Board.GameComplete) { NewGameButton.Content = "This Time for Sure!"; }
            else { NewGameButton.Content = "Im a quitter"; }
        }

        private void NewGame_MouseLeave(object sender, MouseEventArgs e)
        {
            NewGameButton.Content = "New Game";
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "";
            Board.SetupBoard();
            UpdateBoard();
            AdamPlaysMessage(Board.AdamPlays);
            if (Board.AdamPlays == Board.WhoseMove)
            {
                Adam.AnalyzeBoard(); //Also Plays Move
                Board.ToggleTurn();
                UpdateBoard();
            }
            
        }

        private void InitializeAssets()
        {
            GameGridImage.Source = ImageControls.ByteToImage(Properties.Resources.GameGrid);

        }


        /// <summary>
        /// Used for Grabbing the win message based on current board
        /// </summary>
        /// <returns>Returns string based on the winner of the current board</returns>
        private static string WinMessage()
        {
            return Board.CheckForWin().Item2 == TileType.X ? "Xs Win!" : "Os Win!";
        }


        internal void ChangeMessage(string message)
        {
            StatusText.Text = message;
        }

        internal void AdamPlaysMessage(TileType type)
        {
            AdamPlaysLabel.Content = type == TileType.O ? "Adam Plays: Os" : "Adam Plays: Xs";
        }

        private byte RetrieveTag(object sender)
        {
            return Convert.ToByte(int.Parse((string)((Image)sender).Tag));
        }
    }
}
