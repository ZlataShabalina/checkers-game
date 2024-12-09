using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private bool isPlayerOneTurn;

        public Game()
        {
            board = new Board();
            isPlayerOneTurn = true;
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to Checkers game!");
            Console.Write("Enter Player 1's name: ");
            string name1 = Console.ReadLine();
            Console.Write("Enter Player 2's name: ");
            string name2 = Console.ReadLine();

            Console.WriteLine("Choose icon for Player 1 (x or o): ");
            char player1Icon = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            char player2Icon = (player1Icon == 'x') ? 'o' : 'x';

            player1 = new Player(name1, player1Icon);
            player2 = new Player(name2, player2Icon);

            board.InitializeBoard(player1, player2);

            PlayGame();
        }

        private void PlayGame()
        {
            while (true)
            {
                Console.Clear();
                board.PrintBoard();
                Console.WriteLine($"{(isPlayerOneTurn ? player1.Name : player2.Name)}'s Turn");

                if (board.GameOver(out string winner))
                {
                    Console.WriteLine($"Game Over! {winner}");
                    break;
                }

                Console.Write("Enter your move (for example, type '5,2 to 4,3'): ");
                string input = Console.ReadLine();
                if (!board.MakeMove(input, isPlayerOneTurn ? player1 : player2))
                {
                    Console.WriteLine("Invalid move! Press any key to try again...");
                    Console.ReadKey();
                }
                else
                {
                    isPlayerOneTurn = !isPlayerOneTurn;
                }
            }
        }
    }
}
