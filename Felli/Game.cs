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
            ushort inputCheck;

            Tilestate playerColor;

            Tile currentPos;
            Tile nextPos;
            
            userInterface.ShowBoard(gameBoard);
            
            playerColor = userInterface.BeginningLoop();

            if (playerColor == Tilestate.Empty) return;

            gameBoard.SelectPlayersTurn(playerColor);

            while ((userInterface.Input != "q") && !gameBoard.GameOver)
            {
                userInterface.MessageTurn(gameBoard.Turn, gameBoard.NextTurn);
                userInterface.ShowBoard(gameBoard);
                userInterface.MessageCommands();

                userInterface.WriteOnString();
                userInterface.SplitString();

                inputCheck = userInterface.InputFirstCheck("choose", "pass");

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

                // Will obtain the tile in the Game Board by the input
                currentPos = gameBoard.GetTile(
                    ConvertStringToPos(userInterface.SplitInput[1]));

                if (currentPos.IsSurrounded())
                {
                    userInterface.MessageSurroundWarning();
                }
                
                userInterface.MessagePieceChosen();

                userInterface.WriteOnString();
                userInterface.SplitString();

                inputCheck = userInterface.InputFirstCheck("move");

                if (inputCheck == 1)
                    break;
                else if (inputCheck == 2)
                {
                    userInterface.ErrorMessage(ErrorCode.IllOpt);
                    continue;
                }

                nextPos = gameBoard.GetTile(
                    ConvertStringToPos(userInterface.SplitInput[1]));
                
                UpdateGame(currentPos, nextPos, gameBoard.NextTurn);
            }

            if (gameBoard.GameOver == true)
                userInterface.MessageWinGame(gameBoard.Winner);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCoord"></param>
        /// <returns></returns>
        private Position ConvertStringToPos(string strCoord)
        {
            int posIndex;
            Position posCoord = new Position(0, 0);

            
            posIndex = int.Parse(strCoord);

            posCoord.IndToPos(posIndex);

            return posCoord;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentTile"></param>
        /// <param name="afterTile"></param>
        /// <param name="currentPlayer"></param>
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