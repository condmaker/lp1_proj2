using System;

namespace Felli
{
    /// <summary>
    /// The Board class. Treats the logic and state of the game.
    /// </summary>
    public class Board
    {
        //por Private
        private Tile[,] corBoard;
        private Tile center;

        public int Turn{get; set;}
        //Type of player for each turn
        private Tilestate firstPlayer, secondPlayer;
        //Number of pieces in game of each color 
        private byte whiteNum, blackNum;


        //Property that returns the player that is currently playing 
        // DAR CRÉDITO PRO STOR NO FINAL
        public Tilestate NextTurn
        {
            get
            {
                if(GameOver)
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
        //Property that checks if the game is over or not
        public bool GameOver
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

        //Property that returns the player who won
        public Tilestate Winner
        {
            get
            {
                if(GameOver)
                {
                    if(whiteNum == 0 && blackNum >= 0)     
                    { 
                        return Tilestate.Black; 
                    }
                    else if(whiteNum == 0 && whiteNum >= 0)
                    { 
                        return Tilestate.Black;
                    }
                }
         
                return Tilestate.Empty;    
            }
        }


        //Constructor
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

            corBoard = new Tile[4,3]; 
           
            //Creates bidimensional array of tiles
            for(int y = 0; y < 4; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    corBoard[y,x] = new Tile( (y*3) + x );
                }
            }
            center = new Tile(12);

            PlacePieces();
            SetNeighbours();
        }


        /// <summary>
        /// 
        /// </summary>
        private void SetNeighbours()
        {       
            Tile[] centerAux = new Tile[6];

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
        /// 
        /// </summary>
        private void PlacePieces()
        {
            short i = -1; 
            foreach (Tile t in corBoard)
            {
                i++;

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
        /// <param name="coord"></param>
        /// <returns></returns>
        public Tile GetTile(Position coord)
        {
            if(coord.Col == 4){return center;}
            return corBoard[coord.Col, coord.Row];
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateSimple(Tile current, Tile after, Tilestate player)
        {
            current.State = Tilestate.Empty;
            after.State = player;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateEnemy(Tile current, Tile after, Tilestate player)
        {
            Tilestate aux;

            current.State = Tilestate.Empty;
            after.State = player;

            if (player == Tilestate.White)
                aux = Tilestate.Black;
            else 
                aux = Tilestate.White;

            current.GetTileBetween(after, player).State = aux;
        }

        /// <summary>
        /// Selects the players turns
        /// </summary>
        /// <param name="firstPlayer"></param>
        public void SelectPlayersTurn(Tilestate firstPlayer)
        {
            
            if(firstPlayer == Tilestate.Black)
            {
                //Selects Black to be the first player to play
                this.firstPlayer = firstPlayer;     
                secondPlayer = Tilestate.White;         
            }
            else if(firstPlayer == Tilestate.White)
            {
                //Selects White to be the first player to play
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