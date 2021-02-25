using System;

namespace Johny_Scripten_4_Eindproduct
{
    class Program
    {

        static void Main(string[] args)
        {
            //variable for playfield, change number to change the height
            string[][] playField = new string[16][];

            //variable for width of playfield, change number to change the width


            int playFieldWidth = 32;

            //variable for tracking player location
            int playerX = 0;
            int playerY = 1;

            int oldPlayerX;
            int oldPlayerY;

            //variable for tracking if an apple is currently in the game
            bool PrizeSpawned = false;

            //variable for tracking location of the prize
            int prizeX = 0;
            int prizeY = 0;

            //variable for tracking the score, change number to change starting score
            int score = 0;

            //variable to set how objects in the game look, you can change the tile sprite here, the other sprites are set in-game
            string prizeSprite = "";
            string playerSprite = "";
            string tileSprite = "  ";
            string wallSprite = "[]";

            //variable for tracking how many attempts the player made on typing the correct amount of characters
            int attempt1 = 0;
            int attempt2 = 0;

            //input
            string pressedKey = "d";
            string oldInput = "d";
            bool inputWorked = true;

            //random seed set
            Random rnd = new Random(5873495);

            //ask player to set their looks, response changes based on how many attempts the player has made to enter 2 characters
            while (playerSprite.Length != 2)
            {
                switch (attempt1)
                {
                    case 0:
                        Console.WriteLine("Hey, Welcome to the game!");
                        Console.WriteLine("Please customize your character.");
                        playerSprite = Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Sorry, I should've clarified. You need to enter 2 characters, your character will be those 2 characters.");
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
                        if(attempt1 <= 2)
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
            if(attempt1 <= 3 && attempt2 <= 2)
            {
                Console.WriteLine("Okay, with that out of the way, let's start the game!");
                Console.WriteLine("The goal is to collect the prizes you set, so in your case, " + prizeSprite + ".");
                Console.WriteLine("You can move by pressing enter, and change direction by pressing wasd keys, so w is up, a is left, etc.");
                Console.WriteLine("The prizes will randomly appear on the board, try to get a high score!");
                Console.WriteLine("Good luck! Press enter to continue.");
            }
            else
            {
                Console.WriteLine("....Okay, with that out of the way, let's start the game.");
                Console.WriteLine("The goal is to collect the prizes you were supposed to set, so in your case, " + prizeSprite + ".");
                Console.WriteLine("You can move by pressing enter, and change direction by pressing wasd keys, so w is up, a is left, etc.");
                Console.WriteLine("The prizes will randomly appear on the board. Press enter to continue.");
            }

            //game loop
            while (true)
            {
                //stores the pressed key in a variable, and at the start of the game, also gives the player time to read instructions
                if (inputWorked)
                {
                    oldInput = pressedKey;
                }

                inputWorked = true;
                pressedKey = Console.ReadLine();

                //clears the console, so new lines can be written like it's a new frame of the same game
                Console.Clear();

                //generate playing field, including border
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

                    while (x < playField[y].Length && y != 0 && y != playField.Length - 1)
                    {
                        if(x == 0 || x == playField[y].Length - 1)
                        {
                            playField[y][x] = wallSprite;
                        }
                        else if (playField[y][x] == playerSprite)
                        {
                            playField[y][x] = playerSprite;
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
                        inputWorked = false;
                        switch (oldInput)
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
                        break;

                }

                //if the player touches a wall, move them back to their old location
                if (playField[playerY][playerX] == wallSprite)
                {
                    playerX = oldPlayerX;
                    playerY = oldPlayerY;
                }

                //if the player is touching a prize, increase the score, request a new prize and spawn a wall on the player's old location
                if (playerY == prizeY && playerX == prizeX)
                {
                    playField[oldPlayerY][oldPlayerX] = playerSprite;
                    score += 1;
                    PrizeSpawned = false;
                }
                
                //write score on the screen
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

                    //make sure the prize isn't spawning on top of the player or a wall
                    while ((playField[prizeY][prizeX] == playerSprite) || (playField[prizeY][prizeX] == wallSprite))
                    {
                        prizeX = rnd.Next(1, playField[0].Length - 1);
                        prizeY = rnd.Next(1, playField.Length - 1);
                    }

                    PrizeSpawned = true;
                }

                //spawn the prize in either the newly generated location, or in the old location if it hasn't been picked up yet.
                playField[prizeY][prizeX] = prizeSprite;

                //write the playfield, player and prize included, on the screen
                foreach (string[] horizontalLine in playField)
                {
                    foreach (string verticalLine in horizontalLine)
                    {
                        Console.Write(verticalLine);
                    }
                    Console.WriteLine("");
                }
            }
        }
    }
}
