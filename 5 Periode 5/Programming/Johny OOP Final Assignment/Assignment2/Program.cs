using System;
using System.Collections.Generic;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            //2.Declare a variable for a list that can contain strings
            List<string> names;

            //3.Create (instantiate) a list object and assign it to your variable. In other words, your variable must refer to the list object.
            names = new List<string>();

            //4.Add your name and the names of some class mates to the list
            names.Add("Johny");
            names.Add("Samantha");
            names.Add("Jiri");

            //5.Sort the names in the list
            names.Sort();

            //6.Show all the names in the list on the screen
            Console.WriteLine(String.Join(", ", names));
        }
    }
}
