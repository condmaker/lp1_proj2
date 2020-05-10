using System;

namespace Felli
{
    /// <summary>
    /// The User Interface class. Treats of visual components and inputs.
    /// </summary>
    public class UI
    {
        // The single string that manages all inputs on the game.
        public string Input { get; private set; } = "";
        // A splitted array of strings used to read commands.
        public string[] SplitInput { get; private set;} 

        /// <summary>
        /// Displays the start message.
        /// </summary>
        private void MessageStart()
        {
            Console.WriteLine(
                "Welcome to Felli! PLAYER 1, please choose BLACK or WHITE " +
                "pieces with the 'choose <color>' command. This player " +
                "will also be the first one to begin.");
        }      

        /// <summary>
        /// Displays the endgame message.
        /// </summary>
        public void MessageGoodbye()
        {
            Console.WriteLine("Thanks for playing, see ya!");
        }

        /// <summary>
        /// Displays the current turn information.
        /// </summary>
        /// <param name="turn">The current numeric turn</param>
        /// <param name="player">The player of this turn</param>
        public void MessageTurn(int turn, Tilestate player)
        {
            Console.WriteLine($"TURN {turn}");
            Console.WriteLine($"{player} Piece Player round");
        }

        /// <summary>
        /// The starting loop of the game. Decides if PLAYER1 is black/white
        /// and assumes PLAYER2 as the opposite.
        /// </summary>
        /// <returns>A ushort Return Code to see if the user wants to continue 
        /// (0) or leave the program (1).</returns>
        public Tilestate BeginningLoop()
        {
            ushort returnCode;

            while (true)
            {
                // Display the first game message
                MessageStart();
                // Rewrites the string
                WriteOnString();
                // Separates the string
                SplitString();
                // Checks if the inputs match
                returnCode = InputCheck("choose", "black", "white");

                // Observes the Return Code's value
                if (returnCode == 1) return Tilestate.Empty;
                else if (returnCode == 2) continue;

                if (SplitInput[1] == "black") return Tilestate.Black;
                else return Tilestate.White;
            }
        }


        /// <summary>
        /// Will print the game's main screen with all of the game's main
        /// commands.
        /// </summary>
        public void MainMenu()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("|              Felli              |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("| m - Print this menu again       |");
            Console.WriteLine("| t - See the game's tutorial     |");
            Console.WriteLine("| s - Start the game              |");
            Console.WriteLine("| q - Exit                        |");
            Console.WriteLine("-----------------------------------");

        }

        /// <summary>
        /// Shows the game's tutorial.
        /// </summary>
        public void ShowTutorial()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("|           Introduction          |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("| Welcome to Felli! This a two    |");
            Console.WriteLine("| player board game, where each   |");
            Console.WriteLine("| player controls a set of black  |");
            Console.WriteLine("| or white pieces. The first      |");
            Console.WriteLine("| player will choose what color   |");
            Console.WriteLine("| he wants to play as, and the    |");
            Console.WriteLine("| second player will              |");
            Console.WriteLine("| automatically be assigned the   |");
            Console.WriteLine("| other color. After that it will |");
            Console.WriteLine("| be asked who wants to play      |");
            Console.WriteLine("| first.                          |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| Each set has 6 pieces each, and |");
            Console.WriteLine("| they are evenly distributed     |");
            Console.WriteLine("| across the board. The board     |");
            Console.WriteLine("| layout is quite peculiar, since |");
            Console.WriteLine("| it consists of 7 points (tiles) |");
            Console.WriteLine("| forming an isosceles triangle,  |");
            Console.WriteLine("| one on each side. Since it is   |");
            Console.WriteLine("| quite difficult to clearly      |");
            Console.WriteLine("| explain, here is an example of  |");
            Console.WriteLine("| empty board:                    |");

            // Show empty board here

            Console.WriteLine("| The player's pieces are         |");
            Console.WriteLine("| distributed evenly, white ones  |");
            Console.WriteLine("| on the downer triangle, and the |");
            Console.WriteLine("| black ones at the upper         |");
            Console.WriteLine("| triangle, with the middle being |");
            Console.WriteLine("| the only empty spot:            |");

            // Starting board here

            if (ContinueTutorial() == false) return;
        }

        /// <summary>
        /// A simple bool function to work in tandem with ShowTutorial(). It
        /// asks if the player wants to quit the tutorial or continue it.
        /// </summary>
        /// <returns>A boolean confirming if the input is to continue or
        /// to quit.</returns>
        private bool ContinueTutorial()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("|      Press 'c' to continue,     |");
            Console.WriteLine("|   or anything else to go back.  |");
            Console.WriteLine("-----------------------------------");

            WriteOnString();

            if (Input == "c") return true;
            return false;
        }

        /// <summary>
        /// Will check if the splitted string has the correct inputs. Order of 
        /// arguments is important.
        /// </summary>
        /// <param name="comm1">First Argument</param>
        /// <param name="comm2">Second Argument</param>
        /// <returns>A ushort Return Code to see if the user inputted 
        /// correctly (0), incorrectly(2), or wants to leave (1)</returns>
        private ushort InputCheck(string comm1, string comm2)
        {
            if (SplitInput[0] == "q") return 1;
            else if (SplitInput[0] != comm1) 
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }

            if (SplitInput[1].ToLower() != comm2)
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }

            return 0;
        }
        /// <summary>
        /// Will check if the splitted string has the correct inputs. Order of 
        /// arguments is important.
        /// </summary>
        /// <param name="comm1">First Argument</param>
        /// <param name="comm2">Second Argument</param>
        /// <param name="comm3">Third Argument</param>
        /// <returns>A ushort Return Code to see if the user inputted 
        /// correctly (0), incorrectly(2), or wants to leave (1)</returns>
        private ushort InputCheck(string comm1, string comm2, string comm3)
        {
            if (SplitInput[0] == "q") return 1;
            else if (SplitInput[0] != comm1) 
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }

            if (SplitInput[1].ToLower() != comm2 && 
            SplitInput[1].ToLower() != comm3)
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// Changes the Input for the instance.
        /// </summary>
        public void WriteOnString()
        {
            Console.Write(">");
            Input = Console.ReadLine();
        }

        /// <summary>
        /// Splits a string in various ones, separated by spaces. In case the
        /// resulting array of strings has more than 3 elements, the array
        /// will be emptied and the UnkInput Error Code will be shown.
        /// </summary>
        public void SplitString()
        {
            SplitInput = Input.Split(" ");

            if (SplitInput.Length > 3) 
            {
                Array.Clear(SplitInput, 0, SplitInput.Length);
                ErrorMessage(ErrorCode.UnkInput);
            }
        }

        /// <summary>
        /// Obtains numerous error codes.
        /// </summary>
        /// <param name="errorNumb">ErrorCode enumerate that represents the 
        /// error code</param>
        public void ErrorMessage(ErrorCode errorNumb)
        {
            switch (errorNumb)
            {
                // Error Number 0
                // For unknown inputs.
                case ErrorCode.UnkInput:
                    Console.WriteLine("Unknown input.");
                    break;

                // Error Number 1
                //
                case ErrorCode.IllMove:
                    Console.WriteLine("Illegal move.");
                    break;
                    
                default:
                    break;
            }

            return;
        }
    }
}