using System;

namespace Felli
{
    /// <summary>
    /// The tile class. Manages a certain tile's state, index, and neighbours.
    /// </summary>
    public class Tile
    {
        // The index position of a certain tile.
        public int index;

        // The tile's state (Empty, Black, or White)
        public Tilestate State{get; set;}

        // An array of tiles signalizing this tile's neighbours.
        public Tile[] Neighbours{get; set;}

        /// <summary>
        /// The class constructor. Only initializes the index position.
        /// </summary>
        /// <param name="index">The index position to be swapped.</param>
        public Tile(int index)
        {
            this.index = index;
        }

        /// <summary>
        /// A method that observes if a player can move between a tile.
        /// </summary>
        /// <param name="targetTile">The tile that the player wants to move to
        /// </param>
        /// <param name="playerState">The current player calling the method
        /// </param>
        /// <returns>A movelist enum refering the possibilities (Impossible to
        /// move), (Possible to move), (If can jump over enemy)</returns>
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

        /// <summary>
        /// Checks if a certain tile is surrounded (adjacent tiles are not
        /// empty)
        /// </summary>
        /// <returns>A bool that determines if it is surrounded or not</returns>
        public bool IsSurrounded()
        {
            // Begins the bool stating that its true, and can turn it false 
            // at the rest of the method.
            bool isSurronded = true;

            // A foreach looping through the tile's neighbours
            foreach (Tile t in Neighbours)
            {
                // In case the tile is null, continues the loop
                if (t == null)
                    continue;

                // If the tilestate of any neighbour is empty, it is not 
                // surrounded
                if (t.State == Tilestate.Empty)
                {
                    isSurronded = false;
                    break;
                }
            }

            return isSurronded;
        }

        /// <summary>
        /// Obtains the tile between two tiles.
        /// </summary>
        /// <param name="target">The target tile that will be compared alongside
        /// the instanced tile</param>
        /// <param name="playerState">Verifies which player is calling the
        /// method</param>
        /// <returns>The tile between</returns>
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


        /// <summary>
        /// Observes the possibilities of tiles in-between two positions.
        /// </summary>
        /// <param name="pos1">The first position to be compared</param>
        /// <param name="pos2">The second position to be compared</param>
        /// <returns>A bool that returns true if there is a tile
        /// in-between, or false if there is not.</returns>
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

            if(pos1.Row != pos2.Row)
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