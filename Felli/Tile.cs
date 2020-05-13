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
                if (t == null)
                    continue;

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

        public bool IsSurrounded()
        {
            bool isSurronded = true;

            foreach (Tile t in Neighbours)
            {
                if (t == null)
                    continue;

                if (t.State == Tilestate.Empty)
                {
                    isSurronded = false;
                    break;
                }
            }

            return isSurronded;
        }

        public Tile GetTileBetween(Tile target, Tilestate playerState)
        {
            Tile betweenTile = null;
            Position pos1 = new Position(0,0); 
            Position pos2 = new Position(0,0); 
            
            foreach(Tile t in Neighbours)
            {
                if (t == null)
                    continue;

                foreach(Tile s in target.Neighbours)
                {
                    if (s == null)
                        continue;

                    if(s.index == t.index)
                    {
                        if(pos1.IndToPos(this.index).Row 
                            == pos2.IndToPos(target.index).Row)
                        {
                            continue;
                        }

                        if(pos1.IndToPos(this.index).Col 
                            == pos2.IndToPos(target.index).Col)
                        {
                            continue;
                        }

                        if(t.State != playerState)
                        {
                            Console.WriteLine("1");
                            betweenTile = t;
                        }

                    }
                }
            }

            
            return betweenTile;

        } 

    }
}