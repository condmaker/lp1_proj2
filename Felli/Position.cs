using System;

namespace Felli
{
    /// <summary>
    /// The tile class.
    /// </summary>
    public class Position
    {
        public int Row { get; private set;}
        public int Col { get; private set;}
        
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

    
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
                    //center
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