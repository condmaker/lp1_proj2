using System;

namespace Felli
{
    /// <summary>
    /// The Board class. Treats the logic and state of the game.
    /// </summary>
    public class Board
    {
        //Talvez fique public
        private Tilestate[] board;
        private int turn;
        //Type of player for each turn
        private Tilestate firstPlayer, secondPlayer;
        //Number of pieces in game of each color 
        private byte whiteNum, blackNum;


        //Property that returns the player that is currently playing 
        public Tilestate NextTurn
        {
            get
            {
                if(GameOver)
                {
                    return Tilestate.Empty;
                }
                else if(turn % 2 == 0)
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
                return false;
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


        public Board()
        {
            board = new Tilestate[13];
            turn = 0;
        }

        //Selects the players turns
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