using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tst
{
    public enum Condition
    {
        OK, Weaken, Ill, Poisoned, Rooted, Dead
    }
    public enum Race
    {
        Human, Elf, Ork, Goblin
    }
    public enum Gender
    {
        Male, Female
    }

    public class Character : IComparable
    {
        public Character(string name, Race race, Gender gender)
        {
            ID = ++NewID;
            Name = name;
            Race = race;
            Gender = gender;
        }
        public Character(string name, string raceName, string genderName)
        {
            ID = ++NewID;
            Name = name;
            SetRace(raceName);
            SetGender(genderName);
        }

        protected static int NewID { get; set; } = 0;
        public int ID { get; private set; }
        public string Name { get; private set; }

        protected Condition condition = Condition.OK;
        public Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                //Во время брони персонаж неуязвим, т.е. не может измениться его статус
                if (Armored)
                    return;
                if (condition == value)
                    return;
                condition = value;
                UpdateCondition();
            }
        }
        public bool AbleToSpeak { get; set; } = true;
        public bool AbleToMove { get; set; } = true;
        public Race Race { get; private set; }

        public Gender Gender { get; private set; }
        public int Age { get; set; } = 18;

        private void SetRace(string raceName)
        {
            try
            {
                this.Race = (Race)Enum.Parse(typeof(Race),raceName, true);
            }

            catch(Exception ex)
            {
                Console.WriteLine("Не удалось создать персонажа с рассой " + raceName);
            }
            
        }
        private void SetGender(string genderName)
        {
            try
            {
                this.Gender = (Gender)Enum.Parse(typeof(Gender), genderName, true);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Не удалось создать персонажа с рассой " + genderName);
            }
        }
        
       

        protected int currentHP = 100;
        public int CurrentHP
        {
            get
            {
                return currentHP;
            }
            set
            {
                //Во время брони персонаж не может терять здоровье
                if (Armored && value < currentHP)
                    return;

                if (value < 0)
                    currentHP = 0;
                else
                    currentHP = value;

                if (currentHP > MaxHP)
                    currentHP = MaxHP;

                UpdateCondition();
            }
        }
        public int MaxHP { get; set; } = 100;
        public int Exp { get; set; } = 0;
        public bool Armored { get; set; } = false;

        public Inventory Inventory { get; set; } = new Inventory();

        public int CompareTo(object obj)
        {
            if (!(obj is Character))
                throw new ArgumentException("Object is not Character");
            return Exp.CompareTo((obj as Character).Exp);
        }

        void UpdateCondition()
        {
            if (Condition == Condition.Weaken && (float)currentHP / MaxHP >= 0.1)
                Condition = Condition.OK;
            else if (Condition == Condition.OK && (float)currentHP / MaxHP < 0.1 && (float)currentHP / MaxHP >= 0)
                Condition = Condition.Weaken;
            else if (currentHP == 0)
                Condition = Condition.Dead;

            AbleToMove = Condition != Condition.Rooted;
        }
        public override string ToString()
        {
            return string.Format(
                "Информация о персонаже: \n" +
                "Имя: {0} \n" +
                "ID: {1} \n" +
                "Состояние: {2} \n" +
                "Cпособен двигаться: {3} \n" +
                "Способен говорить: {4} \n" +
                "Раса: {5} \n" +
                "Пол: {6} \n" +
                "Возраст: {7} \n" +
                "Здоровье: {8}/{9} \n" +
                "Опыт: {10} \n" +
                "Броня: {11} \n",
                Name,
                ID.ToString(),
                Condition.ToString(),
                AbleToSpeak.ToString(),
                AbleToMove.ToString(),
                Race.ToString(),
                Gender.ToString(),
                Age.ToString(),
                CurrentHP.ToString(),
                MaxHP.ToString(),
                Exp.ToString(),
                Armored.ToString()
                );
            ;
        }


        public void UseArtefact(string artefactName, Character target = null, uint power = 0)
        {
            Artefact artefact = Inventory[artefactName];
            if (artefact == null)
            {
                Console.WriteLine("Артефакт не найден");
                return;
            }
            UseArtefact_Validated(artefact, target, power);
        }
        public void UseArtefact(int artefactIndex, Character target = null, uint power = 0)
        {
            Artefact artefact = Inventory[artefactIndex];
            if (artefact == null)
            {
                Console.WriteLine("Артефакт не найден");
                return;
            }
            UseArtefact_Validated(artefact, target, power);
        }
        public void UseArtefact(Artefact artefactInstance, Character target = null, uint power = 0)
        {
            Artefact artefact = Inventory[artefactInstance];
            if (artefact == null)
            {
                Console.WriteLine("Артефакт не найден");
                return;
            }
            UseArtefact_Validated(artefact, target, power);
        }

        private void UseArtefact_Validated(Artefact artefact, Character target = null, uint power = 0)
        {


            if (target == null)
                target = this;

            if (!artefact.Applyable(target))
            {
                Console.WriteLine("Артефакт неприменим к данной цели \n");
                return;
            }

            artefact.Apply(target, power);

            if (!artefact.ReUsable)
            {
                Inventory.RemoveItem(artefact);
            }
        }
        // public void UseArtifact(Artefact artifact, Character target = null, uint power = 0)
    }
}
