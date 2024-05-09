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

        /// <summary>
        /// The function inits the first two random values of the board
        /// </summary>
        public void InitFirstValues()
        {
            Random rnd = new Random();
            int boardSize = this.Data.GetLength(0) * this.Data.GetLength(1);
            int index1 = rnd.Next(0, boardSize);
            int index2 = rnd.Next(0, boardSize);
            int value1 = rnd.Next(0, 2); // 0-1
            int value2 = rnd.Next(0, 2); // 0-1

            while(index2 == index1) // in case the two random positions are the same
            {
                index2 = rnd.Next(0, boardSize);
            }

            // set values to the random x,y position
            this.Data[index1 / this.Data.GetLength(1), index1 % this.Data.GetLength(1)] = value1 == 0 ? 2 : 4;
            this.Data[index2 / this.Data.GetLength(1), index2 % this.Data.GetLength(1)] = value2 == 0 ? 2 : 4;
        }

        /// <summary>
        /// The function adds a new random value to the board
        /// </summary>
        public void addNewValue()
        {
            Random rnd = new Random();
            bool invalidCell = true;

            while (invalidCell)
            {
                int boardSize = this.Data.GetLength(0) * this.Data.GetLength(1);
                int index = rnd.Next(0, boardSize);
                int value = rnd.Next(0, 2); // 0-1


                if (this.Data[index / this.Data.GetLength(1), index % this.Data.GetLength(1)] == 0) // empty cell
                {
                    // set values to the random x,y position
                    this.Data[index / this.Data.GetLength(1), index % this.Data.GetLength(1)] = value == 0 ? 2 : 4;
                    invalidCell = false;
                }
            }
        }

        /// <summary>
        /// The function gets a direction and moves the whole board in that direction
        /// </summary>
        /// <param name="direction">Direction to move the board to</param>
        /// <param name="realPlay">True if considered as the player's play, false if not (used when checking if there are any possiblities left)</param>
        /// <returns></returns>
        public int Move(Direction direction, bool realPlay)
        {
            int pointsGained = 0;
            bool boardChanged = false;


            if (direction == Direction.Up || direction == Direction.Down) // moving by the cols
            {
                for (int i = 0; i < this.Data.GetLength(1); i++)
                {
                    int[] currentCol = this.GetCol(i); // getting current col
                    int[] shiftedCol = Board.ShiftLine(currentCol, direction, ref pointsGained, realPlay); // shifting the current col
                    this.UpdateCol(shiftedCol, i); // updating to the new shifted col

                    if (!SameLines(currentCol, shiftedCol)) // checking if the col stayed the same after shifting (same values)
                    {
                        boardChanged = true;
                    }
                }

            }

            else if (direction == Direction.Right || direction == Direction.Left) // moving by the rows
            {
                for (int i = 0; i < this.Data.GetLength(0); i++)
                {
                    int[] currentRow = this.GetRow(i); // getting current row
                    int[] shiftedRow = Board.ShiftLine(currentRow, direction, ref pointsGained, realPlay); // shifting the current row
                    this.UpdateRow(shiftedRow, i); // updating to the new shifted row

                    if (!SameLines(currentRow, shiftedRow)) // checking if the row stayed the same after shifting (same values)
                    {
                        boardChanged = true;
                    }
                }
            }

            if (boardChanged) // adding a new value only if the board changed
                this.addNewValue();

            return pointsGained;
        }

        /// <summary>
        /// The function returns the wanted row
        /// </summary>
        /// <param name="index">Index of the wanted row</param>
        /// <returns>The wanted row</returns>
        private int[] GetRow(int index)
        {
            int[] row = new int[this.Data.GetLength(1)];
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = this.Data[index, i];
            }

            return row;
        }

        /// <summary>
        /// The function returns teh wanted col
        /// </summary>
        /// <param name="index">Index of teh wanted col</param>
        /// <returns>The wanted col</returns>
        private int[] GetCol(int index)
        {
            int[] col = new int[this.Data.GetLength(0)];
            for (int i = 0; i < col.Length; i++)
            {
                col[i] = this.Data[i, index];
            }

            return col;
        }

        /// <summary>
        /// The function checks if two lines are the same (same values in the same positions)
        /// </summary>
        /// <param name="line1">First line</param>
        /// <param name="line2">Second line</param>
        /// <returns>True if the same, false if not</returns>
        private static bool SameLines(int[] line1, int[] line2)
        {
            if ((line1.Length != line2.Length)) // not same length
            {
                return false;
            }

            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i] != line2[i]) // not same values
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The function checks if two boards are the same (same values in the same positions)
        /// </summary>
        /// <param name="board1">First board</param>
        /// <param name="board2">Second board</param>
        /// <returns>True if the same, false if not</returns>
        public static bool SameBoards(int[,] board1, int[,] board2)
        {
            if ((board1.GetLength(0) != board2.GetLength(0)) || (board1.GetLength(1) != board2.GetLength(1))) // not same length
            {
                return false;
            }

            for (int i = 0; i < board1.GetLength(0); i++)
            {
                for (int j = 0; j < board1.GetLength(1); j++)
                {
                    if (board1[i, j] != board2[i, j]) // not same values
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// The function groups all the values in a given line together
        /// </summary>
        /// <param name="line">Line to group its values</param>
        private static void GroupLineTogether(int[] line)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] == 0) // empty cell
                {

                    int nextValue = line[i + 1];
                    line[i] = nextValue; // moving next value to the current cell
                    line[i + 1] = 0; // reseting the next cell

                    if (nextValue != 0 && i != 0)
                    {
                        i = i - 2; // if the next cell was filled, there is a chance it should be moved even more than only once. so we check it again
                    }
                }
            }
        }

        /// <summary>
        /// The function merges the cells which have the same values
        /// </summary>
        /// <param name="line">Line to merge its values</param>
        /// <param name="pointsGained">Current amount of points gained</param>
        /// <param name="realPlay">True if there is need to check if the game was won, false if not</param>
        private static void MergeSameCellValues(int[] line, ref int pointsGained, bool realPlay)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                if ((line[i + 1] == line[i]) && line[0] != 0) // same values so need to merge
                {
                    line[i] *= 2; // multiplying by 2
                    line[i + 1] = 0; // reseting the other same cell
                    pointsGained += line[i];
                    if (line[i] == 2048 && realPlay) // the player won the game - reached 2048
                    {
                        Game.Status = GameStatus.Win;
                    }
                }
            }
        }

        /// <summary>
        /// The fucntion shifts all the values in the given line based on the given direction
        /// </summary>
        /// <param name="line">Line to shift</param>
        /// <param name="direction">Direction to move to</param>
        /// <param name="pointsGained">Current amount of points gained</param>
        /// <param name="realPlay">True if it's the player's move, false if not</param>
        /// <returns></returns>
        private static int[] ShiftLine(int[] line, Direction direction, ref int pointsGained, bool realPlay)
        {
            int[] shiftedLine = (int[])line.Clone();

            if (direction == Direction.Down || direction == Direction.Right) // down and right are in the opposite direction, so need to reverse before shifting
            {
                Array.Reverse(shiftedLine);
            }

            GroupLineTogether(shiftedLine); // group all the values together
            MergeSameCellValues(shiftedLine, ref pointsGained, realPlay); // merge the same values
            GroupLineTogether(shiftedLine); // group again (in cased there was a merge)

            if (direction == Direction.Down || direction == Direction.Right) // if down or right, reverse again
            {
                Array.Reverse(shiftedLine);
            }

            return shiftedLine;
        }

        /// <summary>
        /// The function updates the given row
        /// </summary>
        /// <param name="row">New row</param>
        /// <param name="index">Index of the row to update</param>
        private void UpdateRow(int[] row, int index)
        {
            for (int i = 0; i < row.Length; i++)
            {
                this.Data[index, i] = row[i];
            }
        }

        /// <summary>
        /// The function updates the given col
        /// </summary>
        /// <param name="col">New col</param>
        /// <param name="index">Index of the col to update</param>
        private void UpdateCol(int[] col, int index)
        {
            for (int i = 0; i < col.Length; i++)
            {
                this.Data[i, index] = col[i];
            }
        }
    }
}
