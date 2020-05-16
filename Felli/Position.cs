using System;

namespace Felli
{
    /// <summary>
    /// A class that manages position and position conversion by the index of
    /// the board.
    /// </summary>
    public class Position
    {
        // Row of the current instance position
        public int Row { get; private set;}
        // Column of the current instance position
        public int Col { get; private set;}
        
        /// <summary>
        /// Constructor of the class. Initializes the instance's row and column.
        /// </summary>
        /// <param name="row">Will determine the row of this instance.</param>
        /// <param name="col">Will determine the column of this instance.
        /// </param>
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// A method that converts a tile index to a Row/Column based position.
        /// </summary>
        /// <param name="ind">The given tile index to be analyzed.</param>
        /// <returns>Returns itself, the instance of the position with the
        /// correct row and column by the analyzed index.</returns>
        public Position IndToPos(int ind)
        {
            switch(ind)
            {
                case 0:
                    Col = 0;
                    Row = 0;
                    break;
                case 1:
                    Col = 1;
                    Row = 0;
                    break;
                case 2:
                    Col = 2;
                    Row = 0;
                    break;
                case 3:
                    Col = 0;
                    Row = 1;
                    break;
                case 4:
                    Col = 1;
                    Row = 1;
                    break;
                case 5:
                    Col = 2;
                    Row = 1;
                    break;
                case 6:
                    // Center tile.
                    Col = 4;
                    Row = 4;
                    break;
                case 7:
                    Col = 0;
                    Row = 2;
                    break;
                case 8:
                    Col = 1;
                    Row = 2;
                    break;
                case 9:
                    Col = 2;
                    Row = 2;
                    break;
                case 10:
                    Col = 0;
                    Row = 3;
                    break;
                case 11:
                    Col = 1;
                    Row = 3;
                    break;
                case 12:
                    Col = 2;
                    Row = 3;
                    break;

            }
            return this; 
        }

        

    }
}