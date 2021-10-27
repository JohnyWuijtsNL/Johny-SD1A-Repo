using System;

namespace DruidApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Druid druid1 = new Druid();
            Druid druid2 = new Druid(druid1.Wisdom / 2);
            Console.WriteLine("Wisdom druid 1: " + druid1.Wisdom);
            Console.WriteLine("Wisdom druid 2: " + druid2.Wisdom);
        }
    }

    class Druid
    {
        float _wisdom;
        public float Wisdom { get { return _wisdom; } }

        public Druid()
        {
            _wisdom = 1;
        }

        public Druid(float wisdom)
        {
            _wisdom = wisdom;
        }
    }
}
