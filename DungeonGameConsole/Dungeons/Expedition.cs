using DungeonGameConsole.Mechanic;
using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Dungeons
{
    public class Expedition : FightLogic
    {
        private Mag player;
        private Cube cube;
        private Stopwatch stopwatch;


        public Expedition(Mag player, Cube cube)
        {
            this.player = player;
            this.cube = cube;
            this.stopwatch = new Stopwatch();

        }

        private int GenerateRandomDamage()
        {
            Random random = new Random();

            return Math.Max(0, random.Next(10, 31));
        }

        private bool CheckProbability(int percentage)
        {
            Random random = new Random();
            return random.Next(1, 101) <= percentage;
        }

        public void Explore()
        {
            Console.WriteLine("Vyrazili jste na dobrodružství!");

            int numberOfExpeditions = 1;
            int baseExpeditionDuration = 10000;
            int expeditionDuration;

            while (true)
            {
                ;

                int remainingSeconds;
                int damageTaken = 0;

                stopwatch.Restart();

                while (stopwatch.ElapsedMilliseconds < baseExpeditionDuration)
                {
                    long remainingTime = baseExpeditionDuration - stopwatch.ElapsedMilliseconds;
                    remainingSeconds = (int)Math.Ceiling(remainingTime / 1000.0);

                    Console.Write($"\rZbývající čas: {remainingSeconds} sekund");

                    if (CheckProbability(90))
                    {
                        Console.Clear();
                        Console.WriteLine("Vyrazili jste na dobrodružství!");
                        Console.Write($"\rZbývající čas: {remainingSeconds} sekund");

                        int damage = GenerateRandomDamage();
                        Console.WriteLine($"\u001b[31m\nNarazili jste na nebezpečné místo! Utrpěli jste {damage} poškození.\u001b[0m");
                        player.ReceiveDamage(damage);
                        damageTaken += damage;
                    }



                    if (player.IsDead())
                    {

                        Console.WriteLine("\u001b[31mUmřeli jste! Hra končí.\u001b[0m");
                        HandlePlayerMoney();
                        player.RestartHealth();
                        return;
                    }

                    Thread.Sleep(1000);
                }

                Console.Clear();
                Console.WriteLine($"Výprava č. {numberOfExpeditions} byla úspěšně dokončena!");
                DisplayExpeditionResults();

                Console.WriteLine($"\u001b[31mCelkové poškození během výpravy: {damageTaken}\u001b[0m");
                Console.WriteLine($"\u001b[32mCelkový čas výpravy: {baseExpeditionDuration / 1000} sekund\u001b[0m");

                Thread.Sleep(2500);


                Console.Clear();
                return;
            }
        }




        private void HandlePlayerMoney()
        {
            if (player.Money < 0)
            {
                Console.WriteLine("Upozornění: Nemáte dostatek peněz. Odečítáme 50 zlaťáků.");
                player.Money = Math.Max(0, player.Money - 50);
            }
        }




        private void DisplayExpeditionResults()
        {
            Console.WriteLine($"\u001b[31mZdraví: {player.GraphicHealth()}  \u001b[0m\n");

            int foundGold = GenerateRandomGoldAmount();
            Console.WriteLine($"Získali jste \u001b[33m{foundGold} zlaťáků.\u001b[0m\n");

            if (CheckProbability(10))
            {
                Console.WriteLine("\u001b[32mNalezli jste bednu se zlatem!\u001b[0m\n");
                int additionalGold = GenerateRandomGoldAmount();
                Console.WriteLine($"\u001b[33mZískali jste další {additionalGold} zlaťáků.\u001b[0m\n");
                foundGold += additionalGold;
            }

            //the amount of gold the player earns
            player.Money += foundGold;

            Thread.Sleep(3500);
            Console.Clear();
        }

        private int GenerateRandomGoldAmount()
        {
            Random random = new Random();
            return random.Next(20, 100);
        }


    }
}
