using DungeonGame.Mechanic;
using DungeonGame.Role;
using System;
using System.Diagnostics;
using System.Threading;

namespace DungeonGame.Dungeon
{
    public class Expedition : FightLogic
    {
        private Mag player;
        private Cube cube;
        private int expeditionDurationMinutes = 1; // Čas doby dobrodružství
        private Stopwatch stopwatch;

        public Expedition(Mag player, Cube cube) : base()
        {
            this.player = player;
            this.cube = cube;
            this.stopwatch = new Stopwatch();
        }

        public void Explore()
        {
            
            Console.WriteLine("Vyrazili jste na dobrodružství!");

            // Nastavíme dobu trvání vypravy na 1 minutu (60000 milisekund)
            int expeditionDuration = 5000;

            // Vytvoříme nový časovač
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < expeditionDuration)
            {
                // Zbývající čas vypravy v milisekundách
                long remainingTime = expeditionDuration - stopwatch.ElapsedMilliseconds;

                // Převádíme zbývající čas na sekundy
                int remainingSeconds = (int)Math.Ceiling(remainingTime / 1000.0);

                // Zobrazíme zbývající čas digitálně na jednom řádku
                Console.Write($"\rZbývající čas: {remainingSeconds} sekund");


                // Pravděpodobnost, že hráč utrpí poškození (např. 10%)
                if (CheckProbability(10))
                {
                    Console.Clear();
                    Console.WriteLine("Vyrazili jste na dobrodružství!");
                    Console.Write($"\rZbývající čas: {remainingSeconds} sekund");

                    int damage = GenerateRandomDamage();
                    Console.WriteLine($"\u001b[31m\nNarazili jste na nebezpečné místo! Utrpěli jste {damage} poškození.\u001b[0m");
                    player.ReceiveDamage(damage);
                }




                // Čekáme 1 sekundu
                Thread.Sleep(1000);
            }


     


            // Počkáme na ukončení vypravy
            stopwatch.Stop();

            // Zobrazíme výsledky vypravy
            DisplayExpeditionResults();
        }










    private void DisplayExpeditionResults()
        {
            Console.Clear();
            Console.WriteLine("\nVyprava skončila!");
            Console.WriteLine($"\u001b[31mZdraví: {player.GraphicHealth()}  \u001b[0m\n");

            // Příklad generování náhodných výsledků (upravte podle potřeby)
            int foundGold = GenerateRandomGoldAmount();
            Console.WriteLine($"Získali jste \u001b[33m{foundGold} zlaťáků.\u001b[0m\n");
            
            // Pravděpodobnost nalezení bedny se zlatem
            if (CheckProbability(10))
            {
                Console.WriteLine("\u001b[32mNalezli jste bednu se zlatem!\u001b[0m\n");
                int additionalGold = GenerateRandomGoldAmount();
                Console.WriteLine($"\u001b[33mZískali jste další {additionalGold} zlaťáků.\u001b[0m\n");
                foundGold += additionalGold;
            }

            // Můžete přidat další odměny nebo události podle potřeby.

            // Přidáme získané zlato hráči
            player.Money += foundGold;

            
            Thread.Sleep(3500);
            Console.Clear();

        }

        private int GenerateRandomGoldAmount()
        {
            Random random = new Random();
            return random.Next(20, 100);
        }

        private int GenerateRandomDamage()
        {
            Random random = new Random();
            return random.Next(10, 30);
        }

        private bool CheckProbability(int probabilityPercentage)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101); // Generuje náhodné číslo od 1 do 100

            // Porovná náhodné číslo s pravděpodobností
            return randomNumber <= probabilityPercentage;
        }
    }
}
