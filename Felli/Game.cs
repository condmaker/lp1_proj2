using System;

namespace Felli
{
    /// <summary>
    /// The game class. Treats of the game loop and game logic (player turns,
    /// game state, control).
    /// </summary>
    public class Game
    {
        // New instance of the board, to be used of the whole instance
        private Board gameBoard = new Board();
        // New instance of UI, to be used of the whole instance
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

                // Enters certain methods depending on player input
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
        /// The main game loop of the program.
        /// </summary>
        private void BeginGame()
        {
            // A simple ushort to store numbers returned by certain methods
            // to verify player input.
            ushort inputCheck;

            // Used before the game starts to store the selected player's input
            // of chosen color.
            Tilestate playerColor;

            // Will be used to store tiles selected by the player's input.
            Tile currentPos;
            Tile nextPos;
            
            // Shows the starting board
            userInterface.ShowBoard(gameBoard);
            
            // Updates the playerColor variable with the player's input
            playerColor = userInterface.BeginningLoop();

            // If the previous method returned empty, leaves the method and
            // consequently the program
            if (playerColor == Tilestate.Empty) return;

            // Selects the first player to play
            gameBoard.SelectPlayersTurn(playerColor);

            // The main game loop. Will run while the user's input is different
            // than 'q' and the GameOver property from this instances' 
            // game board returns true (game still not over)
            while ((userInterface.Input != "q") && !gameBoard.IsGameOver)
            {
                // Prints the starting messages and records the player's input,
                // and splits it accordingly
                userInterface.MessageTurn(gameBoard.Turn, gameBoard.NextTurn);
                userInterface.ShowBoard(gameBoard);
                userInterface.MessageCommands();

                userInterface.WriteOnString();
                userInterface.SplitString();

                // Observes if the first input from the player is equal to 
                // 'choose' or pass, with the returning code from the input
                // determining it.
                inputCheck = userInterface.InputFirstCheck("choose", "pass");

                // If input equaled 'q'.
                if (inputCheck == 1)
                    break;

                // If input check failed.
                else if (inputCheck == 2)
                {
                    userInterface.ErrorMessage(ErrorCode.IllOpt);
                    continue;
                }

                // If input check was 'pass'. Increments the turn, passing the
                // player's turn.
                else if (inputCheck == 3)
                {
                    gameBoard.Turn++;
                    continue;
                }

                // Converts the position in the input to a tile in the 
                // current board.
                currentPos = gameBoard.GetTile(
                    ConvertStringToPos(userInterface.SplitInput[1]));

                // Checks if the chosen piece is surrounded and warns the player
                if (currentPos.IsSurrounded())
                {
                    userInterface.MessageSurroundWarning();
                }
                
                // Shows the player that the piece was successfully chosen,
                // And records the input again to move it.
                userInterface.MessagePieceChosen();

                userInterface.WriteOnString();
                userInterface.SplitString();

                inputCheck = userInterface.InputFirstCheck("move", "pass");

                if (inputCheck == 1)
                    break;
                else if (inputCheck == 2)
                {
                    userInterface.ErrorMessage(ErrorCode.IllOpt);
                    continue;
                }
                else if (inputCheck == 3)
                {
                    gameBoard.Turn++;
                    continue;
                }

                // Converts the position in the input to a tile in the 
                // current board.
                nextPos = gameBoard.GetTile(
                    ConvertStringToPos(userInterface.SplitInput[1]));
                
                // Updates the current board with the new information.
                UpdateGame(currentPos, nextPos, gameBoard.NextTurn);
            }

            // After the loop is over, checks if the game was ended and 
            // prints who won if so.
            if (gameBoard.IsGameOver == true)
                userInterface.MessageWinGame(gameBoard.Winner);

        }

        /// <summary>
        /// Will convert a string with a number index to a position.
        /// </summary>
        /// <param name="strCoord">Contains the index position to be converted
        /// </param>
        /// <returns>The position in Row/Column of the given index.</returns>
        private Position ConvertStringToPos(string strCoord)
        {
            // Will store the number in strCoord
            int posIndex;
            // A position that will have the current index Row/Column position
            Position posCoord = new Position(0, 0);

            // Parses the string to posIndex
            posIndex = int.Parse(strCoord);

            // Converts the index in posCoord to R/C position.
            posCoord.IndToPos(posIndex);

            return posCoord;
        }


        /// <summary>
        /// Observes the given tiles made by player input and updates the game
        /// board accordingly
        /// </summary>
        /// <param name="currentTile">The current tile that the chosen player
        /// piece is.</param>
        /// <param name="afterTile">The tile that the chosen player piece
        /// desires to move to.</param>
        /// <param name="currentPlayer">Will observe the current player to
        /// correctly determine who is the enemy tile</param>
        private void UpdateGame(
            Tile currentTile, Tile afterTile, Tilestate currentPlayer)
        {

            switch(currentTile.CanMoveBetweenTile(afterTile, currentPlayer))
            {
                case MoveList.Impossible:
                    userInterface.ErrorMessage(ErrorCode.IllMove);

                    break;
                case MoveList.Possible:
                    gameBoard.UpdateSimple(
                        currentTile, afterTile);

                    gameBoard.Turn++;
                    break;
                case MoveList.Enemy:
                    gameBoard.UpdateEnemy(
                        currentTile, afterTile);
                        
                    gameBoard.Turn++;
                    break;
            }
        }
    }
}