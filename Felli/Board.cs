using System;

namespace Felli
{
    /// <summary>
    /// The Board class. Treats the logic and state of the game.
    /// </summary>
    public class Board
    {
        public Tilestate[] board;
        private int turn;
        private Tilestate firstPlayer, secondPlayer;

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
            else if(firstPlayer == Tilestate.Black)
            {
                //Selects White to be the first player to play
                this.firstPlayer = firstPlayer;     
                secondPlayer = Tilestate.White;        
            }
            else
            {
                //Error message probably
            }


        }
        


    }
}