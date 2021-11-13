using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //2.Declare two variables that can store whole numbers
            int a, b;

            //3.Assign a number to the first variable
            a = 5;

            //4.Next, assign the value of the first variable to the second variable (you must use the first variable for that)
            b = a;

            //5.Increase the value of the first variable with the value of the second variable
            a += b;

            //6.Print the values of both variables on the screen
            Console.WriteLine("a = " + a + "\nb = " + b);
        }
    }
}
