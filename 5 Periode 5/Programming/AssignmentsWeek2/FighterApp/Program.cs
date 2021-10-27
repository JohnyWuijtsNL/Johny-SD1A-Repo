using System;

namespace FighterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Fighter fighter = new Fighter(50);

            Console.WriteLine("Intimidation = " + fighter.Intimidation);
        }
    }

    class Fighter
    {
        int _intimidation;
        public int Intimidation { get { return _intimidation; } }

        public Fighter(int intimidation)
        {
            _intimidation = intimidation;
        }
    }
}
