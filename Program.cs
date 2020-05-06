using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tst
{
   
    class Program
    {
        class Program
    {
        // вначале хотел сделать юзер-френдли интерфейс....
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
            //создание персонажей
            //
            //
            //Нормально ли, что при выборе несуществующей расы создаётся персонаж с расой Human
            hero = new Character("Player", "numan", "Female");

            mage = new Mage("Gendalf", Race.Human, Gender.Male);


            badGuy = new Character("Ыыынгыз", Race.Goblin, Gender.Male);


            badGuy.Age = 50;
            mage.Age = 100;

            badGuy.Age = 50;
            mage.Age = 100;

            Console.WriteLine("Текущее значение маны мага = " + mage.CurrentMana);

            //Заклинания

            mage.LearnSpell(new UnRoot());
            mage.ForgotSpell(new UnRoot());
            mage.CastSpell(new UnRoot());


            mage.CurrentHP = 30;

            mage.LearnSpell(new IncreaseHP());
            mage.CastSpell(new IncreaseHP(),30);


            
            Console.WriteLine("Текущее значение маны мага = " + mage.CurrentMana);
            Console.WriteLine("Текущее значение хп мага = " + mage.CurrentHP);

            Console.WriteLine();
            Character experimentalMen = new Character("Его надо возродить", Race.Elf, Gender.Male);
            experimentalMen.CurrentHP = 0;
            Console.WriteLine("Текущее значение хп эльфа = " + experimentalMen.CurrentHP);
            Console.WriteLine("Текущее состояние эльфа = " + experimentalMen.Condition.ToString());

            mage.LearnSpell(new Revive());
            mage.CastSpell(new Revive(),1, experimentalMen);
            //эх не получилось
            Console.WriteLine("Текущее значение хп эльфа = " + experimentalMen.CurrentHP);
            Console.WriteLine("Текущее состояние эльфа = " + experimentalMen.Condition.ToString());

            //aртефакты

            Console.WriteLine();

            hero.UseArtefact(new EyeOfVasilisk(10), badGuy);
            DeadWater findArtefactDW = new DeadWater(BottleType.Малая);
            hero.Inventory.AddItem(findArtefactDW);
            hero.UseArtefact(findArtefactDW,mage);
        

            LightningStaff findArtefactLS = new LightningStaff(125);
            //попытка самоустранится
            hero.Inventory.AddItem(findArtefactLS);
            hero.UseArtefact(findArtefactLS,hero,40);
            //  почти удалось!
            
            Console.WriteLine("Текущее значение хп игрока = " + hero.CurrentHP);
            Console.WriteLine("Текущее состояние игрока = " + hero.Condition.ToString()+"\n");

            hero.UseArtefact(findArtefactLS,badGuy,45);
            Console.WriteLine("Текущее значение хп гоблина-вражины = " + badGuy.CurrentHP);
            Console.WriteLine("Текущее состояние гоблина-вражины = " + badGuy.Condition.ToString() + "\n");

            

            PoisonousSaliva saliva = new PoisonousSaliva(52);
            badGuy.Inventory.AddItem(saliva);
            badGuy.UseArtefact(saliva, hero, 26);
            Console.WriteLine("Текущее значение хп игрока = " + hero.CurrentHP);
            Console.WriteLine("Текущее состояние игрока = " + hero.Condition.ToString() + "\n");

            FrogDecoction healPoison = new FrogDecoction();
            mage.Inventory.AddItem(healPoison);
            mage.UseArtefact(healPoison, hero, 25);
            Console.WriteLine("Текущее значение хп игрока = " + hero.CurrentHP);
            Console.WriteLine("Текущее состояние игрока = " + hero.Condition.ToString() + "\n");

            //попытка использовать не реюзабельный артефакт
            mage.UseArtefact(healPoison, hero, 25);

            HolyWater holyWater = new HolyWater(BottleType.Большая);
            hero.Inventory.AddItem(holyWater);
            hero.UseArtefact(holyWater);
            Console.WriteLine("Текущее значение хп игрока = " + hero.CurrentHP);
            Console.WriteLine("Текущее состояние игрока = " + hero.Condition.ToString() + "\n");
            //передаём посох магу
            hero.Inventory.GiveItem(mage, "Посох молнии");
            // и сразу в бой
            mage.UseArtefact(findArtefactLS,badGuy,20);
            Console.WriteLine("Текущее значение хп гоблина-вражины = " + badGuy.CurrentHP);
            Console.WriteLine("Текущее состояние гоблина-вражины = " + badGuy.Condition.ToString() + "\n");


            //сравнение игроков
            Console.WriteLine(mage.CompareTo(hero));
            Console.WriteLine(hero.CompareTo(badGuy));
            //если добавить опыта, то всё изменится
            mage.Exp = 100;
            Console.WriteLine(mage.CompareTo(hero)+ "\n");
            

            //Работа брони
            mage.LearnSpell(new Armor());
            //cначала подбавим маны
            mage.CurrentMana = 100;
            Console.WriteLine("Текущее значение хп мага = " + mage.CurrentHP);

            mage.CastSpell(new Armor());
            //попробуем задомажить теперь мага
            mage.CurrentHP = 15;

            Console.WriteLine("Текущее состояние мага = " + mage.Condition.ToString());
            Console.WriteLine("Текущее значение хп мага = " + mage.CurrentHP);
         }
    }
}
