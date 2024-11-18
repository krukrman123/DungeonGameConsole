using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Dungeons
{
    class Village
    {

        public static void ExploreVillage(Mag player)
        {




            while (true)
            {
                Console.Clear();


                Console.WriteLine("\u001b[90m __          __  _                             _                _ _ _                  \r\n" +
                " \\ \\        / / | |                           | |              (_) | |                 \r\n" +
                "  \\ \\  /\\  / /__| | ___ ___  _ __ ___   ___   | |_ ___   __   ___| | | __ _  __ _  ___ \r\n" +
                "   \\ \\/  \\/ / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\  | __/ _ \\  \\ \\ / / | | |/ _` |/ _` |/ _ \\\r\n" +
                "    \\  /\\  /  __/ | (_| (_) | | | | | |  __/  | || (_) |  \\ V /| | | | (_| | (_| |  __/\r\n" +
                "     \\/  \\/ \\___|_|\\___\\___/|_| |_| |_|\\___|   \\__\\___/    \\_/ |_|_|_|\\__,_|\\__, |\\___|\r\n" +
                "                                                                             __/ |     \r\n" +
                "                                                                            |___/      \u001b[0m");





                Console.Write($" \u001b[31m  Zdraví:          {player.GraphicHealth()}  \u001b[0m");
                Console.WriteLine($" \u001b[34m  Mana:        {player.GraphicMana()}    \u001b[0m");
                Console.WriteLine("   -----------------------------------------------------------------------------------------------");
                Console.WriteLine($"    \u001b[90mObrana:    {player.defense}\u001b[0m       ");
                Console.WriteLine("   ----------------------");
                Console.WriteLine($"    \u001b[35mUtok:      {player.attack}\u001b[0m      ");
                Console.WriteLine("   ----------------------");
                Console.WriteLine($"    \u001b[34mMagicky Utok: {player.magickAttack}\u001b[0m      ");
                Console.WriteLine("   ----------------------");
                Console.WriteLine($"    \u001b[32mPeníze:    {player.Money}\u001b[0m        ");
                Console.WriteLine("   ----------------------");
                Console.WriteLine($"    \u001b[33mZkušenosti:{player.Experience}\u001b[0m        ");
                Console.WriteLine("   ----------------------");






                Console.WriteLine("                         Možnosti vylepšení:");
                Console.WriteLine("   ----------------------------------------------------------------");
                Console.WriteLine($"   | \u001b[31m            1. Zvýšit životy (cena: 10 zlataku)        \u001b[0m      |");
                Console.WriteLine("   ----------------------------------------------------------------");
                Console.WriteLine($"   | \u001b[90m            2. Zvýšit obranu (cena: 15 zlataku)        \u001b[0m      |");
                Console.WriteLine("   ----------------------------------------------------------------");
                Console.WriteLine($"   | \u001b[35m            3. Zvýšit útok (cena: 20 zlataku)          \u001b[0m      |");
                Console.WriteLine("   ----------------------------------------------------------------");
                Console.WriteLine($"   | \u001b[34m            4. Zvýšit magicky útok (cena: 35 zlataku)        \u001b[0m|");
                Console.WriteLine("   ----------------------------------------------------------------");
                Console.WriteLine($"   | \u001b[32m            5. Doplnit Zivoty (cena: 8 zlataku)        \u001b[0m      |");
                Console.WriteLine("   ----------------------------------------------------------------");
                Console.WriteLine($"   | \u001b[33m            6. Navštívit obchod                        \u001b[0m      |");
                Console.WriteLine("   ----------------------------------------------------------------\n   |                                                              |");
                Console.WriteLine($"   | \u001b[31m            0. Odejít z vesnice                        \u001b[0m      |");
                Console.WriteLine("   ----------------------------------------------------------------");


                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 6)
                {
                    Console.WriteLine("Zadejte platnou volbu (0-6).");
                }


                if (choice == 0)
                {

                    Console.WriteLine("Opouštíte vesnici.");
                    Thread.Sleep(1500);
                    break;
                }


                switch (choice)
                {
                    case 1:
                        if (player.Money >= 10)
                        {

                            player.BuyArmor();
                            Console.WriteLine($"Zivoty byly vylepšené \u001b[31m({player.health} HP)\u001b[0m");
                        }
                        else
                        {
                            Console.WriteLine("Nemáte dost peněz na vylepšení životů.");
                        }
                        break;
                    case 2:
                        if (player.Money >= 15)
                        {
                            player.defense += 5;
                            player.Money -= 15;
                            Console.WriteLine($"Obrana byla zvýšena \u001b[90m({player.defense} Arm)\u001b[0m ");
                        }
                        else
                        {
                            Console.WriteLine("Nemáte dost peněz na vylepšení obrany.");
                        }
                        break;
                    case 3:
                        if (player.Money >= 20)
                        {
                            player.attack += 10;
                            player.Money -= 20;
                            Console.WriteLine($"Útok byl zvišen o 10 stav \u001b[35m({player.attack}Dmg)\u001b[0m");
                        }
                        else
                        {
                            Console.WriteLine("Nemáte dost peněz na vylepšení útoku.");
                        }
                        break;
                    case 4:
                        if (player.Money >= 35)
                        {
                            player.magickAttack += 10;
                            player.Money -= 35;
                            Console.WriteLine($"Magicky útok byl zvišen o 10 stav \u001b[34m({player.magickAttack}Dmg)\u001b[0m");
                        }
                        else
                        {
                            Console.WriteLine("Nemáte dost peněz na vylepšení Magickeho útoku.");
                        }
                        break;
                    case 5:
                        if (player is Mag)
                        {
                            Mag magPlayer = (Mag)player;
                            magPlayer.BuyMissingHealth();
                        }
                        else
                        {
                            Console.WriteLine("Tato možnost není dostupná pro vaši postavu.");
                        }
                        break;
                    case 6:
                        Shop.EnterShop(player);
                        break;
                }

                Thread.Sleep(1500);
            }
        }

    }
}
