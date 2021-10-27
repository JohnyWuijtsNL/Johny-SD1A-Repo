using System;

namespace BarbarianApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Barbarian barbarian = new Barbarian("Barbara Babbersworth");
            Druid druid = new Druid("Druial Druijts");
            barbarian.ShowName();
            druid.ShowName();
        }
    }

    class Character
    {
        public string Name;

        public void ShowName()
        {
            Console.WriteLine("Name: " + Name);
        }
    }

    class Barbarian : Character
    {
        public Barbarian(string name)
        {
           Name = name;
        }
    }

    class Druid : Character
    {
        public Druid(string name)
        {
            Name = name + " the Druid";
        }
    }
}
