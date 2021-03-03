using System;

namespace Johny_Scripten_4_Eindproduct
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.ResetColor();
            //variables for tracking the score and goal score
            int score = 0;
            int goalScore = 15;

            //variable to set how objects in the game look, you can change the tile and wall sprites here, the other sprites are set in-game
            string prizeSprite = "";
            string playerSprite = "";
            string tileSprite = "  ";
            string wallSprite = "[]";

            //variable for tracking how many attempts the player made on typing the correct amount of characters
            int attempt1 = 0;
            int attempt2 = 0;

            //gamestate and timer
            bool gameStarted = false;
            float finalScore;
            float highScore = -1;
            float startTime = 0;
            float endTime;
            bool wantToPlay = true;

            //input
            string pressedKey = "";

            //random seed set
            Random rnd = new Random(5873495);

            //ask player to set their looks, response changes based on how many attempts the player has made to enter 2 characters
            while (playerSprite.Length != 2)
            {
                switch (attempt1)
                {
                    case 0:
                        Console.WriteLine("Hey, Welcome to the game!");
                        Console.WriteLine("Please customize your character. You need to enter 2 characters, your character will be those 2 characters.");
                        playerSprite = Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Come on, you can do it! Just enter 2 characters!");
                        playerSprite = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("I just told you, you need to enter 2 characters! It's not that hard, try something like XX or &&!");
                        playerSprite = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Alright fine! If you won't listen, I'll set the sprites myself!");
                        playerSprite = "00";
                        break;
                }
                attempt1++;
            }

            //ask player to set their prize, response changes based on how many attempts the player has made to enter 2 characters, in both this and the previous question
            while (prizeSprite.Length != 2)
            {
                switch (attempt2)
                {
                    case 0:
                        if (attempt1 <= 2)
                        {
                            Console.WriteLine("Good, good. Now, please customize the prize you want to go after.");
                            prizeSprite = Console.ReadLine();
                        }
                        else if (attempt1 == 3)
                        {
                            Console.WriteLine("Finally. Now, please customize the prize you want to go after. Just like before, enter 2 characters, no more, no less.");
                            prizeSprite = Console.ReadLine();
                        }
                        else
                        {
                            prizeSprite = "%%";
                        }
                        break;
                    case 1:
                        if (attempt1 < 3)
                        {
                            Console.WriteLine("We've went through this before, just enter 2 characters, like HF or RC.");
                            prizeSprite = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Come on dude! Look, we did this before, remember? When you had to enter 2 characters for your character? Just do the same here!");
                            prizeSprite = Console.ReadLine();
                        }
                        break;
                    default:
                        Console.WriteLine("...I'll just set the sprite myself.");
                        prizeSprite = "XX";
                        break;
                }
                attempt2++;
            }

            //player instructions
            if (attempt1 <= 3 && attempt2 <= 2)
            {
                Console.WriteLine("Okay, with that out of the way, let's start the game!");
                Console.WriteLine("The goal is to collect the prizes you set, so in your case, " + prizeSprite + ".");
                Console.WriteLine("You can move by pressing wasd keys, so w is up, a is left, etc.");
                Console.WriteLine("The prizes will randomly appear on the board, try to get " + goalScore + " points as fast as possible!");
                Console.WriteLine("Good luck! Press enter to continue.");
            }
            else
            {
                Console.WriteLine("....Okay, with that out of the way, let's start the game.");
                Console.WriteLine("The goal is to collect the prizes you were supposed to set, so in your case, " + prizeSprite + ".");
                Console.WriteLine("You can move by pressing wasd keys, so w is up, a is left, etc.");
                Console.WriteLine("The prizes will randomly appear on the board. Try to get " + goalScore + " points as fast as possible, press enter to continue.");
            }

            //game loop
            while (wantToPlay)
            {
                //variable set here so they get reset after each game
                int phase = 1;

                //variable for width of playfield
                int playFieldHeight = 10;

                //variable for width of playfield
                int playFieldWidth = 10;

                //variable for tracking player location
                int playerX = 3;
                int playerY = 4;
                int oldPlayerX;
                int oldPlayerY;

                //variable for tracking if a prize is currently in the game
                bool PrizeSpawned = false;
                score = 0;

                //variable for tracking location of the prize
                int prizeX = 0;
                int prizeY = 0;

                //single game
                while (score < goalScore)
                {

                    //makes what the player types invisible
                    Console.ForegroundColor = Console.BackgroundColor;
                    ConsoleKeyInfo pKey = Console.ReadKey();
                    pressedKey = pKey.Key.ToString().ToLower();

                    //starts the timer if ran for the first time
                    if (!gameStarted)
                    {
                        startTime = System.Environment.TickCount;
                        gameStarted = true;
                    }

                    //clears the console, so new lines can be written like it's a new frame of the same game
                    Console.Clear();

                    //generate playing field, including border
                    string[][] playField = new string[playFieldHeight][];
                    int y = 0;
                    while (y < playField.Length)
                    {
                        playField[y] = new string[playFieldWidth];
                        int x = 0;
                        if (y == 0 || y == playField.Length - 1)
                        {
                            while (x < playField[y].Length)
                            {
                                playField[y][x] = wallSprite;
                                x++;
                            }
                        }
                        if (y == 9 && phase == 3)
                        {
                            while (x < playField[y].Length)
                            {
                                if ((x != 13 && x != 14) && y != 0 && y != playField.Length - 1)
                                {
                                    playField[y][x] = wallSprite;
                                }
                                else
                                {
                                    playField[y][x] = tileSprite;
                                }
                                x++;
                            }
                        }

                        while (x < playField[y].Length && y != 0 && y != playField.Length - 1)
                        {
                            //prevents player from being overwritten
                            if (playField[y][x] == playerSprite)
                            {
                                playField[y][x] = playerSprite;
                            }
                            else if ((x == 0 || x == playField[y].Length - 1) && phase == 1)
                            {
                                playField[y][x] = wallSprite;
                            }
                            else if ((x == 0 || x == playField[y].Length - 1 || (x == 9 && y != 5 && y != 4)) && phase == 2)
                            {
                                playField[y][x] = wallSprite;
                            }
                            else if ((x == 0 || x == playField[y].Length - 1 || (x == 9 && y != 5 && y != 4 && y != 13 && y != 14)) && phase == 3)
                            {
                                playField[y][x] = wallSprite;
                            }
                            else
                            {
                                playField[y][x] = tileSprite;
                            }
                            x++;
                        }
                        y++;
                    }

                    //store old location of player, in case the player touches a wall
                    oldPlayerX = playerX;
                    oldPlayerY = playerY;

                    //check which key was pressed, and move the player accordingly (wasd controls)
                    switch (pressedKey)
                    {
                        case "w":
                            playerY -= 1;
                            break;
                        case "a":
                            playerX -= 1;
                            break;
                        case "s":
                            playerY += 1;
                            break;
                        case "d":
                            playerX += 1;
                            break;
                        default:
                            break;

                    }

                    //if the player touches a wall, move them back to their old location
                    if (playField[playerY][playerX] == wallSprite)
                    {
                        playerX = oldPlayerX;
                        playerY = oldPlayerY;
                    }

                    //if the player is touching a prize, increase the score, request a new prize and change state of the game if applicable
                    if (playerY == prizeY && playerX == prizeX)
                    {
                        playField[oldPlayerY][oldPlayerX] = playerSprite;
                        score += 1;
                        PrizeSpawned = false;

                        if (score > 9)
                        {
                            playFieldHeight = 19;
                            phase = 3;
                        }
                        else if (score > 4)
                        {
                            playFieldWidth = 19;
                            phase = 2;
                        }

                    }

                    //write score on the screen
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Your score is: " + score);

                    //empty old location of the player
                    playField[oldPlayerY][oldPlayerX] = tileSprite;

                    //set new location of the player
                    playField[playerY][playerX] = playerSprite;


                    //if a prize isn't in the game, spawn a new one
                    if (!PrizeSpawned)
                    {
                        //generate random location for the prize to spawn
                        prizeX = rnd.Next(1, playField[0].Length - 1);
                        prizeY = rnd.Next(1, playField.Length - 1);

                        PrizeSpawned = true;
                    }

                    //make sure the prize isn't spawning on top of the player or a wall
                    while ((playField[prizeY][prizeX] == playerSprite) || (playField[prizeY][prizeX] == wallSprite))
                    {
                        prizeX = rnd.Next(1, playField[0].Length - 1);
                        prizeY = rnd.Next(1, playField.Length - 1);
                    }
                    //spawn the prize in either the newly generated location, or in the old location if it hasn't been picked up yet.
                    playField[prizeY][prizeX] = prizeSprite;

                    //write the playfield, player and prize included, on the screen
                    Console.BackgroundColor = ConsoleColor.Black;
                    foreach (string[] horizontalLine in playField)
                    {
                        foreach (string verticalLine in horizontalLine)
                        {
                            if (verticalLine == playerSprite)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(verticalLine);
                            }
                            else if (verticalLine == wallSprite)
                            {
                                switch (phase)
                                {
                                    case 1:
                                        Console.BackgroundColor = ConsoleColor.DarkGray;
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        break;
                                    case 2:
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        break;
                                    case 3:
                                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        break;
                                }
                                Console.Write(verticalLine);
                                Console.BackgroundColor = ConsoleColor.Black;

                            }
                            else if (verticalLine == prizeSprite)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(verticalLine);
                            }
                            else
                            {
                                Console.Write(verticalLine);
                            }
                        }
                        Console.WriteLine("");
                    }
                }

                //game loop ended, time is measured and checked if it breaks the high score, dialogue changes accordingly
                Console.ForegroundColor = ConsoleColor.White;
                gameStarted = false;
                endTime = System.Environment.TickCount;
                finalScore = (endTime - startTime) / 1000;
                if (highScore < 0)
                {
                    highScore = finalScore;
                    Console.WriteLine("Congratulations, you did it!");
                    Console.WriteLine("It took you " + finalScore + " seconds.");
                    Console.WriteLine("Do you want to try to beat your score? y/n");
                }
                else if (finalScore > highScore)
                {
                    Console.WriteLine("You did it again!");
                    Console.WriteLine("It took you " + finalScore + " seconds.");
                    Console.WriteLine("This is " + (finalScore - highScore) + " seconds slower than your high score, that's a shame!");
                    Console.WriteLine("Do you want to try to beat your score again? y/n");
                }
                else
                {
                    Console.WriteLine("You did it again!");
                    Console.WriteLine("It took you " + finalScore + " seconds.");
                    Console.WriteLine("This is " + (highScore - finalScore) + " seconds faster than your high score, nice!");
                    Console.WriteLine("Do you want to try to beat your score again? y/n");
                    highScore = finalScore;
                }

                while (pressedKey != "y" && pressedKey != "n")
                {
                    pressedKey = Console.ReadLine();
                    switch (pressedKey)
                    {
                        case "n":
                            wantToPlay = false;
                            Console.WriteLine("Okay, goodbye then!");
                            break;
                        case "y":
                            Console.WriteLine("Alright! Press enter to restart.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}