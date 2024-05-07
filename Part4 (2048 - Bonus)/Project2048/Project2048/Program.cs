﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2048
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            (int rows, int cols) = ConsoleGame.GetBoardSize();
            Game game = new Game(rows, cols);

            while (Game.Status == GameStatus.Idle)
            {

                ConsoleGame.PrintBoard(game);
                Direction direction = ConsoleGame.GetUserInput();
                // Console.WriteLine("You chose: " + direction);
                game.Move(direction);

                Board shiftedLeftBoard = new Board(game.Board);
                Board shiftedRightBoard = new Board(game.Board);
                Board shiftedUpBoard = new Board(game.Board);
                Board shiftedDownBoard = new Board(game.Board);

                shiftedUpBoard.Move(Direction.Up, false);
                shiftedDownBoard.Move(Direction.Down, false);
                shiftedLeftBoard.Move(Direction.Left, false);
                shiftedRightBoard.Move(Direction.Right, false);


                if (Board.SameBoards(shiftedLeftBoard.Data, game.Board.Data) &&
                  Board.SameBoards(shiftedRightBoard.Data, game.Board.Data) &&
                  Board.SameBoards(shiftedDownBoard.Data, game.Board.Data) &&
                  Board.SameBoards(shiftedUpBoard.Data, game.Board.Data))
                {
                    Game.Status = GameStatus.Lose;
                    ConsoleGame.PrintBoard(game);
                }

            }

            if (Game.Status == GameStatus.Win)
            {
                Console.WriteLine("You win!");
            }

            if (Game.Status == GameStatus.Lose)
            {
                Console.WriteLine("You Lost!");
            }
        }
    }
}