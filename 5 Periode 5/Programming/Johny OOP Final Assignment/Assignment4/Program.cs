using System;

namespace Assignment4
{
    //2.Create a class that can store information about a rectangle:
    class Rectangle
    {
        //a.Declare fields for the width and height of the rectangle
        public float Width;
        public float Height;

        //b.Declare a field that can store the surface area of the rectangle
        float _area;
        public float Area { get { return _area; } }

        //c.Declare a method that computes the surface area. The result of the computation must be stored in the field from step b.
        public void ComputeArea()
        {
            _area = Width * Height;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //3.Inside the Main method:
            //a.Create a rectangle object and assign it to a variable
            Rectangle rectangle = new Rectangle();

            //b.Set the width and height of the rectangle
            rectangle.Width = 5.6f;
            rectangle.Height = 3.8f;

            //c.Let the rectangle compute its surface area (you must call the method that you have created)
            rectangle.ComputeArea();

            //d.Print the dimensions and surface area on the screen
            Console.WriteLine("Width: " + rectangle.Width + "\nHeight: " + rectangle.Height + "\nArea: " + rectangle.Area);
        }
    }
}
