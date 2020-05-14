using System;

namespace Felli
{
    /// <summary>
    /// The Board class. Treats the logic and state of the game.
    /// </summary>
    public class Board
    {
        // The 'game board' itself. An multidimensional array of tiles.
        private Tile[,] corBoard;
        // The odd tile of the board, the center one, at another instance.
        private Tile center;

        // The current turn. Can be incremented from the outside.
        public int Turn{get; set;}
        // Type of player for each turn
        private Tilestate firstPlayer, secondPlayer;
        // Number of pieces in game of each color 
        private byte whiteNum, blackNum;


        // Property that returns the player that is currently playing 
        // DAR CRÉDITO PRO STOR NO FINAL
        public Tilestate NextTurn
        {
            get
            {
                if(IsGameOver)
                {
                    return Tilestate.Empty;
                }
                else if(Turn % 2 == 0)
                {
                    return firstPlayer;
                }
                else
                {
                    return secondPlayer;
                }

            }
        }

        // Property that checks if the game is over or not
        public bool IsGameOver
        {
            get
            {
                if(whiteNum == 0 || blackNum == 0)
                {
                    return true;
                }
                else { return false; }
            }
        }

        // Property that returns the player who won
        public Tilestate Winner
        {
            get
            {
                if(IsGameOver)
                {
                    if(whiteNum == 0 && blackNum >= 0)     
                    { 
                        return Tilestate.Black; 
                    }
                    else if(blackNum == 0 && whiteNum >= 0)
                    { 
                        return Tilestate.White;
                    }
                }
         
                return Tilestate.Empty;    
            }
        }


        // Constructor of the class. Will always start at turn 0 with 6 pieces
        // to each player, and initializes the board.
        public Board()
        {
            whiteNum = 6;
            blackNum = 6;
            Turn = 0;
            CreateBoard();
        }

        /// <summary>
        /// Creates the board
        /// </summary>
        private void CreateBoard()
        {   

            // Initializes corBoard
            corBoard = new Tile[4,3]; 
           
            // Creates the bidimensional array of tiles in corBoard
            for(int y = 0; y < 4; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    if(y <= 1)
                        corBoard[y,x] = new Tile( (y*3) + x );
                    else
                        corBoard[y,x] = new Tile( 1 + (y*3) + x );
                }
            }

            // Defines the center tile separately
            center = new Tile(6);

            // Defines the pieces' Tilestate (color) in the board
            PlacePieces();
            // Sets neighbours for each tile
            SetNeighbours();
        }


        /// <summary>
        /// Sets the neighbours for each tile in a specific instance.
        /// </summary>
        private void SetNeighbours()
        {       
            // A tile array of 6 tiles.
            Tile[] centerAux = new Tile[6];

            // A double for loop that will verify each tile and check who is
            // adjacent to it.
            for(int y = 0; y < 4; y++)
            {
                for(int x = 0; x < 3; x++)
                { 
                    Tile[] aux = new Tile[4];
                    
                    //left
                    if(x > 0)
                    {
                        aux[0] = corBoard[y, x - 1];
                    }
                    //right
                    if(x < 2)
                    {                  
                        aux[1] = corBoard[y, x + 1];
                    }
                    //up
                    if(y == 1 || y == 3)
                    {                  
                        aux[2] = corBoard[y - 1, x];
                    }
                    //down
                    if(y == 0 || y == 2)
                    {            
                        aux[3] = corBoard[y + 1, x];
                    }
                    
                    if(y == 1)
                    {
                        aux[3] = center;
                        centerAux[((y*3) + x) - 3] = corBoard[y,x];
                    }
                    if(y == 2)
                    {
                        aux[2] = center;
                        centerAux[((y*3) + x) - 3] = corBoard[y,x];
                    }
                
                    corBoard[y,x].Neighbours = aux;
                }
           
                center.Neighbours = centerAux;
           
            }
        }

        /// <summary>
        /// Will define the initial state of each tile in the board
        /// </summary>
        private void PlacePieces()
        {
            // A number that starts at -1 so that the first increment puts it
            // at 0.
            short i = -1; 

            // A foreach loop for each tile in the board.
            foreach (Tile t in corBoard)
            {
                // Increments at the start so that it increments even if it
                // enters the if statement in this loop.
                i++;

                // If the i is less than 6, it means that the tile is a black 
                // one.
                if (i < 6)
                {
                    t.State = Tilestate.Black;
                    continue;
                }

                t.State = Tilestate.White;
            }
        }

        /// <summary>
        /// Get tile from coordinates from the board's tileset
        /// </summary>
        /// <param name="coord">The coordinate that will be analyzed to 
        /// obtain the tile</param>
        /// <returns>The given tile.</returns>
        public Tile GetTile(Position coord)
        {
            if(coord.Col == 4){return center;}
            return corBoard[coord.Row, coord.Col];
        }

        /// <summary>
        /// The simple update of the game board, when a player isn't 'jumping 
        /// over' an enemy piece.
        /// </summary>
        /// <param name="current">The current tile that will be swapped with an
        /// empty one</param>
        /// <param name="after">The after tile that will be swapped with the
        /// current player's state</param>
        /// <param name="player">Current player's state (black or white)</param>
        public void UpdateSimple(Tile current, Tile after, Tilestate player)
        {
            current.State = Tilestate.Empty;
            after.State = player;
        }

        /// <summary>
        /// The more complex update of the game board, if a player is going to
        /// jump over an enemy piece.
        /// </summary>
        /// <param name="current">The current tile that will be swapped with an
        /// empty one</param>
        /// <param name="after">The after tile that will be swapped with the
        /// current player's state</param>
        /// <param name="player">Current player's state (black or white</param>
        public void UpdateEnemy(Tile current, Tile after, Tilestate player)
        {
            current.State = Tilestate.Empty;
            after.State = player;
            // Tile where the enemy is, will be swapped with a empty one
            current.GetTileBetween(after, player).State = Tilestate.Empty;
        }

        /// <summary>
        /// Selects the players turns at the start of the game
        /// </summary>
        /// <param name="firstPlayer">The selected player's color, that will
        /// determine who begins and who plays at each turn.</param>
        public void SelectPlayersTurn(Tilestate firstPlayer)
        {
            
            if(firstPlayer == Tilestate.Black)
            {
                // Selects Black to be the first player to play
                this.firstPlayer = firstPlayer;     
                secondPlayer = Tilestate.White;         
            }
            else if(firstPlayer == Tilestate.White)
            {
                // Selects White to be the first player to play
                this.firstPlayer = firstPlayer;     
                secondPlayer = Tilestate.Black;        
            }
            else
            {
                //Error message probably
            }


        }
        

    }
}