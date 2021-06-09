using System;

namespace Johny_Scripten_Eindopdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("What's your name?");
            string input = "";

            while (input == "")
            {
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine("Not valid input. Try again.");
                }
            }

            Hero myHero = new Hero();
            myHero.name = input;
            myHero.hitPoints = 100;
            myHero.attackBonus = 3;
            myHero.defending = false;
            Monster myMonster = new Monster();
            myMonster.type = "Blue Slime";
            myMonster.hitPoints = 50;
            myMonster.fleeChance = 10;

            bool gamePlaying = true;
            int turn = 0;

            while(gamePlaying)
            {
                Console.Clear();
                //Console.WriteLine("Hitpoints " + myHero.name + ": "+ myHero.hitPoints);
                //Console.WriteLine("Hitpoints " + myMonster.type + ": "+ myMonster.hitPoints);
                Console.WriteLine("");
                MonsterTurn(myHero, myMonster, turn);
                Console.WriteLine("");
                gamePlaying = CheckState(myHero, myMonster, turn);
                if (gamePlaying)
                {
                    HeroTurn(myHero, myMonster);
                    gamePlaying = CheckState(myHero, myMonster, turn);
                    myHero.turn++;
                }
            }

        }

        static bool CheckState(Hero myHero, Monster myMonster, int turn)
        {
            //Console.WriteLine(turn);

            if (myHero.hitPoints <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You got killed by the " + myMonster.type + "!");
                Console.WriteLine("YOU LOSE!");
                return false;
            }
            if (myMonster.hitPoints <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You killed the " + myMonster.type + "!");
                Console.WriteLine("YOU WIN!");
                return false;
            }
            else if (myHero.turn < 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("I guess you win.");
                return false;
            }
            else
            {
                return true;
            }
        }

        static void MonsterTurn(Hero myHero, Monster myMonster, int turn)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (myHero.turn == 0)
            {
                Console.WriteLine("A wild " + myMonster.type + " approaches!");
            }
            else if (myHero.turn == 40)
            {
                Console.WriteLine(myMonster.type + " got bored and hopped away.");
                myHero.turn = -10;
            }
            else
            {
                int damage = 0;
                Random random = new Random(System.Environment.TickCount);
                bool flee = random.Next(1, 101) < myMonster.fleeChance;
                int randomTurn = random.Next(1, 11);
                if (myMonster.hitPoints < 20 && flee)
                {
                    Console.WriteLine("The " + myMonster.type + " hopped away from the battle!");
                    myHero.turn = -10;
                }
                else if (randomTurn <= 1)
                {
                    Console.WriteLine("The " + myMonster.type + ".....does nothing!");
                }
                else if (randomTurn <= 6)
                {
                    damage = random.Next(1, 7) + random.Next(1, 7);
                    Console.WriteLine("The " + myMonster.type + " hops at you! You took " + damage + " damage!");
                }
                else if (randomTurn <= 9)
                {
                    damage = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
                    Console.WriteLine("The " + myMonster.type + " spits acid! You took " + damage + " damage!");
                }
                else
                {
                    damage = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
                    Console.WriteLine("The " + myMonster.type + " jumps on top of you! You took " + damage + " damage!");
                }

                if (myHero.defending && damage > 0)
                {
                    damage /= 2;
                    Console.WriteLine("You blocked the hit! Damage reduced to " + damage + "!");
                }
                myHero.hitPoints -= damage;
            }
        }

        static void HeroTurn(Hero myHero, Monster myMonster)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Okay " + myHero.name + ", what do you want to do?");
            Console.WriteLine("1. Attack!");
            Console.WriteLine("2. Defend!");
            Console.WriteLine("");
            string input = "";

            while (input != "1" && input != "2")
            {
                input = Console.ReadLine();
                if (input != "1" && input != "2")
                {
                    Console.WriteLine("Not valid input. Try again.");
                }
            }

            if (input == "1")
            {
                Random random = new Random(System.Environment.TickCount);
                int damage = random.Next(1, 13) + myHero.attackBonus;
                myMonster.hitPoints -= damage;
                Console.WriteLine("You attacked the " + myMonster.type + "! You did " + damage + " damage!");
                myHero.defending = false;

                Console.WriteLine("");

                Console.ForegroundColor = ConsoleColor.Red;
                if (myMonster.hitPoints >= 45)
                {
                    Console.WriteLine(myMonster.type + " looks shiny and slimy!");
                }
                else if (myMonster.hitPoints >= 25)
                {
                    Console.WriteLine(myMonster.type + " gurgles and shakes!");
                }
                else if (myMonster.hitPoints >= 10)
                {
                    Console.WriteLine(myMonster.type + " is getting less bouncy!");
                }
                else if (myMonster.hitPoints >= 1)
                {
                    Console.WriteLine(myMonster.type + " is leaking acid everywhere!");
                }
            }
            else
            {
                Console.WriteLine("You hold up your shield! Less damage for the next turn!");
                myHero.defending = true;
            }



            if (myMonster.hitPoints > 0)
            {
                Console.ReadLine();
            }

        }
    }

    class Hero
    {
        public string name;
        public int hitPoints;
        public int attackBonus;
        public bool defending;
        public int turn = 0;
    }

    class Monster
    {
        public string type;
        public int hitPoints;
        public int fleeChance;
    }
}
