using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2048
{
    public class Board
    {
        public int[,] Data { get; protected set; }


        public Board(int rows, int cols)
        {
            this.Data = new int[rows, cols];
            this.InitFirstValues();
        }

        public Board(Board board)
        {
            this.Data = new int[board.Data.GetLength(0), board.Data.GetLength(1)];

            for (int i = 0; i < board.Data.GetLength(0); i++)
            {
                for (int j = 0; j < board.Data.GetLength(1); j++)
                {
                    this.Data[i, j] = board.Data[i, j];
                }
            }
        }

        public void InitFirstValues()
        {
            Random rnd = new Random();
            int boardSize = this.Data.GetLength(0) * this.Data.GetLength(1);
            int index1 = rnd.Next(0, boardSize + 1); // 0-15
            int index2 = rnd.Next(0, boardSize + 1); // 0-15
            int value1 = rnd.Next(0, 2); // 0-1
            int value2 = rnd.Next(0, 2); // 0-1

            this.Data[index1 / this.Data.GetLength(1), index1 % this.Data.GetLength(1)] = value1 == 0 ? 2 : 4;
            this.Data[index2 / this.Data.GetLength(1), index2 % this.Data.GetLength(1)] = value2 == 0 ? 2 : 4;
        }

        public void addNewValue()
        {
            Random rnd = new Random();
            bool invalidCell = true;

            while (invalidCell)
            {
                int index = rnd.Next(0, 16); // 0-15
                int value = rnd.Next(0, 2); // 0-1

                if (this.Data[index / this.Data.GetLength(1), index % this.Data.GetLength(1)] == 0)
                {
                    this.Data[index / this.Data.GetLength(1), index % this.Data.GetLength(1)] = value == 0 ? 2 : 4;
                    invalidCell = false;
                }
            }
        }

        public int Move(Direction direction, bool realPlay)
        {
            int pointsGained = 0;
            bool boardChanged = false;


            if (direction == Direction.Up || direction == Direction.Down)
            {
                for (int i = 0; i < this.Data.GetLength(1); i++)
                {
                    int[] currentCol = this.GetCol(i);
                    int[] shiftedCol = Board.ShiftLine(currentCol, direction, ref pointsGained, realPlay);
                    this.UpdateCol(shiftedCol, i);

                    if (!SameLines(currentCol, shiftedCol))
                    {
                        boardChanged = true;
                    }
                }

            }

            else if (direction == Direction.Right || direction == Direction.Left)
            {
                for (int i = 0; i < this.Data.GetLength(0); i++)
                {
                    int[] currentRow = this.GetRow(i);
                    int[] shiftedRow = Board.ShiftLine(currentRow, direction, ref pointsGained, realPlay);
                    this.UpdateRow(shiftedRow, i);

                    if (!SameLines(currentRow, shiftedRow))
                    {
                        boardChanged = true;
                    }
                }
            }

            if (boardChanged)
                this.addNewValue();

            return pointsGained;
        }

        private int[] GetRow(int index)
        {
            int[] row = new int[this.Data.GetLength(1)];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = this.Data[index, i];
            }

            return row;
        }

        private int[] GetCol(int index)
        {
            int[] col = new int[this.Data.GetLength(0)];
            for (int i = 0; i < col.Length; i++)
            {
                col[i] = this.Data[i, index];
            }

            return col;
        }


        private static bool SameLines(int[] line1, int[] line2)
        {
            if ((line1.Length != line2.Length) || (line1.Length != line2.Length))
            {
                return false;
            }

            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i] != line2[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static bool SameBoards(int[,] board1, int[,] board2)
        {
            if ((board1.GetLength(0) != board2.GetLength(0)) || (board1.GetLength(1) != board2.GetLength(1)))
            {
                return false;
            }

            for (int i = 0; i < board1.GetLength(0); i++)
            {
                for (int j = 0; j < board1.GetLength(1); j++)
                {
                    if (board1[i, j] != board2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void GroupLineTogether(int[] line)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] == 0) // empty cell
                {

                    int nextValue = line[i + 1];
                    line[i] = nextValue;
                    line[i + 1] = 0;
                    if (nextValue != 0 && i != 0)
                    {
                        i = i - 2;
                    }
                }
            }
        }

        private static void MergeSameCellValues(int[] line, ref int pointsGained, bool realPlay)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                if ((line[i + 1] == line[i]) && line[0] != 0)
                {
                    line[i] = 2 * line[i];
                    line[i + 1] = 0;
                    pointsGained += line[i];
                    if (line[i] == 2048 && realPlay)
                    {
                        Game.Status = GameStatus.Win;
                    }
                }
            }
        }

        private static int[] ShiftLine(int[] line, Direction direction, ref int pointsGained, bool realPlay)
        {
            int[] shiftedLine = (int[])line.Clone();

            if (direction == Direction.Down || direction == Direction.Right)
            {
                Array.Reverse(shiftedLine);
            }

            GroupLineTogether(shiftedLine);

            MergeSameCellValues(shiftedLine, ref pointsGained, realPlay);

            GroupLineTogether(shiftedLine);

            if (direction == Direction.Down || direction == Direction.Right)
            {
                Array.Reverse(shiftedLine);
            }

            return shiftedLine;
        }

        private void UpdateRow(int[] row, int index)
        {
            for (int i = 0; i < row.Length; i++)
            {
                this.Data[index, i] = row[i];
            }
        }

        private void UpdateCol(int[] col, int index)
        {
            for (int i = 0; i < col.Length; i++)
            {
                this.Data[i, index] = col[i];
            }
        }
    }
}
