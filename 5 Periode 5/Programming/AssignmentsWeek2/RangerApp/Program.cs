using System;

namespace RangerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Ranger ranger1 = new Ranger("Rangey Rangerson");
            Ranger ranger2 = new Ranger("Raygel Rangelford", 0.5f);
            Console.WriteLine("Stealth of " + ranger1.Name + ": " + ranger1.Stealth);
            Console.WriteLine("Stealth of " + ranger2.Name + ": " + ranger2.Stealth);
        }
    }

    class Ranger
    {
        string _name;
        public String Name { get { return _name; } }

        float _stealth;
        public float Stealth { get { return _stealth; } }

        public Ranger(string name)
        {
            _name = name;
            _stealth = 1;
        }

        public Ranger(string name, float stealth) : this(name)
        {
            _stealth = stealth;
        }
    }
}
