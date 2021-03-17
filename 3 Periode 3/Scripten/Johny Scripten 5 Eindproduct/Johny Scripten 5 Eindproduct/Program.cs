using System;
using System.Collections.Generic;
using System.Threading;

namespace Johny_Scripten_5_Eindproduct
{
    class Program
    {
        static void Main(string[] args)
        {
            //highscore variable
            int highScore = 0;


            //set sprite variables
            string wallSprite = "[]";
            string playerSprite = "00";
            string prizeSprite = "%%";
            string tileSprite = "  ";

            //set width and height of playing field
            int playFieldHeight = 23;
            int playFieldWidth = 45;

        startGame:

            //set variables for player location
            int playerX = 1;
            int playerY = 1;
            List<int[]> snakeList = new List<int[]>();
            int snakeLength = 2;

            //set variable for key pressed
            string pressedKey = " ";
            string validKey = " ";

            //set variable for game speed and state
            float gameSpeedF = 100f;
            int gameSpeed = 100;
            float difficultyCurve = 0.9f;

            //set variable for apple
            bool appleSpawned = false;
            int appleX = 0;
            int appleY = 0;
            Random rnd;
            int score = 0;
            int appleType = 1;
            int frames = 0;

            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    //make new random seed based on how much time has passed
                    rnd = new Random(System.Environment.TickCount);

                    //clears the console, so new lines can be written like it's a new frame of the same game
                    Console.SetCursorPosition(0, 0);

                    //generate border of playing field
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
                        else
                        {
                            while (x < playField[y].Length)
                            {
                                if (x == 0 || x == playField[y].Length - 1)
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
                        y++;
                    }

                    //remove snake pieces that make the snake too long
                    while (snakeList.Count > snakeLength)
                    {
                        snakeList.RemoveAt(0);
                    }

                    //place a piece of snake at every position it was in previously, minus what was removed in the code above
                    foreach (var snakePiece in snakeList)
                    {
                        playField[snakePiece[0]][snakePiece[1]] = playerSprite;
                    }

                    //check which key was pressed, and move the player accordingly (wasd controls)
                    switch (pressedKey)
                    {
                        case "w":
                            if (validKey == "s")
                            {
                                goto default;
                            }
                            playerY -= 1;
                            validKey = pressedKey;
                            break;
                        case "a":
                            if (validKey == "d")
                            {
                                goto default;
                            }
                            playerX -= 1;
                            validKey = pressedKey;
                            break;
                        case "s":
                            if (validKey == "w")
                            {
                                goto default;
                            }
                            playerY += 1;
                            validKey = pressedKey;
                            break;
                        case "d":
                            if (validKey == "a")
                            {
                                goto default;
                            }
                            playerX += 1;
                            validKey = pressedKey;
                            break;
                            //if input was invalid, use previous valid key
                        default:
                            switch (validKey)
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


                    //if the player touches a wall or a piece of themselves, game over
                    if (playField[playerY][playerX] == wallSprite || (playField[playerY][playerX] == playerSprite && validKey != " "))
                    {
                        goto isDead;
                    }

                    //if the player touches an apple, grow, request new apple and increase game speed
                    if (playerY == appleY && playerX == appleX)
                    {
                        if(appleType <=75)
                        {
                            snakeLength += 1;
                            score += 1;
                        }
                        else if (appleType <= 90)
                        {
                            snakeLength += 4;
                            score += 2;
                        }
                        else if (appleType <= 98)
                        {
                            snakeLength += 9;
                            score += 6;
                        }
                        else
                        {
                            snakeLength += 25;
                            score += 20;
                        }

                        appleSpawned = false;
                        gameSpeedF *= difficultyCurve;
                        gameSpeed = (int)(Math.Round(gameSpeedF));
                    }

                    //set new location of the player
                    playField[playerY][playerX] = playerSprite;

                    //store new location of player in list
                    snakeList.Add(new int[] { playerY, playerX });



                    //generate new apple location if apple isn't on the screen
                    if (!appleSpawned)
                    {
                        //generate new apple location until the apple isn't spawning on player or wall
                        while (playField[appleY][appleX] == playerSprite || playField[appleY][appleX] == wallSprite)
                        {
                            appleX = rnd.Next(1, playFieldWidth - 1);
                            appleY = rnd.Next(1, playFieldHeight - 1);
                        }
                        appleSpawned = true;

                        //choose type of apple
                        appleType = rnd.Next(1, 101);


                    }

                    playField[appleY][appleX] = prizeSprite;

                    //write playing field to console
                    foreach (string[] horizontalLine in playField)
                    {
                        foreach (string verticalLine in horizontalLine)
                        {
                            if (verticalLine == wallSprite)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            }
                            else if (verticalLine == playerSprite)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (verticalLine == prizeSprite)
                            {
                                if (appleType <= 75)
                                {
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                else if (appleType <= 90)
                                {
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else if (appleType <= 98)
                                {
                                    if (frames % 8 <= 3)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Red;
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkRed;
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                    }

                                }
                                else
                                {
                                    if(frames % 6 == 0)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Red;
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                    if (frames % 6 == 1)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Yellow;
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                    }
                                    if (frames % 6 == 2)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Green;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                    }
                                    if (frames % 6 == 3)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Cyan;
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                    }
                                    if (frames % 6 == 4)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                    }
                                    if (frames % 6 == 5)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Magenta;
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                    }

                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            Console.Write(verticalLine);
                        }
                        Console.WriteLine("");
                    }
                    frames++;
                    Thread.Sleep(gameSpeed);
                    Console.ResetColor();
                }
                ConsoleKeyInfo pKey = Console.ReadKey(true);
                pressedKey = pKey.Key.ToString().ToLower();
            }
            //when the player dies, give score and give player the option to play again
        isDead:
            Console.SetCursorPosition(0, playFieldHeight + 1);
            Console.WriteLine("Game Over! Your score was " + score + "!");
            if (score > highScore && highScore != 0)
            {
                highScore = score;
                Console.WriteLine("This is a new highscore! Congratulations!");
            }
            else if (score < highScore)
            {
                Console.WriteLine("The highscore is " + highScore + "."); 
            }
            else if (score == highScore && highScore != 0)
            {
                Console.WriteLine("You tied the highscore!");
            }
            else
            {
                highScore = score;
                Console.WriteLine();
            }

            Console.WriteLine("Do you want to try again? (y/n)");
            string answer = " ";
            while (answer != "y" && answer != "n")
            {
                Console.SetCursorPosition(0, playFieldHeight + 4);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, playFieldHeight + 4);
                answer = Console.ReadLine();
            }

            if (answer == "y")
            {
                Console.WriteLine("Alright! Press enter to restart.");
                Console.ReadLine();
                Console.Clear();
                goto startGame;
            }

            Console.WriteLine("Alright, goodbye then!");

        }

    }   
}
