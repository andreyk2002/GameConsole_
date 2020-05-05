using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tst
{
   
    class Program
    {
        /*
        private static void SetHero(Character hero)
        {
            string heroName;
            string heroRace;
            string heroGender;
            Console.Write("Введите имя вашего персонажа >>");
            heroName = Console.ReadLine();
            while (hero == null)
            {


                Console.WriteLine("Введите расу вашего персонажа >>");
                heroRace = Console.ReadLine();
                Console.WriteLine("Введите пол вашего персонажа >>");
                heroGender = Console.ReadLine();
                Console.WriteLine("Является ли ваш персонаж магом (y/n)");
                char mage = Console.ReadKey().KeyChar;
                try
                {
                    if (mage == 'n')
                    {
                        hero = new Character(heroName, heroRace, heroGender);
                    }
                    if (mage == 'y')
                    {
                        hero = new Mage(heroName, heroRace, heroGender);
                    }

                }
                catch (ArgumentException aExeption)
                {
                    Console.WriteLine("Уууупс!");
                    Console.WriteLine(aExeption.Message);
                }
            }
        }

        private static void ShowOptionsList()
        {
            Console.WriteLine("1 - изменить возраст персонажа");
            Console.WriteLine("2 - идти сражатся!");
            Console.WriteLine("3 - отправится за артефактами");
            Console.WriteLine("4 - работать с ")
        }*/
        static void Main(string[] args)
        {
            Character hero = null;
            Character badGuy = null;
            Mage mage = null;
            try
            {
                hero = new Character("Player","Human","Female");

            }
            catch (ArgumentException aExeption)
            {
               
                Console.WriteLine(aExeption.Message);
                return;
            }
            try
            {
                mage = new Mage("Gendalf", Race.Human, Gender.Male);

            }
            catch (ArgumentException aExeption)
            {

                Console.WriteLine(aExeption.Message);
                return;
            }
            try
            {
                badGuy = new Character("", Race.Goblin, Gender.Male);

            }
            catch (ArgumentException aExeption)
            {

                Console.WriteLine(aExeption.Message);
                return;
            }

            badGuy.Age = 50;
            mage.Age = 100;

            mage.MaxHP = 500;
            mage.CurrentHP = 500;

            Console.WriteLine("Текущее значению маны персонажа = " + mage.CurrentMana);

            mage.LearnSpell(new )

        }
    }
}
