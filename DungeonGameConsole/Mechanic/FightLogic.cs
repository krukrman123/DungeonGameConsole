using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Mechanic
{
    public class CombatState
    {
        public Mag Player { get; set; }
        public Fighter Opponent { get; set; }


        public CombatState(Mag player, Fighter opponent)
        {
            Player = player;
            Opponent = opponent;
        }

    }

    public class FightLogic
    {
        public static List<Fighter> defeatedEnemies = new List<Fighter>();

        public CombatState combatState;

        public bool UsedShield { get; set; } = false;
        public int TemporaryDefenseBonus { get; set; } = 0;



        #region ZobrazeniHRY
        public Fighter Fight(Mag player, List<Fighter> opponents)
        {
            Fighter opponent = SelectOpponent(opponents);

            combatState = new CombatState(player, opponent);



            Fighter b1 = player;
            Fighter b2 = opponent;
            Console.WriteLine("Vítejte v souboji!");
            Console.WriteLine("Dnes se utkají {0} s {1}! \n", player.name, opponent.name);

            // Swap fighters
            bool zacinaBojovnik2 = opponent.cube.ThrowIt() <= opponent.cube.ReturnCountSten() / 2;
            if (zacinaBojovnik2)
            {
                b1 = player;
                b2 = opponent;
            }
            Console.WriteLine("Začínat bude bojovník {0}! \nZápas může začít...", b1.name);
            Console.ReadKey();

            while (b1.IsAlive() && b2.IsAlive())
            {
                if (!b1.IsAlive() || !b2.IsAlive())
                {
                    break;
                }

                if (ExecuteRound(b1, b2))
                {
                    break;
                }

                if (b1.IsDead() || b2.IsDead())
                {
                    break;
                }

                if (ExecuteRound(b2, b1))
                {
                    break;
                }



                Console.WriteLine();
            }

            if (b2.IsDead())
            {

                Draw(combatState.Player, combatState.Opponent);
                Console.ForegroundColor = ConsoleColor.White;

                defeatedEnemies.Add(opponent);
                DisplayVictorySummary(player);
                Thread.Sleep(2000);

                Console.Clear();
                return player;
            }
            else
            {


                Draw(combatState.Player, combatState.Opponent);

                Console.WriteLine("\u001b[31m __ \r\n/" +
                    "\\_/" +
                    "\\  ___   _   _    / /    ___   ___   ___ \r\n" +
                    "\\_ _/ / _ " +
                    "\\ | | | |  / /    / _ \\ / __| / _ " +
                    "\\\r\n / " +
                    "\\ | (_) || |_| | / /___ | (_) |" +
                    "\\__ " +
                    "\\|  __/\r\n \\_/  \\___/  " +
                    "\\__,_| \\____/  \\___/ |___/ \\___|\u001b[0m\n");

                Thread.Sleep(2500);

                int penaltyMoney = player.Money;
                player.Money = 0;
                player.RestartHealth();
                opponent.RestartHealth();
                Console.WriteLine($" \u001b[31mZtrácíte {penaltyMoney} zlaťáků.\u001b[0m");
                Thread.Sleep(2500);
                Console.Clear();

                return player;
            }
        }

        private bool ExecuteRound(Fighter attacker, Fighter defender)
        {
            Draw(combatState.Player, combatState.Opponent);
            WriteMessage(attacker.ReturnLastMessage()); // Message about attack
            WriteMessage(defender.ReturnLastMessage()); // message about defense

            if (attacker == combatState.Player)
            {
                Console.WriteLine($"{combatState.Player.name}vyberte akci:");
                Console.WriteLine("1. Útok");
                Console.WriteLine("2. Použít předmět");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
                {
                    Console.Clear();
                    Console.WriteLine("Zadejte platnou volbu (1-2).");

                    Draw(combatState.Player, combatState.Opponent);
                    WriteMessage(attacker.ReturnLastMessage());
                    WriteMessage(defender.ReturnLastMessage());

                    Console.WriteLine($"{combatState.Player.name}, vyberte akci:");
                    Console.WriteLine("1. Útok");
                    Console.WriteLine("2. Použít předmět");
                }

                switch (choice)
                {
                    case 1:
                        attacker.Combat(defender);
                        break;
                    case 2:
                        UseItem(combatState.Player, combatState.Opponent);
                        break;
                }
            }
            else
            {
                attacker.Combat(defender);
            }




            return false;
        }



        public Fighter SelectOpponent(List<Fighter> opponents)
        {
            Console.Clear();

            Console.WriteLine("Vyberte protivníka:");

            for (int i = 0; i < opponents.Count; i++)
            {
                Fighter enemy = opponents[i];

                // Check if enemy was defeat
                if (!defeatedEnemies.Contains(enemy))
                {
                    Console.WriteLine($"{i + 1}. {enemy.name}");

                }
                else
                {
                    Console.WriteLine($"{i + 1}. \u001b[32m{enemy.name} (Poražen)\u001b[0m");


                }

            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > opponents.Count || defeatedEnemies.Contains(opponents[choice - 1]))
            {
                Console.WriteLine("Zadejte platné číslo nepřítele. \n(Je bud porazen nebo neni v nabidce)");
            }




            return opponents[choice - 1];

        }



        private void Draw(Mag player, Fighter opponent)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(@"
       __    ____  ____  _  _    __   
      /__\  (  _ \( ___)( \( )  /__\  
     /(__)\  )   / )__)  )  (  /(__)\ 
    (__)(__)(_)\_)(____)(_)\_)(__)(__)");
            Console.WriteLine();

            WriteFighter(player);
            Console.WriteLine();
            WriteFighter(opponent);
            Console.WriteLine();
        }


        public void ResetTemporaryDefense(Mag player)
        {
            player.defense -= TemporaryDefenseBonus;
            TemporaryDefenseBonus = 0;
            UsedShield = false;
        }


        private void WriteFighter(Fighter fighter)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"Jméno: {fighter.name}");
            Console.WriteLine("-------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Zdraví: {fighter.GraphicHealth()}");

            if (fighter is Mag magFighter)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Mana:   {magFighter.GraphicMana()}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Útok: {fighter.attack}");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Obrana: {fighter.defense}");
        }

        private void WriteMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Thread.Sleep(500);

        }




        #endregion









        private void UseItem(Mag player, Fighter opponent)
        {
            while (true)
            {
                var usableItems = player.Inventory
                    .Where(item => item.Type == ItemType.Sword || item.Type == ItemType.HealthPotion)
                    .ToList();


                // If the player has not any items to use
                if (usableItems.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Nemáte žádné použitelné předměty (meč nebo léčivý lektvar) v inventáři.");
                    Console.WriteLine("1. Útok");
                    Console.WriteLine("2. Zkusit znovu vybrat předmět (není dostupný)");

                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Nemáte žádné použitelné předměty (meč nebo léčivý lektvar) v inventáři.");
                        Console.WriteLine("1. Útok");
                        Console.WriteLine("2. Zkusit znovu vybrat předmět (není dostupný)");
                    }

                    if (choice == 1)
                    {

                        player.Combat(opponent);
                        return;
                    }
                }

                // If the player has any items to use
                Console.Clear();
                Console.WriteLine("Vyberte předmět k použití:");
                DisplayInventorySectionWithNumbers("Použitelné předměty", usableItems, (item, index) => $"{index + 1}. {item.Name,-20} | {item.Value} zlaťáků");
                Console.WriteLine("\u001b[31m0. Zpět\u001b[0m");

                int itemIndex;
                while (!int.TryParse(Console.ReadLine(), out itemIndex) || itemIndex < 0 || itemIndex > usableItems.Count)
                {
                    Console.WriteLine("Zadejte platné číslo předmětu nebo 0 pro návrat do souboje.");
                }


                // If the player choose choice back 
                if (itemIndex == 0)
                {
                    return;
                }
                else
                {

                    Item selectedItem = usableItems[itemIndex - 1];

                    switch (selectedItem.Type)
                    {
                        case ItemType.HealthPotion:
                            UseHealthPotion(player, opponent);
                            break;
                        case ItemType.Sword:
                            UseSword(player, opponent);
                            break;
                        default:
                            Console.WriteLine("Tento předmět nelze použít.");
                            break;
                    }


                    usableItems = player.Inventory
                        .Where(item => item.Type == ItemType.Sword || item.Type == ItemType.HealthPotion)
                        .ToList();


                    // If are not useable items the player retunr to fight
                    if (usableItems.Count == 0)
                    {
                        Console.WriteLine("Nemáte žádné další použitelné předměty.");
                        return;
                    }
                }
            }
        }







        public static void DisplayInventorySectionWithNumbers<T>(string sectionTitle, List<T> items, Func<T, int, string> itemFormatter)
        {
            Console.WriteLine($"+-------------------------+------------+");
            Console.WriteLine($"| \u001b[33m{sectionTitle}\u001b[0m     | \u001b[32mCena\u001b[0m       |");
            Console.WriteLine($"+-------------------------+------------+");

            for (int i = 0; items != null && i < items.Count; i++)
            {
                Console.WriteLine($"| \u001b[33m{itemFormatter(items[i], i)}\u001b[0m  |");
            }

            Console.WriteLine("+-------------------------+------------+");
        }





        private void UseHealthPotion(Mag player, Fighter opponent)
        {
            // Check if the player has a HealthPotion in inventory
            HealthPotion healthPotion = player.Inventory.OfType<HealthPotion>().FirstOrDefault();

            if (healthPotion != null)
            {
                healthPotion.Use(player, opponent);

                player.Inventory.Remove(healthPotion);
            }
            else
            {
                Console.WriteLine($"{player.name} nemá žádný léčivý lektvar v inventáři.");
            }
        }




        private void UseSword(Mag player, Fighter opponent)
        {
            // Check if the player has a Sword in inventory
            Sword sword = player.Inventory.OfType<Sword>().FirstOrDefault();

            if (sword != null)
            {
                sword.Use(player, opponent);



                player.Inventory.Remove(sword);




            }
            else
            {
                Console.WriteLine($"{player.name} nemá žádný mec v inventáři.");
                Thread.Sleep(2200);
            }


            Console.Clear();
        }






        #region OceneniZaHru


        void HandleRewards(Mag player)
        {
            int earnedMoney = CalculateEarnedMoney();
            player.Money += earnedMoney;

            int earnedExperience = CalculateEarnedExperience();
            player.Experience += earnedExperience;

            Item earnedItem = GenerateRandomItem();
            player.Inventory.Add(earnedItem);
        }

        int CalculateEarnedMoney()
        {
            Random random = new Random();
            return random.Next(10, 30);
        }

        int CalculateEarnedExperience()
        {
            Random random = new Random();
            return random.Next(5, 15);
        }








        void DisplayVictorySummary(Mag player)
        {


            Console.WriteLine("Vyhrál jste! Gratulujeme!");

            HandleRewards(player);

            Console.WriteLine($"Získali jste {CalculateEarnedMoney()} peněz za výhru!");
            Console.WriteLine($"Získali jste {CalculateEarnedExperience()} zkušeností za výhru!");

            if (player.Inventory.Any())
            {
                Item earnedItem = player.Inventory.Last();
                Console.WriteLine($"Získali jste předmět: {earnedItem.Name}");
            }
            else
            {
                Console.WriteLine("Player's inventory is empty.");
            }

        }


        private Item GenerateRandomItem()
        {

            Random random = new Random();
            ItemType randomType = (ItemType)random.Next(Enum.GetValues(typeof(ItemType)).Length);
            int randomValue = random.Next(5, 20); // item price is 5 a 20
            bool isSellable = random.Next(0, 2) == 0; // 50% chance that itme can sell

            string itemName = GenerateItemName(randomType);


            return new Item(itemName, randomType, randomValue, isSellable, true);
        }


        private string GenerateItemName(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.ManaElixir:
                    return "Mana lektvar";
                case ItemType.MagicScroll:
                    return "Magický svitek";
                case ItemType.EnchantedAmulet:
                    return "Kouzelný amulet";
                case ItemType.Shield:
                    return "Štít";
                case ItemType.Ring:
                    return "Prsten";
                case ItemType.Robe:
                    return "Plášť";
                case ItemType.Book:
                    return "Kniha kouzel";
                case ItemType.Dagger:
                    return "Dýka temnoty";
                case ItemType.Helmet:
                    return "Kouzelná přilba";
                case ItemType.AmuletOfWisdom:
                    return "Amulet moudrosti";
                case ItemType.Gauntlets:
                    return "Rukavice odolnosti";
                case ItemType.AncientScroll:
                    return "Starodávný svitek";
                case ItemType.Spellbook:
                    return "Kniha zaklínadel";
                case ItemType.InvisibilityCloak:
                    return "Plášť neviditelnosti";
                case ItemType.PoisonedArrow:
                    return "Otrávená šípka";
                case ItemType.LuckyCharm:
                    return "Štěstíčko";
                default:
                    Console.WriteLine("Nic jste neziskal za item");
                    return string.Empty;
            }
        }


    }

    #endregion
}

