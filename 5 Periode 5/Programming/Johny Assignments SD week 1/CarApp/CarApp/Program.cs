using System;

namespace CarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();
            car.fuel = 40;
            car.Drive(240);
            Console.WriteLine("Fuel left: " + car.fuel);
            car.AddFuel(12);
            Console.WriteLine("Fuel left: " + car.fuel);
        }
    }

    class Car
    {
        public float fuel = 0;

        public void Drive(float kilometers)
        {
            this.fuel -= kilometers * 0.05f;
        }

        public void AddFuel(float liters)
        {
            this.fuel += liters;
        }
    }
}
