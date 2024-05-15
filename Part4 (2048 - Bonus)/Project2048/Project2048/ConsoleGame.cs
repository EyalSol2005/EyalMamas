using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2048
{
    public static class ConsoleGame
    {
        /// <summary>
        /// The function prints out the board and amount of points
        /// </summary>
        /// <param name="game">The game that contains the board and points</param>
        public static void PrintBoard(Game game)
        {
            Console.WriteLine($"Points: ({game.Points})");
            for (int i = 0; i < game.Board.Data.GetLength(0); i++)
            {
                for (int j = 0; j < game.Board.Data.GetLength(1); j++)
                {
                    Console.Write($"{game.Board.Data[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();

        }

        /// <summary>
        /// The function gets an input from the user and returns the chosen direction
        /// </summary>
        /// <returns>Direction that the user chose</returns>
        public static Direction GetUserInput()
        {
            // Console.WriteLine("Enter direction: ");
            var input = Console.ReadKey();
            Direction direction;

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    direction = Direction.Up;
                    break;

                case ConsoleKey.DownArrow:
                    direction = Direction.Down;
                    break;

                case ConsoleKey.RightArrow:
                    direction = Direction.Right;
                    break;

                case ConsoleKey.LeftArrow:
                    direction = Direction.Left;
                    break;

                default:
                    Console.WriteLine("Invalid input. Only Up, Down, Right, Left are allowed.");
                    return GetUserInput(); // trying to get input again
            }

            return direction;
        }

        /// <summary>
        /// The function gets the amount of wanted rows and cols from the user (Bonus)
        /// </summary>
        /// <returns>amount of rows and cols</returns>
        public static (int, int) GetBoardSize()
        {
            int rows = GetNumberInput("Enter amount of rows:");
            int cols = GetNumberInput("Enter amount of cols:");
            return (rows, cols);
        }

        /// <summary>
        /// The function gets a number from the user
        /// </summary>
        /// <param name="message">Message to print</param>
        /// <returns>The number the user chose</returns>
        private static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string numberStr = Console.ReadLine();
            int number;
            if (!Int32.TryParse(numberStr, out number) || number <= 1)
            {
                Console.WriteLine("Invalid input. Only enter a positive integer greater than one.");

                return GetNumberInput(message);
            }

            return number;
        }
    }
}
