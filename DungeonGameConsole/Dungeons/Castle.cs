using DungeonGameConsole.Mechanic;
using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Dungeons
{
    public class Castle : FightLogic
    {
        private Mag player;
        private Cube cube;
        private List<Fighter> castleEnemies;
        private FightLogic fightLogic;


        public Castle(Mag player, Cube cube, List<Fighter> castleEnemies)

        {
            this.player = player;
            this.cube = cube;
            this.castleEnemies = castleEnemies;
            this.fightLogic = new FightLogic();
        }




        public void ExploreCastle()
        {
            Console.Clear();


            while (true)
            {

                Console.WriteLine("\u001b[36mVítejte v hradu!\u001b[0m");
                Console.WriteLine("\u001b[33mCo chcete dělat v hradu\u001b[0m");
                Console.WriteLine("1. Bojovat \u001b[31m(Nelze se vratit až po zabití nepřitele)\u001b[0m\u001b[0m");
                Console.WriteLine("2. Jit do obchodu");
                Console.WriteLine("0. Vrátit se do hlavního menu");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Zadejte platnou volbu (1-4).");
                }

                if (choice == 0)
                {
                    Console.WriteLine("Opouštíte hrad.");
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
                Console.ReadLine();
                return;
            }


            fightLogic.Fight(player, opponents);
        }





        public List<Fighter> SelectOpponents()
        {
            if (castleEnemies == null || castleEnemies.All(enemy => defeatedEnemies.Contains(enemy)))
            {
                Console.WriteLine("V této lokalitě již nejsou žádní nepřátelé.");
                Console.ReadLine();
                return null;
            }



            return castleEnemies.Where(enemy => !defeatedEnemies.Contains(enemy)).ToList();
        }

    }
}
