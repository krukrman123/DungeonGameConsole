using DungeonGameConsole.Dungeons;
using DungeonGameConsole.Mechanic;
using DungeonGameConsole.Role;
using DungeonGameConsole.Interface;
using System.Numerics;

class Program
{


    static void Main(string[] args)
    {

        Console.Title = "Dungeon of game";
        Game game = new Game();
        game.Start();

    }

}

class Game
{
    private Mag player;
    private Cube cube;
    private Arena arena;
    private Castle castle;
    private Cave cave;
    private Expedition expedition;

    private string GetPlayerName()
    {
        Console.Write("Zadejte jméno bojovníka: ");
        return Console.ReadLine();

    }



    public Game()
    {



        //  name, health, attack, defense, Cube, mana, magicAttack, money, experience
        string playerName = GetPlayerName();
        Console.Clear();
        player = new Mag(playerName, 100, 100, 10, new Cube(), 50, 150, 120, 101);
        cube = new Cube();
        arena = new Arena(player, GenerateFighters(60), cube);
        castle = new Castle(player, cube, GenerateFightersCastle(40));
        cave = new Cave(player, cube, GenerateFightersCave(40));
        expedition = new Expedition(player, cube);
    }






    public void Start()
    {




        while (true)
        {
            Console.Clear();

            Console.WriteLine("" +
          "\u001b[90m" +
" ______                                      _____                     \r\n" +
" |  _  \\                                    |  __ \\                    \r\n" +
" | | | |_   _ _ __   __ _  ___  ___  _ __   | |  \\/_ __ ___   __ _  ___ \r\n" +
" | | | | | | | '_ \\ / _` |/ _ \\/ _ \\| '_ \\  | | __| '_ ` _ \\ / _` |/ _ \\\r\n" +
" | |/ /| |_| | | | | (_| |  __/ (_) | | | | | |_\\ \\ | | | | | (_| |  __/\r\n" +
" |___/  \\__,_|_| |_|\\__, |\\___|\\___/|_| |_|  \\____/_| |_| |_|\\__,_|\\___|\r\n" +
"                     __/ |                                              \r\n" +
"                    |___/                                               \u001b[0m\n");


            Console.WriteLine("   ----------------------------------------------------------------");
            Console.WriteLine("   |        \u001b[33mCo chcete dělat nyní?\u001b[0m                                 |");
            Console.WriteLine("   ----------------------------------------------------------------");
            Console.WriteLine("   |        \u001b[36m1. Průzkum vesnice\u001b[0m                                    |");
            Console.WriteLine("   ----------------------------------------------------------------");
            Console.WriteLine("   |        \u001b[36m2. Bojovat v aréně\u001b[0m                                    |");
            Console.WriteLine("   ----------------------------------------------------------------");
            Console.WriteLine("   |        \u001b[36m3. Průzkum jeskyně\u001b[0m                                    |");
            Console.WriteLine("   ----------------------------------------------------------------");
            Console.WriteLine("   |        \u001b[36m4. Průzkum hradu  \u001b[0m                                    |");
            Console.WriteLine("   ----------------------------------------------------------------");
            Console.WriteLine("   |        \u001b[36m5. Výprava (Cena 50 zlataku)\u001b[0m                          |");
            Console.WriteLine("   ----------------------------------------------------------------\n   |                                                              |");
            Console.WriteLine("   |        \u001b[31m0. Ukončit hru\u001b[0m                                        |");
            Console.WriteLine("   ----------------------------------------------------------------");


            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
            {
                Console.WriteLine("Zadejte platnou volbu (0-5).");
            }


            switch (choice)
            {
                case 1:
                    Village.ExploreVillage(player);
                    break;
                case 2:
                    arena.ShowMenuArena();
                    break;
                case 3:
                    if (player.Experience >= Constants.CaveExperienceRequirement)
                    {
                        cave.ExploreCave();
                    }
                    else
                    {
                        Console.WriteLine($" \u001b[31mNemáte dostatek zkušeností na průzkum [Jeskyně]. Potřebujete alespoň {Constants.CaveExperienceRequirement} zkušeností.\u001b[0m");
                        Thread.Sleep(2500);
                        Console.Clear();
                    }
                    break;
                case 4:
                    if (player.Experience >= Constants.CastleExperienceRequirement)
                    {
                        castle.ExploreCastle();
                    }
                    else
                    {
                        Console.WriteLine($" \u001b[31mNemáte dostatek zkušeností na průzkum [Hradu]. Potřebujete alespoň {Constants.CastleExperienceRequirement} zkušeností.\u001b[0m");
                        Thread.Sleep(2500);
                        Console.Clear();
                    }
                    break;
                case 5:
                    if (player.Experience >= Constants.ExpeditionExperienceRequirement && player.Money >= Constants.ExpeditionExperienceCost)
                    {
                        player.Money -= Constants.ExpeditionExperienceCost;
                        expedition.Explore();
                    }
                    else if (player.Experience < Constants.ExpeditionExperienceRequirement)
                    {
                        Console.WriteLine($" \u001b[31mNemáte dostatek zkušeností na tuto vypravu. Potřebujete alespoň {Constants.ExpeditionExperienceRequirement} zkušeností.\u001b[0m");
                        Thread.Sleep(2500);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine($" \u001b[31mNemáte dostatek zlataku na tuto vypravu. Potřebujete alespoň {Constants.ExpeditionExperienceCost} zlataku.\u001b[0m");
                        Thread.Sleep(2500);
                        Console.Clear();
                    }
                    break;
                case 0:
                    Console.Clear();
                    Console.Write("Opravdu chcete ukončit hru? (ano/ne): ");
                    string confirmation = Console.ReadLine().ToLower();

                    if (confirmation == "ano" || confirmation == "a")
                    {
                        Console.WriteLine("Ukončujete hru.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Pokračujete v hře.");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    break;

            }

        }
    }





    List<Fighter> GenerateFighters(int count)
    {
        List<Fighter> fighters = new List<Fighter>();

        string[] names = { "Gladiator", "Pit Fighter", "Arena Champion", "Thunderblade", "Shadow Warrior", "Bloodhound", "Ironclad", "Savage Reaper", "Spartan", "Stormbringer",
                "Viper", "Blazeheart", "Juggernaut", "Swiftshadow", "Moonlight Sentinel", "Dark Phoenix", "Thunderstrike", "Warrior Princess", "Venomancer", "Steel Serpent", "Inferno Warden",
                "Blizzard Berserker", "Rogue Ronin", "Crimson Crusader", "Nightshade Ninja", "Lunar Lancer", "Silent Specter", "Eternal Enforcer", "Mystic Marauder" };

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            string name = names[random.Next(names.Length)] + (i + 1);
            int health = random.Next(50, 800);
            int attack = random.Next(20, 50);
            int defense = random.Next(5, 15);

            fighters.Add(new Fighter(name, health, attack, defense, new Cube()));
        }

        return fighters.OrderBy(f => f.health).ToList();
    }


    List<Fighter> GenerateFightersCastle(int count)
    {
        List<Fighter> fighters = new List<Fighter>();

        string[] names = { "Orc", "Goblin", "Troll", "Skeleton", "Dragon", "Witch", "Werewolf", "Minotaur", "Zombie", "Harpy", "Specter", "Cyclops", "Banshee", "Wyvern",
                "Lich", "Manticore", "Chimera", "Basilisk", "Siren", "Centaur", "Medusa", "Kraken", "Cerberus", "Gorgon", "Harpie", "Naga", "Griffin", "Phoenix", "Satyr", "Cockatrice",
                "Cait Sith", "Kobold", "Mummy", "Djinn", "Bogeyman", "Succubus", "Incubus", "Imp", "Baphomet" };

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            string name = names[random.Next(names.Length)] + (i + 1);
            int health = random.Next(2000, 8000);
            int attack = random.Next(80, 280);
            int defense = random.Next(30, 80);

            fighters.Add(new Fighter(name, health, attack, defense, new Cube()));
        }

        return fighters.OrderBy(f => f.health).ToList();
    }


    List<Fighter> GenerateFightersCave(int count)
    {
        List<Fighter> fighters = new List<Fighter>();

        string[] names = { "Orc", "Goblin", "Troll", "Skeleton", "Dragon", "Witch", "Werewolf", "Minotaur", "Zombie", "Harpy", "Specter", "Cyclops", "Banshee", "Wyvern",
                "Lich", "Manticore", "Chimera", "Basilisk", "Siren", "Centaur", "Medusa", "Kraken", "Cerberus", "Gorgon", "Harpie", "Naga", "Griffin", "Phoenix", "Satyr", "Cockatrice",
                "Cait Sith", "Kobold", "Mummy", "Djinn", "Bogeyman", "Succubus", "Incubus", "Imp", "Baphomet" };

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            string name = names[random.Next(names.Length)] + (i + 1);
            int health = random.Next(2000, 8000);
            int attack = random.Next(80, 280);
            int defense = random.Next(30, 80);

            fighters.Add(new Fighter(name, health, attack, defense, new Cube()));
        }

        return fighters.OrderBy(f => f.health).ToList();
    }
}






static class Constants
{
    public const int CaveExperienceRequirement = 150;       //  Needed experience for enter to the [Cave]
    public const int CastleExperienceRequirement = 350;     // Needed experience for enter to the [Castle]
    public const int ExpeditionExperienceRequirement = 100;  // Needed experience for [Expedition]
    public const int ExpeditionExperienceCost = 50;         // Price for expedition (golds)
}


