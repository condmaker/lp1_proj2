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
            Position selfPos = new Position(0,0).IndToPos(index); 
            Position targetPos = new Position(0,0).IndToPos(target.index); 
            Position betweenPos = new Position(0,0); 
            


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
                        bool aux = false;
                        betweenPos.IndToPos(s.index);


                        if(betweenPos.Row == targetPos.Row
                        && betweenPos.Row == selfPos.Row) 
                        {
                            aux = true;
                        }
                        
                        if(CheckBetweenPossibilities(targetPos, selfPos))
                        {
                            aux = true;
                        }

                        if(aux)
                        {
                            if(t.State != playerState)
                            {
                                betweenTile = t;
                            }
                        }

                    }
                }
            }

            
            return betweenTile;

        } 


        public bool CheckBetweenPossibilities(Position pos1, Position pos2)
        {
            string colString = pos1.Col + "" + pos2.Col;
            string rowstring = pos1.Row + "" + pos2.Row;

            bool aux = false;

            switch(rowstring)
            {
                case "04":
                    aux = true;
                    break;
                case "40":
                    aux = true;
                    break;
                case "43":
                    aux = true;
                    break;
                case "34":
                    aux = true;
                    break; 
            }

            switch(colString)
            {
                case "02":
                    aux = true;
                    break;
                case "20":
                    aux = true;
                    break;
                case "11":
                    aux = true;
                    break;
            }

            
            return aux;
        }



    }
}