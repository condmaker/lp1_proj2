using System;

namespace Felli
{
    /// <summary>
    /// The game class treats of the game loop and game logic (player turns,
    /// game state, control).
    /// </summary>
    public class Game
    {
        // New instance of the board
        private Board gameBoard = new Board();
        // New instance of UI
        private UI userInterface = new UI();
        
        /// <summary>
        /// The 'Main Menu' game loop of the program.
        /// </summary>
        public void Initiate()
        {
            // Prints the Main Menu screen, with all the necessary commands
            // and inputs available to the player.
            userInterface.MainMenu();

            // Main game loop that runs while user Input is not 'q'.
            while (userInterface.Input != "q")
            {
                // Changes the string value
                userInterface.WriteOnString();

                // 
                switch (userInterface.Input)
                {
                    case "m":
                        userInterface.MainMenu();
                        break;
                    
                    case "t":
                        userInterface.ShowTutorial();
                        break;

                    case "s":
                        BeginGame();
                        break;

                    case "q":
                        break;

                    default:
                        userInterface.ErrorMessage(ErrorCode.UnkInput);
                        break;
                }
            }

            userInterface.MessageGoodbye();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BeginGame()
        {
            userInterface.ShowBoard(gameBoard, false);

            Tilestate playerColor;
            
            playerColor = userInterface.BeginningLoop();

            if (playerColor == Tilestate.Empty) return;

            gameBoard.SelectPlayersTurn(playerColor);
            
            while (userInterface.Input != "q")
            {
                userInterface.MessageTurn(gameBoard.Turn, gameBoard.NextTurn);
                userInterface.ShowBoard(gameBoard, false);

                userInterface.WriteOnString();
                // UpdateGame();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="afterPos"></param>
        /// <param name="currentPlayer"></param>
        private void UpdateGame(
            Position currentPos, Position afterPos, Tilestate currentPlayer)
        {
            Tile currentTile = gameBoard.GetTile(currentPos);
            Tile afterTile =   gameBoard.GetTile(afterPos);

            switch(currentTile.CanMoveBetweenTile(afterTile, currentPlayer))
            {
                case MoveList.Impossible:
                    userInterface.ErrorMessage(ErrorCode.IllMove);

                    break;
                case MoveList.Possible:
                    gameBoard.UpdateSimple(
                        currentTile, afterTile, currentPlayer);

                    gameBoard.Turn++;
                    break;
                case MoveList.Enemy:
                    gameBoard.UpdateEnemy(
                        currentTile, afterTile, currentPlayer);
                        
                    gameBoard.Turn++;
                    break;
            }
        }
    }
}