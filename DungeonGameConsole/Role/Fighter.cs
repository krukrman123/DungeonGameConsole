using DungeonGameConsole.Mechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Role
{
    public class Fighter
    {
        public string name;
        public int health;
        public int maxHealth;
        public int attack;
        public int defense;
        public Cube cube;
        public string message;
        private int experience = 0;






        public Fighter(string name, int health, int attack, int defense, Cube cube)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
            this.attack = attack;
            this.defense = defense;
            this.cube = cube;
        }





        public bool IsAlive()
        {
            return (health > 0);
        }


        public bool IsDead()
        {
            return (health == 0);
        }









        public void RestartHealth()
        {
            health = maxHealth;
        }


        protected string GraphicIndicator(int current, int maximal)
        {
            string s = "";
            int total = 20;
            double count = Math.Round(((double)current / maximal) * total);
            if ((count == 0) && (IsAlive()))
                count = 1;
            for (int i = 0; i < count; i++)
                s += "█";
            s = s.PadRight(total);

            s += $" ({current}/{maximal})";

            return s;
        }

        public string GraphicHealth()
        {
            return GraphicIndicator(health, maxHealth);
        }

        public virtual void Combat(Fighter enemy)
        {
            int strike = attack + cube.ThrowIt();
            SetMessage(String.Format("{0} útočí s úderem za {1} hp", name, strike));
            enemy.Defensive(strike);
        }







        public void Defensive(int strike)
        {
            int hurt = strike - (defense + cube.ThrowIt());
            if (hurt > 0)
            {
                health -= hurt;
                message = String.Format("{0} utrpěl poškození {1} hp", name, hurt);
                if (health <= 0)
                {
                    health = 0;
                    message += " a zemřel";
                }

            }
            else
            {
                message = String.Format("{0} odrazil útok", name);
            }

            if (health < 0)
            {
                health = 0;
            }

            SetMessage(message);
        }


        protected void SetMessage(string message)
        {
            this.message = message;
        }

        public string ReturnLastMessage()
        {
            return message;
        }


    }
}
