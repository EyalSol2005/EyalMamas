using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2048
{
    public static class ConsoleGame
    {
        public static void PrintBoard(Game game)
        {
            Console.WriteLine($"Points: ({game.Points})");
            for (int i = 0; i < game.Board.Data.GetLength(0); i++)
            {
                Console.WriteLine("-----------------");
                for (int j = 0; j < game.Board.Data.GetLength(1); j++)
                {
                    Console.Write($"| {game.Board.Data[i, j]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----------------");
        }

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

        public static (int, int) GetBoardSize()
        {
            int rows = GetNumberInput("Enter amount of rows:");
            int cols = GetNumberInput("Enter amount of cols:");
            return (rows, cols);
        }

        private static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string numberStr = Console.ReadLine();
            int number;
            if (!Int32.TryParse(numberStr, out number) || number <= 0)
            {
                Console.WriteLine("Invalid input. Only enter a positive integer.");

                return GetNumberInput(message);
            }

            return number;
        }
    }
}
