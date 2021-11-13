using System;

namespace Assignment5
{
    //2.Create a class that can be used for player objects:
    class Player
    {
        //a.Declare a field for the name of the player
        string _name;
        public string Name { get { return _name; } }

        //b.Declare a numeric field for the health of the player
        int _health;
        public int Health { get { return _health; } }

        //c.Declare a constructor with two parameters: one for a name and one for the health
        public Player(string name, int health)
        {
            //d.Inside the constructor, set the values of the fields to the values of the parameters
            _name = name;
            _health = health;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //3.Inside the Main method:
            //a.Create a player object and pass a name and health to the constructor
            Player player = new Player("Dennis", 100);

            //b.Print the name and health of the player on the screen
            Console.WriteLine("Player name: " + player.Name + "\nPlayer health: " + player.Health);
        }
    }
}
