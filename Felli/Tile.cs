using System;

namespace Felli
{
    /// <summary>
    /// The tile class.
    /// </summary>
    public class Tile
    {
        
        public int index;

        public Tilestate State{get; private set;}

        public Tile[] Neighbours{get; set;}

        public Tile(int index)
        {
            this.index = index;
        }

    }
}