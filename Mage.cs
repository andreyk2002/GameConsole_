using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{

    public class Mage : Character
    {
        private int currentMana = 100;
        public int CurrentMana
        {
            get
            {
                return currentMana;
            }
            set
            {
                if (value < 0)
                    currentMana = 0;
                else
                    currentMana = value;

                if (currentMana > MaxMana)
                {
                    currentMana = MaxMana;
                }
            }
        }
        public int MaxMana { get; set; } = 100;
        public Mage(string name, Race race, Gender gender) : base(name, race, gender)
        {
        }
        public Mage(string name, string raceName, string genderName) : base(name, raceName, genderName)
        {
        }
        public override string ToString()
        {
            return string.Format(
                base.ToString() +
                "Мана {0}/{1} \n",
                CurrentMana.ToString(),
                MaxMana.ToString());
        }

        private List<Spell> spells;

        //избегаем дублирования
        private bool FindSpell(Spell spell)
        {
            foreach (var el in spells)
            {
                if (el.GetType().Equals(spell.GetType()))
                {
                    return true;
                }
            }
            return false;
        }

        public void LearnSpell(Spell spell)
        {
            if(!FindSpell(spell))
            {
                //если не маг не владеет данным видом заклинания
                spells.Add(spell);
            }
            
        }
        public void ForgotSpell(Spell spell)
        {
            if (FindSpell(spell))
            {
                spells.Remove(spell);
            }
        }
        public void CastSpell(Spell spell, uint power = 0, Character target = null)
        {
            if (FindSpell(spell))
            {
                if (target == null)
                {
                    target = this;
                }

                if (!spell.Applyable(target))
                {
                    Console.WriteLine("Заклинание неприменимо к данной цели \n");
                    return;
                }

                if (spell.RequiredMana(power) > CurrentMana)
                {
                    Console.WriteLine("Недостаточно маны\n");
                    return;
                }


                CurrentMana -= (int)spell.RequiredMana(power);
                spell.Apply(target, power);
            }
            else
            {
                Console.WriteLine("Данное заклинание не изучено");
            }
        }
        /*
        public void UseSpell(Spell spell, uint power = 0, Character target = null)
        {
            if (target == null)
                target = this;

            if (!spell.Applyable(target))
            {
                Console.WriteLine("Заклинание неприменимо к данной цели \n");
                return;
            }

            if (spell.RequiredMana(power) > CurrentMana)
            {
                Console.WriteLine("Недостаточно маны\n");
                return;
            }


            CurrentMana -= (int)spell.RequiredMana(power);
            spell.Apply(target, power);
        }
        */

    }
}

