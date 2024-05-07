using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2048
{
    public class Game
    {
        public Board Board { get; set; }
        public static GameStatus Status { get; set; }
        public int Points { get; protected set; }

        public Game(int rows, int cols)
        {
            Board = new Board(rows, cols);
            Status = GameStatus.Idle;
            Points = 0;
        }

        public void Move(Direction direction)
        {
            if (Status == GameStatus.Idle)
            {
                this.Points += this.Board.Move(direction, true);
            }

        }

    }
}
