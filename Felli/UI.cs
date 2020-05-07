using System;

namespace Felli
{
    /// <summary>
    /// 
    /// </summary>
    public class UI
    {
        public string Input { get; private set; } = "";

        /// <summary>
        /// Will print the game's main screen with all game's main commands.
        /// </summary>
        public void MainMenu()
        {
            Console.WriteLine("dog");
        }

        /// <summary>
        /// Shows the game's tutorial.
        /// </summary>
        public void ShowTutorial()
        {

        }

        /// <summary>
        /// Changes the Input for the instance.
        /// </summary>
        public void WriteOnString()
        {
            Input = Console.ReadLine();
        }

        /// <summary>
        /// Obtains numerous error codes.
        /// </summary>
        /// <param name="errorNumb">Byte that represents the error code</param>
        public void ErrorMessage(byte errorNumb)
        {
            switch (errorNumb)
            {
                // Error Number 0
                // For unknown inputs.
                case 0:
                    Console.WriteLine("Invalid input.");
                    break;

                // Error Number 1
                //
                case 1:
                    break;
                
                // Error Number 2
                // 
                case 2:
                    break;
            }

            return;
        }
    }
}