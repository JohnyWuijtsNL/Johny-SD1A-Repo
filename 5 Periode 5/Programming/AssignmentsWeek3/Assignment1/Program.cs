using System;
using System.Collections.Generic;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<Weapon> weapons = new List<Weapon>();
            List<Player> players = new List<Player>();
            int i = rnd.Next(5, 10);
            while (i > 0)
            {
                int rndString = rnd.Next(0, 5);
                string weaponType;
                switch(rndString)
                {
                    case 0:
                        weaponType = "Sword";
                        break;
                    case 1:
                        weaponType = "Bow";
                        break;
                    case 2:
                        weaponType = "Staff";
                        break;
                    case 3:
                        weaponType = "Spear";
                        break;
                    case 4:
                        weaponType = "Whip";
                        break;
                    default:
                        weaponType = "Error";
                        break;
                }
                weapons.Add(new Weapon(weaponType, (float)rnd.Next(1, 20) / 100));
                i--;
            }
            foreach (Weapon weapon in weapons)
            {
                Console.WriteLine(weapon.Name + " does " + weapon.Damage + " damage.");
            }

            players.Add(new Player("Harold"));
            players.Add(new Player("Mike"));
            bool isHarold = true;
            while (weapons.Count > 0)
            {
                int weaponToPick = rnd.Next(0, weapons.Count);
                int playerToPick = 1;
                if (isHarold)
                {
                    playerToPick = 0;
                }
                isHarold = !isHarold;
                
                players[playerToPick].PickUpWeapon(weapons[weaponToPick]);
                Console.WriteLine(players[playerToPick].Name + " picked up the " + weapons[weaponToPick].Name + " that does " + weapons[weaponToPick].Damage + " damage.");
                weapons.RemoveAt(weaponToPick);
            }

            bool playerKilled = false;
            float damage = 0;
            while (!playerKilled)
            {
                int playerToPick = 1;
                if (isHarold)
                {
                    playerToPick = 0;
                }
                isHarold = !isHarold;
                players[playerToPick].Health -= damage;
                if (players[playerToPick].Health <= 0)
                {
                    playerKilled = true;
                    Console.WriteLine(players[playerToPick].Name + " has no health left.");
                    Console.WriteLine(players[playerToPick].Name + " died.");
                    Console.WriteLine("GAME OVER");
                }
                else
                {
                    Console.WriteLine(players[playerToPick].Name + " has " + players[playerToPick].Health + " health left.");
                    Console.WriteLine(players[playerToPick].Name + " used his " + players[playerToPick].ActiveWeapon.Name + " that does " + players[playerToPick].ActiveWeapon.Damage + " damage.");
                    damage = players[playerToPick].ActiveWeapon.Damage;
                }


            }



        }
    }

    class Weapon
    {
        string _name;
        public string Name { get { return _name; } }

        float _damage;
        public float Damage { get { return _damage; } }

        public Weapon(string name, float damage)
        {
            _name = name;
            _damage = damage;
        }
    }

    class Player
    {
        string _name;
        public string Name { get { return _name; } }

        public float Health;

        List<Weapon> Inventory;

        Weapon _activeWeapon;
        public Weapon ActiveWeapon { get { return _activeWeapon;  } }

        public void PickUpWeapon(Weapon weapon)
        {
            Inventory.Add(weapon);
            _activeWeapon = weapon;
        }

        void WeaponStats()
        {
            foreach (Weapon weapon in Inventory)
            {
                Console.WriteLine(weapon.Name + " damage is " + weapon.Damage + ".");
            }
        }

        void Attack(Player opponent)
        {
            opponent.Health -= _activeWeapon.Damage;
        }

        public Player(string name)
        {
            _name = name;
            Health = 1.00f;
            Inventory = new List<Weapon>();
        }
    }
}
