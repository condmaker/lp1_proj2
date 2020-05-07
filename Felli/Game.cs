using System;

namespace Felli
{
    /// <summary>
    /// 
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
                        userInterface.ErrorMessage(0);
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BeginGame()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateGame()
        {

        }
    }
}