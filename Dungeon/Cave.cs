using System;
using System.Collections.Generic;
using DungeonGame.Mechanic;
using DungeonGame.Role;

namespace DungeonGame.Dungeon
{
    public class Cave : FightLogic
    {
        private Mag player;
        private Cube cube;
        private FightLogic fightLogic;
        private List<Fighter> caveEnemies;

        public Cave(Mag player, Cube cube, List<Fighter> caveEnemies)
        {
            this.player = player;
            this.cube = cube;
            this.fightLogic = new FightLogic(); // Instantiate FightLogic in the constructor
            this.caveEnemies = caveEnemies; // Generate cave enemies in the constructor
        }




        public void ExploreCave()
        {
            Console.Clear();

            

            while (true)
            {
                Console.WriteLine("\u001b[36mVítejte v jeskyni!\u001b[0m");
                Console.WriteLine("\u001b[33mCo chcete dělat v jeskyni\u001b[0m");
                Console.WriteLine("1. Bojovat \u001b[31m(Nelze se vrátit až po zabití nepřítele)\u001b[0m\u001b[0m");
                Console.WriteLine("2. Jít do obchodu");
                Console.WriteLine("0. Vrátit se do hlavního menu");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Zadejte platnou volbu (0-2).");
                }

                if (choice == 0)
                {
                    Console.WriteLine("Opouštíte jeskyni.");
                    Console.Clear();
                    return;
                }

                switch (choice)
                {
                    case 1:
                        Fight(player);
                        break;
                    case 2:
                        Shop.EnterShop(player);
                        break;
                }
            }
        }

        public void Fight(Mag player)
        {
            Console.Clear();

            List<Fighter> opponents = SelectOpponents();

            if (opponents == null || opponents.All(enemy => defeatedEnemies.Contains(enemy)))
            {
                Console.WriteLine("V této lokalitě již nejsou žádní nepřátelé nebo jsou všichni poraženi.");
                Console.ReadLine(); // Počkejte, než hráč stiskne klávesu
                return;
            }

            fightLogic.Fight(player, opponents);
        }

        public List<Fighter> SelectOpponents()
        {
            if (caveEnemies == null || caveEnemies.All(enemy => defeatedEnemies.Contains(enemy)))
            {
                Console.WriteLine("V této lokalitě již nejsou žádní nepřátelé nebo jsou všichni poraženi.");
                Console.ReadLine(); // Počkejte, než hráč stiskne klávesu
                return null;
            }

            return caveEnemies.Where(enemy => !defeatedEnemies.Contains(enemy)).ToList();
        }
    }
}
