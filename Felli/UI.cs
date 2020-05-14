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
                "\nWelcome to Felli! PLAYER 1, please choose BLACK or WHITE " +
                "pieces with the 'choose <color>' command. This player " +
                "will also be the first one to begin.");
        }      

        /// <summary>
        /// Displays the possible game commands on screen.
        /// </summary>
        public void MessageCommands()
        {
            Console.WriteLine("\n| LIST OF COMMANDS |");
            Console.WriteLine(
                "| choose <index>   | Chooses an piece on the board.");
            Console.WriteLine(
                "| move <index>     | Moves a chosen piece to a determined " +
                "location.");
            Console.WriteLine(
                "| pass             | Passes current player's turn. \n");
            
        }
        
        /// <summary>
        /// Displays a message confirming the player has chosen a piece.
        /// </summary>
        public void MessagePieceChosen()
        {
            Console.WriteLine("Piece chosen!");
        }

        /// <summary>
        /// Warns the player about the possibility of the play being impossible.
        /// </summary>
        public void MessageSurroundWarning()
        {
            Console.WriteLine("WARNING: This piece is completely surrounded, " +
            "so there is the possibility of it being impossible to move. If "  +
            "you realize you have made that mistake, input whichever "         +
            "coordinate to reset the turn.");
        }

        /// <summary>
        /// Shows which player won at the end.
        /// </summary>
        /// <param name="winner">Defines who won the previous game.</param>
        public void MessageWinGame(Tilestate winner)
        {
            Console.WriteLine($"Congratulations, {winner} Player! You win!");
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
            Console.WriteLine($"\nTURN {turn}");
            Console.WriteLine($"{player} Piece Player round\n");
        }

        /// <summary>
        /// The starting loop of the game. Decides if PLAYER1 is black/white
        /// and assumes PLAYER2 as the opposite.
        /// </summary>
        /// <returns>A 'Return Code' to see if the user wants to continue 
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
            Board exampleBoard = new Board();
            Tile  exampleTile;

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
            Console.WriteLine("| empty board:                    |\n");

            // Show empty board here
            ShowBoard(exampleBoard, true);

            Console.WriteLine("\n| The player's pieces are         |");
            Console.WriteLine("| distributed evenly, white ones  |");
            Console.WriteLine("| on the downer triangle, and the |");
            Console.WriteLine("| black ones at the upper         |");
            Console.WriteLine("| triangle, with the middle being |");
            Console.WriteLine("| the only empty spot:            |\n");

            // Starting board here
            ShowBoard(exampleBoard);

            if (ContinueTutorial() == false) return;

            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("|            Game Rules           |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("| A player, on his turn, can      |");
            Console.WriteLine("| choose *one* piece to move. Out |");
            Console.WriteLine("| of all pieces (6 in total), you |");
            Console.WriteLine("| can only select and move one    |");
            Console.WriteLine("| per turn. The pieces can move   |");
            Console.WriteLine("| in two ways: to a adjacent and  |");
            Console.WriteLine("| empty spot, or 'jumping'        |");
            Console.WriteLine("| through an enemy piece in an    |");
            Console.WriteLine("| adjacent spot, and landing on   |");
            Console.WriteLine("| the 'back' tile of the same     |");
            Console.WriteLine("| direction, eliminating this     |");
            Console.WriteLine("| specific enemy piece. Here is   |");
            Console.WriteLine("| an example of this:             |\n");

            // Update the board to show favorable conditions
            exampleTile = exampleBoard.GetTile(new Position(1, 1));
            exampleTile.State = Tilestate.Empty;
            exampleTile = exampleBoard.GetTile(new Position(4, 4));
            exampleTile.State = Tilestate.Black;
            // Show the board
            ShowBoard(exampleBoard);

            Console.WriteLine("\n| Here we can see that the white  |");
            Console.WriteLine("| piece (marked as O) can 'jump'  |");
            Console.WriteLine("| over a black piece (marked as   |");
            Console.WriteLine("| 0, and by the way, the 'points' |");
            Console.WriteLine("| are empty tiles).               |");
            Console.WriteLine("| Now for the process of doing    |");
            Console.WriteLine("| doing this move:                |\n");

            // Update the board to show favorable conditions
            exampleTile = exampleBoard.GetTile(new Position(1, 1));
            exampleTile.State = Tilestate.White;
            exampleTile = exampleBoard.GetTile(new Position(4, 4));
            exampleTile.State = Tilestate.Empty;
            // Show the board
            ShowBoard(exampleBoard);

            Console.WriteLine("\n| It goes like that. As an        |");
            Console.WriteLine("| addendum, you can only          |");
            Console.WriteLine("| eliminate one piece at a time-- |");
            Console.WriteLine("| unlike some other board games.  |");
            Console.WriteLine("| As is obvious, too, you can't   |");
            Console.WriteLine("| eliminate a piece if the tile   |");
            Console.WriteLine("| you're going to land on         |");
            Console.WriteLine("| afterwards is occupied.         |");

            if (ContinueTutorial() == false) return;

            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("|           How to play           |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("| When the game begins, the       |");
            Console.WriteLine("| screen will show you the        |");
            Console.WriteLine("| current turn count, whose turn  |");
            Console.WriteLine("| it is, and the board itself.    |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| You can choose 3 inputs: the    |");
            Console.WriteLine("| one that will allow you to      |");
            Console.WriteLine("| pick one of your pieces in a    |");
            Console.WriteLine("| certain tile, one to move it    |");
            Console.WriteLine("| after choosing it, and one to   |");
            Console.WriteLine("| leave the game (you can leave   |");
            Console.WriteLine("| the program at all times using  |");
            Console.WriteLine("| q).                             |");
            Console.WriteLine("| To 'jump' over an enemy piece,  |");
            Console.WriteLine("| you need to select the tile     |");
            Console.WriteLine("| that the piece will be at       |");
            Console.WriteLine("| *after* 'jumping', which is not |");
            Console.WriteLine("| adjacent to the piece. Here is  |");
            Console.WriteLine("| the previous example, where the |");
            Console.WriteLine("| white piece jumps over the      |");
            Console.WriteLine("| black one:                      |\n");

            // Update the board to show favorable conditions
            exampleTile = exampleBoard.GetTile(new Position(1, 1));
            exampleTile.State = Tilestate.Empty;
            exampleTile = exampleBoard.GetTile(new Position(4, 4));
            exampleTile.State = Tilestate.Black;

            // Show the board
            ShowBoard(exampleBoard);

            Console.WriteLine("\n>move 4\n");

            // Update the board to show favorable conditions
            exampleTile = exampleBoard.GetTile(new Position(1, 1));
            exampleTile.State = Tilestate.White;
            exampleTile = exampleBoard.GetTile(new Position(4, 4));
            exampleTile.State = Tilestate.Empty;
            // Show the board
            ShowBoard(exampleBoard);
            
            Console.WriteLine("\n| Now, to answer a question that  |");
            Console.WriteLine("| may be pertinent: the other     |");
            Console.WriteLine("| 'board' to the right is a       |");
            Console.WriteLine("| helping image for you to locate |");
            Console.WriteLine("| each tile's position number, as |");
            Console.WriteLine("| you may have seen on this       |");
            Console.WriteLine("| previous example. It is present |");
            Console.WriteLine("| at all times, to help you input |");
            Console.WriteLine("| your commands without problems. |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| Now that all is explained, here |");
            Console.WriteLine("| are the exact commands:         |");
            Console.WriteLine("|                                 |");
            Console.WriteLine("| Choosing a Piece                |\n");
            Console.WriteLine(">choose <tile>                     \n");
            Console.WriteLine("| Moving a piece                  |\n");
            Console.WriteLine(">move <tile>                       \n");
            Console.WriteLine("| Choosing to NOT move            |\n");
            Console.WriteLine(">pass                              \n");

            if (ContinueTutorial() == false) return;

            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("|        Winning Conditions       |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("| In order to win the game, you   |");
            Console.WriteLine("| must eliminate all of the       |");
            Console.WriteLine("| enemies' pieces, with only      |");
            Console.WriteLine("| yours remaining on the board.   |");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("| You have reached the end of the |");
            Console.WriteLine("|            tutorial.            |");
            Console.WriteLine("-----------------------------------");
        }

        /// <summary>
        /// A simple bool function to work in tandem with ShowTutorial(). It
        /// asks if the player wants to quit the tutorial or continue it.
        /// </summary>
        /// <returns>Confirms if the input is to continue or
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
        /// <param name="comm1">First Argument to be compared to with the
        /// first input</param>
        /// <param name="comm2">Second Argument to be compared to with the
        /// second input</param>
        /// <returns>A Return Code to see if the user inputted correctly (0),
        /// incorrectly (2), or wants to leave (1)</returns>
        private ushort InputCheck(string comm1, string comm2)
        {
            // Checks if input is 'q', returning 1 to leave the program
            if (SplitInput[0] == "q") return 1;

            // Compares first input to first argument, returning 2 to go back
            // to the start of the loop
            else if (SplitInput[0] != comm1) 
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }

            // Compares first input to first argument, returning 2 to go back
            // to the start of the loop
            if (SplitInput[1].ToLower() != comm2)
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }

            // Returns 0 normally in case the inputs are equal
            return 0;
        }
        /// <summary>
        /// Will check if the splitted string has the correct inputs. Order of 
        /// arguments is important.
        /// </summary>
        /// <param name="comm1">First Argument to be compared to with the
        /// first input</param>
        /// <param name="comm2">Second Argument to be compared to with the
        /// second input</param>
        /// <param name="comm3">Third Argument to be compared to with the
        /// second input</param>
        /// <returns>A Return Code to see if the user inputted correctly (0),
        /// incorrectly (2), or wants to leave (1)</returns>
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
        /// Checks if only the first argument of the splitted input is equal
        /// to two given strings
        /// </summary>
        /// <param name="comm1">The first given string to be compared with the 
        /// first argument of the splitted input</param>
        /// <param name="comm2">The second given string to be compared with the 
        /// first argument of the splitted input</param>
        /// <returns>A Return Code to see if the user inputted correctly (0),
        /// incorrectly (2), is equal to the second argument (2) or wants to 
        /// leave (1)</returns>
        public ushort InputFirstCheck(string comm1, string comm2)
        {
            if (SplitInput[0] == "q") return 1;

            else if ((SplitInput[0] != comm1) && (SplitInput[0] != comm2))
            {
                ErrorMessage(ErrorCode.UnkInput);
                return 2;
            }
            else if (SplitInput[0] == comm2)
            {
                return 3;
            }
            
            return 0;
        }

        /// <summary>
        /// Prints the game board as a whole. If emptyMode is true, it will
        /// print an empty board.
        /// </summary>
        /// <param name="board">The board class instance to be printed.</param>
        /// <param name="emptyMode">Defines if the board is printed empty or 
        /// not. Can be omitted to be printed normally, but 'true' will print
        /// an empty board regardless of state.</param>
        public void ShowBoard(Board board, bool emptyMode = false)
        {
            // Creates a new position to record the current tile being executed
            // on the loop
            Position pos = new Position(0,0);
            // Will record respective tile character to be printed
            char displayChar = StateToChar(Tilestate.Empty);

            for(int i = 0; i < 13; i++)
            {           

                // Records current tile state of given board in a specific
                // char, if emptyMode is false.
                if(!emptyMode)
                {
                    pos.IndToPos(i);
                    displayChar = StateToChar(board.GetTile(pos).State);
                }

                // First column
                if(i < 3)
                {
                    if((i + 1) % 3 == 0)
                    {
                        Console.Write($"  {displayChar}  ");
                        Console.WriteLine("    |   0    1    2  ");
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write($"  {displayChar}  ");
                    }

                }

                // Second column
                else if(i < 6)
                {
                    if((i + 1) % 3 == 0)
                    {
                        Console.Write($" {displayChar} ");
                        Console.WriteLine("       |    3   4   5 ");
                    }
                    else
                    {
                        Console.Write($" {displayChar} ");
                    }
                }

                // Third column
                else if(i == 6)
                {
                    Console.Write($"       {displayChar}       ");
                    Console.WriteLine("    |        6     ");
                    Console.Write("   ");
                }

                // Fourth column
                else if(i < 10)
                {
                    if(i % 3 == 0)
                    {
                        Console.Write($" {displayChar} ");
                        Console.WriteLine("       |    7   8   9 ");
                    }
                    else
                    {
                        Console.Write($" {displayChar} ");
                    }
                }
                
                // Fifth column
                else 
                {
                    if(i % 3 == 0)
                    {
                        Console.Write($"  {displayChar}  ");
                        Console.WriteLine("    |  10   11    12  ");
                    }
                    else
                    {
                        Console.Write($"  {displayChar}  ");
                    }
                }
          
            }

        }


        /// <summary>
        /// Used in ShowBoard(). Observes the tile state and transforms it
        /// in a char symbol.
        /// </summary>
        /// <param name="state">A specific tile's state to decide what char
        /// it will return</param>
        /// <returns>A char representing a tile.</returns>
        private char StateToChar(Tilestate state)
        {
            // Initializes the char that will be returned
            char displayChar = ' ';

            switch(state)
            {
                case Tilestate.Empty:
                    displayChar = '.';
                    break;

                case Tilestate.Black:
                    displayChar = '0';
                    break;

                case Tilestate.White:
                    displayChar = 'O';
                    break;
            }
            
            return displayChar;
        }

        /// <summary>
        /// Changes the instance Input.
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

            if (SplitInput.Length > 4) 
            {
                Array.Clear(SplitInput, 0, SplitInput.Length);
                ErrorMessage(ErrorCode.UnkInput);
            }
        }

        /// <summary>
        /// Obtains numerous error codes.
        /// </summary>
        /// <param name="errorNumb">Represents the given error code to be 
        /// analyzed.</param>
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
                // For illegal moves where a piece cannot move.
                case ErrorCode.IllMove:
                    Console.WriteLine(
                        "Illegal move. You cannot move to this position.");
                    break;

                // Error Number 3
                // For illegal options where there is not a player piece on the
                // position.
                case ErrorCode.IllOpt:
                    Console.WriteLine(
                        "Illegal option. You don't have a piece in this " +
                        "position.");
                    break;
                    
                default:
                    break;
            }

            return;
        }
    }
}