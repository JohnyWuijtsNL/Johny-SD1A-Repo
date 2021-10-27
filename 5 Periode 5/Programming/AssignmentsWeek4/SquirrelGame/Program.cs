using System;

namespace Squirrel_Game
{
    class Program
    {

        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            Acorn acorn = new Acorn();
            Squirrel squirrel = new Squirrel();
            Grid grid = new Grid();
            Random rnd;

            gm.Introduction();

            while (gm.WantToPlay)
            {
                gm.Reset();
                squirrel.Reset();
                acorn.Reset();
                grid.Reset();

                while (gm.Score < gm.GoalScore)
                {
                    rnd = new Random(System.Environment.TickCount);

                    gm.GetInput();
                    gm.StartGame();

                    Console.SetCursorPosition(0, 0);

                    grid.Generate(gm, squirrel);
                    squirrel.Move(gm.PressedKey, grid);

                    if (squirrel.Y == acorn.Y && squirrel.X == acorn.X)
                    {
                        gm.Score += 1;
                        acorn.OnScreen = false;

                        grid.Random = rnd.Next(0, 4);

                        if (gm.Score > 19)
                        {
                            grid.Phase = 5;
                        }
                        else if (gm.Score > 14)
                        {
                            grid.Phase = 4;
                        }
                        else if (gm.Score > 9)
                        {
                            grid.Height = 19;
                            grid.Phase = 3;
                        }
                        else if (gm.Score > 4)
                        {
                            grid.Width = 19;
                            grid.Phase = 2;
                        }

                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Your score is: " + gm.Score);

                    grid.grid[squirrel.OldY][squirrel.OldX] = grid.TileSprite;

                    grid.grid[squirrel.Y][squirrel.X] = squirrel.Sprite;

                    acorn.Generate(rnd, grid, squirrel.Sprite);

                    grid.Draw(squirrel.Sprite, acorn.Sprite);

                }

                gm.EndGame();
            }
        }
    }

    class GameManager
    {
        public int Score;
        public int GoalScore = 30;
        public bool GameStarted = false;
        public float FinalScore;
        public float HighScore = -1;
        public float StartTime = 0;
        public float EndTime;
        public bool WantToPlay = true;
        public string PressedKey = "";
        


        public void Introduction()
        {
            Console.WriteLine("Hey, Welcome to the game!");
            Console.WriteLine("The goal is to collect acorns (AC).");
            Console.WriteLine("You can move the squirrel (SQ) by pressing wasd keys, so w is up, a is left, etc.");
            Console.WriteLine("The acorns will randomly appear on the board, try to get " + GoalScore + " of them as fast as possible!");
            Console.WriteLine("Good luck! Press enter to continue.");
        }

        public void Reset()
        {
            Score = 0;
        }

        public void GetInput()
        {
            ConsoleKeyInfo pKey = Console.ReadKey(true);
            PressedKey = pKey.Key.ToString().ToLower();
        }

        public void StartGame()
        {
            if (!GameStarted)
            {
                StartTime = System.Environment.TickCount;
                GameStarted = true;
                Console.Clear();
            }
        }

        public void EndGame()
        {
            Console.ForegroundColor = ConsoleColor.White;
            GameStarted = false;
            EndTime = System.Environment.TickCount;
            FinalScore = (EndTime - StartTime) / 1000;
            if (HighScore < 0)
            {
                HighScore = FinalScore;
                Console.WriteLine("Congratulations, you did it!");
                Console.WriteLine("It took you " + FinalScore + " seconds.");
                Console.WriteLine("Do you want to try to beat your score? y/n");
            }
            else if (FinalScore > HighScore)
            {
                Console.WriteLine("You did it again!");
                Console.WriteLine("It took you " + FinalScore + " seconds.");
                Console.WriteLine("This is " + (FinalScore - HighScore) + " seconds slower than your high score, that's a shame!");
                Console.WriteLine("Do you want to try to beat your score again? y/n");
            }
            else
            {
                Console.WriteLine("You did it again!");
                Console.WriteLine("It took you " + FinalScore + " seconds.");
                Console.WriteLine("This is " + (HighScore - FinalScore) + " seconds faster than your high score, nice!");
                Console.WriteLine("Do you want to try to beat your score again? y/n");
                HighScore = FinalScore;
            }

            while (PressedKey != "y" && PressedKey != "n")
            {
                PressedKey = Console.ReadLine();
                switch (PressedKey)
                {
                    case "n":
                        WantToPlay = false;
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

    class Acorn
    {
        string _sprite = "AC";
        public string Sprite { get { return _sprite; } }
        public int X;
        public int Y;
        public bool OnScreen;

        public void Reset()
        {
            OnScreen = false;
        }

        public void Generate(Random rnd, Grid grid, string SquirrelSprite)
        {
            if (!OnScreen)
            {
                X = rnd.Next(1, grid.grid[0].Length - 1);
                Y = rnd.Next(1, grid.grid.Length - 1);

                OnScreen = true;
            }

            while ((grid.grid[Y][X] == SquirrelSprite) || (grid.grid[Y][X] == grid.WallSprite))
            {
                X = rnd.Next(1, grid.grid[0].Length - 1);
                Y = rnd.Next(1, grid.grid.Length - 1);
            }

            grid.grid[Y][X] = Sprite;
        }
    }
    class Squirrel
    {
        public string Sprite = "SQ";
        public int X = 3;
        public int Y = 4;
        public int OldX;
        public int OldY;

        public void Reset()
        {
            X = 3;
            Y = 4;
        }

        public void Move(string pressedKey, Grid grid)
        {
            OldX = X;
            OldY = Y;
            switch (pressedKey)
            {
                        case "w":
                            Y -= 1;
                break;
                        case "a":
                            X -= 1;
                break;
                        case "s":
                            Y += 1;
                break;
                        case "d":
                            X += 1;
                break;
                default:
                            break;

            }

            if (grid.grid[Y][X] == grid.WallSprite)
            {
                X = OldX;
                Y = OldY;
            }
        }
    }

    class Grid
    {
        string _tileSprite = "  ";
        public string TileSprite { get { return _tileSprite; } }
        string _wallSprite = "[]";
        public string WallSprite { get { return _wallSprite; } }

        public int Height = 10;
        public int Width = 10;
        public int Random = 0;
        public int Phase = 1;
        public string[][] grid;

        public void Reset()
        {
            Height = 10;
            Width = 10;
            Phase = 1;
        }
        public void Generate(GameManager gm, Squirrel squirrel)
        {
            grid = new string[Height][];
            int y = 0;
            while (y < grid.Length)
            {
                grid[y] = new string[Width];
                int x = 0;
                if (y == 0 || y == grid.Length - 1)
                {
                    while (x < grid[y].Length)
                    {
                        grid[y][x] = WallSprite;
                        x++;
                    }
                }
                if (y == 9)
                {
                    if (Phase == 4 || Phase == 5)
                    {
                        while (x < grid[y].Length)
                        {
                            if ((x != 4 && x != 5 && x != 13 && x != 14) && y != 0 && y != grid.Length - 1)
                            {
                                grid[y][x] = WallSprite;
                            }
                            else
                            {
                                grid[y][x] = TileSprite;
                            }
                            x++;
                        }
                    }
                    else if (Phase == 3)
                    {
                        while (x < grid[y].Length)
                        {
                            if ((x != 13 && x != 14) && y != 0 && y != grid.Length - 1)
                            {
                                grid[y][x] = WallSprite;
                            }
                            else
                            {
                                grid[y][x] = TileSprite;
                            }
                            x++;
                        }
                    }
                }
                else if ((y == 3 || y == 6) && (Phase == 4 || Phase == 5))
                {
                    while (x < grid[y].Length)
                    {
                        if ((x >= 3 && x <= 9) || x == 0 || x == grid[y].Length - 1 || x == 12 || x == 15)
                        {
                            grid[y][x] = WallSprite;
                        }
                        else
                        {
                            grid[y][x] = TileSprite;
                        }
                        x++;
                    }
                }
                else if ((y == 12 || y == 15) && (Phase == 4 || Phase == 5))
                {
                    while (x < grid[y].Length)
                    {
                        if ((x >= 9 && x <= 15) || x == 0 || x == grid[y].Length - 1 || x == 3 || x == 6)
                        {
                            grid[y][x] = WallSprite;
                        }
                        else
                        {
                            grid[y][x] = TileSprite;
                        }
                        x++;
                    }
                }

                while (x < grid[y].Length && y != 0 && y != grid.Length - 1)
                {
                    if (grid[y][x] == squirrel.Sprite)
                    {
                        grid[y][x] = squirrel.Sprite;
                    }

                    if ((x == 9 && y != 5 && y != 4) && Phase == 2)
                    {
                        grid[y][x] = WallSprite;
                    }
                    else if ((x == 9 && y != 5 && y != 4 && y != 13 && y != 14) && Phase == 3)
                    {
                        grid[y][x] = WallSprite;
                    }
                    else if (((x == 9 && y != 5 && y != 4 && y != 13 && y != 14) || ((x == 3 || x == 6) && (y >= 9 && y <= 15)) || ((x == 12 || x == 15) && (y >= 3 && y <= 9))) && (Phase == 4 || Phase == 5))
                    {
                        grid[y][x] = WallSprite;
                    }
                    else
                    {
                        grid[y][x] = TileSprite;
                    }

                    if (x == 0 || x == grid[y].Length - 1)
                    {
                        grid[y][x] = WallSprite;
                    }
                    x++;
                }
                y++;
            }
            if (Phase == 5)
            {
                y = 0;
                while (y < grid.Length)
                {
                    int x = 0;
                    if (y % 2 == 0)
                    {
                        while (x < grid[y].Length)
                        {
                            if ((x + 2 + Random) % 4 == 0)
                            {
                                if (gm.Score % 2 == 0)
                                {
                                    grid[y][x] = WallSprite;
                                }
                                else
                                {
                                    grid[x][y] = WallSprite;
                                }
                            }
                            x++;
                        }
                    }
                    else if (y % 2 == 1)
                    {
                        while (x < grid[y].Length)
                        {
                            if ((x + Random) % 4 == 0)
                            {
                                if (gm.Score % 2 == 0)
                                {
                                    grid[y][x] = WallSprite;
                                }
                                else
                                {
                                    grid[x][y] = WallSprite;
                                }
                            }
                            x++;
                        }
                    }
                    y++;
                }
            }
        }

        public void Draw(string SquirrelSprite, string AcornSprite)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (string[] horizontalLine in grid)
            {
                foreach (string verticalLine in horizontalLine)
                {
                    if (verticalLine == SquirrelSprite)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(verticalLine);
                    }
                    else if (verticalLine == WallSprite)
                    {
                        switch (Phase)
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
                            case 4:
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case 5:
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                        }
                        Console.Write(verticalLine);
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                    else if (verticalLine == AcornSprite)
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
    }
}