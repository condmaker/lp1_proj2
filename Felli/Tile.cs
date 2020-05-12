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
        public MoveList CanMoveBetweenTile(
            Tile targetTile, Tilestate playerState)
        {
            MoveList canMove = MoveList.Possible;
            MoveList aux = MoveList.Impossible;

            foreach(Tile t in Neighbours)
            {
                if(t.index == targetTile.index)
                {
                    aux = MoveList.Possible;
                }
            }
            canMove = aux;
            
            if(canMove == MoveList.Impossible)
            {
               if(GetTileBetween(targetTile, playerState) != null)
               {
                   canMove = MoveList.Enemy;
               }
            }


            if(targetTile.State != Tilestate.Empty)
            {
                canMove = MoveList.Impossible;
            }

            return canMove;
        }

        public Tile GetTileBetween(Tile target, Tilestate playerState)
        {
            Tile betweenTile = null;
            Position pos1 = new Position(0,0); 
            Position pos2 = new Position(0,0); 
            
            foreach(Tile t in Neighbours)
            {
                foreach(Tile s in target.Neighbours)
                {
                    if(s.index == t.index)
                    {
                        if(Math.Abs( pos1.IndToPos(index).Row - pos2.IndToPos(target.index).Row )  == 1)
                        {
                            continue;
                        }

                        if(t.State != playerState)
                        {
                            betweenTile = t;
                        }

                    }
                }
            }

            return betweenTile;

        } 

    }
}