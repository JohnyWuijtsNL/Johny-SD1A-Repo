using System;
using System.Collections.Generic;
using System.Threading;

namespace Johny_Scripten_5_Eindproduct
{
    class Program
    {
        static void Main(string[] args)
        {
            //set sprite variables
            string wallSprite = "[]";
            string playerSprite = "00";
            string prizeSprite = "%%";
            string tileSprite = "  ";

            //set width and height of playing field
            int playFieldHeight = 27;
            int playFieldWidth = 27;

            //set variables for player location
            int playerX = 1;
            int playerY = 1;
            List<int[]> snakeList = new List<int[]>();


            //set variable for key pressed
            string pressedKey = " ";
            string validKey = " ";

            //set variable for game speed and state
            int gameSpeed = 25;

            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine(snakeList.Count);

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


                    //if the player touches a wall, game over
                    if (playField[playerY][playerX] == wallSprite)
                    {
                        goto isDead;
                    }

                    //set new location of the player
                    playField[playerY][playerX] = playerSprite;
                    //store old location of player in list
                    snakeList.Add(new int[] { playerY, playerX });





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
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Green;
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
                    Thread.Sleep(gameSpeed);
                    Console.ResetColor();
                }
                ConsoleKeyInfo pKey = Console.ReadKey(true);
                pressedKey = pKey.Key.ToString().ToLower();
            }
        isDead:
            Console.SetCursorPosition(0, 28);
            Console.WriteLine("Game Over!");
            Console.ReadLine();

               
        }

    }   
}
