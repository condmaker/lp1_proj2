using System;

namespace Felli
{
    /// <summary>
    /// The tile class.
    /// </summary>
    public class Tile
    {
        
        public int index;

        public Tilestate State{get; set;}

        
        public Tile[] Neighbours{get; set;}

        public Tile(int index)
        {
            this.index = index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetTile"></param>
        /// <param name="playerState"></param>
        /// <returns></returns>
        public int CanMoveBetweenTile(Tile targetTile, Tilestate playerState)
        {
            Position pos1 = new Position(0,0); 
            Position pos2 = new Position(0,0); 
            int canMove = 1;
            int aux = 0;

            foreach(Tile t in Neighbours)
            {
                if(t.index == targetTile.index)
                {
                    aux = 1;
                }
            }
            canMove = aux;
            
            if(canMove == 0)
            {
                foreach(Tile t in Neighbours)
                {
                    foreach(Tile s in targetTile.Neighbours)
                    {
                        if(s.index == t.index)
                        {
                            if(Math.Abs( pos1.IndToPos(index).Row - pos2.IndToPos(targetTile.index).Row )  == 1)
                            {
                                continue;
                            }

                            if(t.State != playerState)
                            {
                                canMove = 2;
                            }

                        }
                    }
                }
            }


            if(targetTile.State != Tilestate.Empty)
            {
                canMove = 0;
            }

            return canMove;
        }

    }
}