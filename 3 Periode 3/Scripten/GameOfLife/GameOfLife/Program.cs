using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            int fieldWidth = 18;
            int fieldHeight = 18;
            string tile = "[]";
            string border = "][";
            string blank = "  ";
            Random rnd = new Random(448945);

            Console.ReadLine();
            string[][] field = new string[fieldHeight][];

            int y = 0;
            while (y == 0 || y == field.Length - 1)
            {
                field[y] = new string[fieldWidth];
                int x = 0;
                while (x < field[y].Length)
                {
                    field[y][x] = border;
                    x++;
                }
                y++;
            }
            while (y < field.Length)
            {
                int x = 0;
                field[y] = new string[fieldWidth];
                if (y == 0 || y == field.Length - 1)
                {
                    while (x < field[y].Length)
                    {
                        field[y][x] = border;
                        x++;
                    }
                }
                else
                {
                    while (x < field[y].Length)
                    {
                        if (x == 0 || x == field[y].Length - 1)
                        {
                            field[y][x] = border;
                        }
                        else
                        {
                            switch (rnd.Next(0, 4))
                            {
                                case 0:
                                    field[y][x] = tile ;
                                    break;
                                default:
                                    field[y][x] = blank;
                                    break;
                            }
                        }
                        x++;
                    }
                }

                y++;
            }

            foreach (var verticalLine in field)
            {
                foreach(var horizontalLine in verticalLine)
                {
                    if (horizontalLine == tile)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.Write(horizontalLine);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = Console.BackgroundColor;
                    }
                    else if (horizontalLine == border)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.Write(horizontalLine);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = Console.BackgroundColor;
                    }
                    else
                    {
                        Console.Write(horizontalLine);
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
