using DungeonGameConsole.Mechanic;
using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Dungeons
{

    public class Arena : FightLogic
    {
        private Mag player;
        private Cube cube;
        private List<Fighter> arenaEnimies;
        private FightLogic fightLogic;


        public Arena(Mag player, List<Fighter> fighters, Cube cube)
        {
            this.player = player;
            this.arenaEnimies = fighters ?? new List<Fighter>();
            this.cube = cube;
            this.fightLogic = new FightLogic();

        }

        public void ShowMenuArena()
        {






            while (true)
            {
                Console.Clear();
                Console.WriteLine("\u001b[90m __          __  _                            _                                     \r\n" +
            " \\ \\        / / | |                          | |                                    \r\n" +
            "  \\ \\  /\\  / /__| | ___ ___  _ __ ___   ___  | |_ ___     __ _ _ __ ___ _ __   __ _ \r\n" +
            "   \\ \\/  \\/ / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\   / _` | '__/ _ \\ '_ \\ / _` |\r\n" +
            "    \\  /\\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | | (_| | | |  __/ | | | (_| |\r\n" +
            "     \\/  \\/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/   \\__,_|_|  \\___|_| |_|\\__,_|\r\n" +
            "                                                                                    \r\n" +
            "                                                                                    \u001b[0m ");



                Console.WriteLine("                                                              Stav hrace\n");
                Console.WriteLine("   --------------------------------------------------------");
                Console.WriteLine($"   |  \u001b[33mCo chcete dělat v aréně\u001b[0m                             | \u001b[31m  Zdraví: {player.GraphicHealth()}  \u001b[0m");
                Console.WriteLine("   --------------------------------------------------------");

                Console.WriteLine("   |  1. Bojovat \u001b[31m(Nelze se vrátit až po zabití nepřítele)\u001b[0m\u001b[0m |");
                Console.WriteLine("   --------------------------------------------------------");

                Console.WriteLine($"   |  2. Jít do obchodu                                   | \u001b[34m  Mana: {player.GraphicMana()}    \u001b[0m");
                Console.WriteLine("   --------------------------------------------------------\n   |                                                      |");

                Console.WriteLine("   |  \u001b[31m0. Vrátit se do hlavního menu \u001b[0m                      |");
                Console.WriteLine("   --------------------------------------------------------");













                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Zadejte platnou volbu (0-2).");
                }


                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Vracíte se do hlavního menu.");
                        Console.Clear();
                        return;
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

            if (opponents.Count == 0)
            {
                Console.WriteLine("V této lokalitě již nejsou žádní nepřátelé nebo jsou všichni poraženi.");
                Console.WriteLine("Pro pokoracivani stisknete klavesu enter");
                Console.ReadLine();
                Console.Clear();
                return;
            }


            fightLogic.Fight(player, opponents);
        }

        public List<Fighter> SelectOpponents()
        {
            return arenaEnimies.Where(enemy => !defeatedEnemies.Contains(enemy)).ToList();
        }

    }
}
