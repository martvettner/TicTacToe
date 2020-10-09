
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current value of the cells in the active game 
        /// </summary>
        private Mark_Type[] mResult;

        /// <summary>
        /// True if it's player 1's turn (X) or players 2' turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended 
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>


        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion

        private void NewGame()
        {
            mResult = new Mark_Type[9];
            for (var i = 0; i < mResult.Length; i++)
                mResult[i] = Mark_Type.Free;

            mPlayer1Turn = true;

            container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });
            mGameEnded = false;
        }
        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button was cliked</param>
        /// <param name="e">The events of the clic</param>
        private void button_Click(object sender, RoutedEventArgs e)
            
        {
            if(mGameEnded)
            {
                NewGame();
                return;  
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResult[index] != Mark_Type.Free)
                return;

            mResult[index] = mPlayer1Turn ? Mark_Type.Cross : Mark_Type.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            mPlayer1Turn ^= true;

            checkforwinner();
        }

        private void checkforwinner()
        {
            #region Horizontal wins

            if (mResult[0] !=Mark_Type.Free && (mResult[0] & mResult[1] & mResult[2]) == mResult[0])
            {
                mGameEnded = true;

                button0_0.Background = button1_0.Background = button2_0.Background = Brushes.Green;
            }

            if (mResult[3] != Mark_Type.Free && (mResult[3] & mResult[4] & mResult[5]) == mResult[3])
            {
                mGameEnded = true;

                button0_1.Background = button1_1.Background = button2_1.Background = Brushes.Green;
            }
            if (mResult[6] != Mark_Type.Free && (mResult[6] & mResult[7] & mResult[8]) == mResult[6])
            {
                mGameEnded = true;

                button0_2.Background = button1_2.Background = button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vertical wins

            if (mResult[0] != Mark_Type.Free && (mResult[0] & mResult[3] & mResult[6]) == mResult[0])
            {
                mGameEnded = true;

                button0_0.Background = button0_1.Background = button0_2.Background = Brushes.Green;
            }

            if (mResult[1] != Mark_Type.Free && (mResult[1] & mResult[4] & mResult[7]) == mResult[1])
            {
                mGameEnded = true;

                button1_0.Background = button1_1.Background = button1_2.Background = Brushes.Green;
            }

            if (mResult[2] != Mark_Type.Free && (mResult[2] & mResult[5] & mResult[8]) == mResult[2])
            {
                mGameEnded = true;

                button2_0.Background = button2_1.Background = button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal win

            if (mResult[0] != Mark_Type.Free && (mResult[0] & mResult[4] & mResult[8]) == mResult[0])
            {
                mGameEnded = true;

                button0_0.Background = button1_1.Background = button2_2.Background = Brushes.Green;
            }

            if (mResult[2] != Mark_Type.Free && (mResult[2] & mResult[4] & mResult[6]) == mResult[2])
            {
                mGameEnded = true;

                button2_0.Background = button1_1.Background = button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No winners

            if (!mResult.Any(result => result == Mark_Type.Free))
            {
                mGameEnded = true;

                container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    
                    Button.Background = Brushes.Orange;
                   
                });

                #endregion
            }
        }
    }
}
