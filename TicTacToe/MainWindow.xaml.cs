using System;
using System.Collections.Generic;
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

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private int[] boardValues;  // Represents the current state of the game board
        private bool isPlayer1Turn; // Indicates whether it's player 1's turn

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            boardValues = new int[9];
            isPlayer1Turn = true;
            board.IsEnabled = true;

            foreach (Button button in board.Children)
            {
                button.Content = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = board.Children.IndexOf(button);

            // If the button is already clicked or the game is over, return
            if (button.Content != "" || !board.IsEnabled)
                return;

            int player = isPlayer1Turn ? 1 : 2;
            boardValues[index] = player;
            button.Content = isPlayer1Turn ? "X" : "O";
            button.IsEnabled = false;

            if (IsGameOver(player))
            {
                board.IsEnabled = false;
                MessageBox.Show($"Player {player} wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (IsBoardFull())
            {
                board.IsEnabled = false;
                MessageBox.Show("It's a tie!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                isPlayer1Turn = !isPlayer1Turn;
                if (!isPlayer1Turn)
                    PerformRobotMove();
            }
        }

        private bool IsGameOver(int player)
        {
            return
                (boardValues[0] == player && boardValues[1] == player && boardValues[2] == player) ||
                (boardValues[3] == player && boardValues[4] == player && boardValues[5] == player) ||
                (boardValues[6] == player && boardValues[7] == player && boardValues[8] == player) ||
                (boardValues[0] == player && boardValues[3] == player && boardValues[6] == player) ||
                (boardValues[1] == player && boardValues[4] == player && boardValues[7] == player) ||
                (boardValues[2] == player && boardValues[5] == player && boardValues[8] == player) ||
                (boardValues[0] == player && boardValues[4] == player && boardValues[8] == player) ||
                (boardValues[2] == player && boardValues[4] == player && boardValues[6] == player);
        }

        private bool IsBoardFull()
        {
            return !boardValues.Contains(0);
        }

        private void PerformRobotMove()
        {
            // Randomly select an available button and make a move
            for (int i = 0; i < boardValues.Length; i++)
            {
                if (boardValues[i] == 0)
                {
                    boardValues[i] = 2;
                    Button button = (Button)board.Children[i];
                    button.Content = "O";
                    button.IsEnabled = false;
                    break;
                }
            }

            if (IsGameOver(2))
            {
                board.IsEnabled = false;
                MessageBox.Show("Robot wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (IsBoardFull())
            {
                board.IsEnabled = false;
                MessageBox.Show("It's a tie!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
