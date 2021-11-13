using System;

namespace Assignment3
{
    //2.Create a class for storing information about characters in a game:
    class Character
    {
        //a.Declare a field for the name of the character
        public string Name;

        //b.Declare a field for the characters’ strength. The field must be able to store decimal values.
        public float Strength;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //3.Inside the Main method:
            //a.Declare two variables that can refer to a game character object
            Character player1, alsoPlayer1;

            //b.Create (instantiate) a character object and make the first variable refer to this object
            player1 = new Character();

            //c.Using this first variable, give the character a name
            player1.Name = "Archibald";

            //d.Let the second variable refer to the (same) character object
            alsoPlayer1 = player1;

            //e.Set the strength of the character using the second variable
            alsoPlayer1.Strength = 9000.1f;

            //f.Print the name and strength of the character on the screen
            Console.WriteLine("Player name: " + player1.Name + "\nPlayer strength: " + player1.Strength);
        }
    }
}
