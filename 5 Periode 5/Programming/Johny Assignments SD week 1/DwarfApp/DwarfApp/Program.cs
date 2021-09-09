using System;

namespace DwarfApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dwarf dwalina = new Dwarf();
            dwalina.name = "Dwalina";
            Dwarf fili = new Dwarf();
            fili.name = "Fili";

            dwalina.SayHello();
            fili.SayHello();
        }
    }

    class Dwarf
    {
        public string name = "Dwarf";

        public void SayHello()
        {
            Console.WriteLine("“Hello, my name is " + this.name + "!“");
        }
    }
}
