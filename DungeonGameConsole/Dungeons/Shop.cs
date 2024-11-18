using DungeonGameConsole.Mechanic;
using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Dungeons
{
    public static class Shop
    {
        private static List<Item> shopInventory;

        static Shop()
        {
            FightLogic fightLogic = new FightLogic();
            shopInventory = new List<Item>
            {

                new Sword("Sword", 2, true, true, 20),
                new HealthPotion("Health Potion", 4, true, 18),



        };




        }


        public static void EnterShop(Mag player)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\u001b[33m  Vítejte v obchodě\u001b[0m");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"  \u001b[32mPeníze hráče: {player.Money}\u001b[0m                        ");
                Console.WriteLine($"  \u001b[33mZkušenosti hráče: {player.Experience}\u001b[0m                    ");


                Console.WriteLine("+----------------------+--------+--------+");
                Console.WriteLine("| \u001b[33mPředměty v obchodě\u001b[0m   | \u001b[33mPočet\u001b[0m  | \u001b[33mCena\u001b[0m   |");
                Console.WriteLine("+----------------------+--------+--------+");

                foreach (var item in shopInventory)
                {
                    int itemCount = player.Inventory.Count(i => i.Name == item.Name);
                    Console.WriteLine($"| \u001b[33m{item.Name,-20}\u001b[0m | \u001b[33m{itemCount,6}\u001b[0m | \u001b[33m{item.Value,6}\u001b[0m |");
                }

                Console.WriteLine("+----------------------+--------+--------+\n");




                Console.WriteLine("+----------------------------------------+");
                Console.WriteLine("|  \u001b[33mVyberte možnost:\u001b[0m                      |");
                Console.WriteLine("+----------------------------------------+");
                Console.WriteLine("|  1. Nákup předmětů                     |");
                Console.WriteLine("+----------------------------------------+");
                Console.WriteLine("|  2. Prodej předmětů                    |");
                Console.WriteLine("+----------------------------------------+\n|                                        |");
                Console.WriteLine("|  \u001b[31m0. Opustit obchod\u001b[0m                     |");
                Console.WriteLine("+----------------------------------------+");



                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Zadejte platné číslo možnosti.");
                }

                if (choice == 0)
                {
                    Console.WriteLine("Opouštíte obchod.");
                    Console.Clear();
                    break;
                }

                switch (choice)
                {

                    case 1:
                        BuyItems(player);
                        break;
                    case 2:
                        SellItem(player);
                        break;
                    default:
                        break;
                }
            }
        }


        public static void DisplayInventorySectionWithNumbers<T>(string sectionTitle, List<T> items, Func<T, int, string> itemFormatter)
        {
            Console.WriteLine($"+-------------------------+-------------------+");
            Console.WriteLine($"| \u001b[33m{sectionTitle}\u001b[0m      | \u001b[32mCena\u001b[0m |\u001b[32m Zkušenosti \u001b[0m|");
            Console.WriteLine($"+-------------------------+-------------------+");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"| \u001b[33m{itemFormatter(items[i], i)} \u001b[0m|");
            }

            Console.WriteLine("+-------------------------+-------------------+\n|                                             |");
        }





        private static void ProcessPurchase(Mag player, Item selectedItem, int quantity)
        {
            // Logic for purchare process  
            int totalCost = selectedItem.Value * quantity;

            if (player.Money >= totalCost)
            {
                for (int i = 0; i < quantity; i++)
                {
                    BuyItem(player, selectedItem);
                }

                Console.Clear();
                Console.WriteLine($"Zakoupili jste: {quantity}x {selectedItem.Name}");
                Console.WriteLine($"Cena: {totalCost} zlaťáků");
                Console.WriteLine($"Zbývající peníze: {player.Money}");
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nemáte dostatek zlata.");
                Thread.Sleep(1500);
            }
        }

        private static void ProcessSale(Mag player, Item selectedItem)
        {
            if (selectedItem.IsSellable)
            {
                player.Money += selectedItem.Value;
                player.Inventory.Remove(selectedItem);
                Console.WriteLine($"Předmět '{selectedItem.Name}' byl prodán za {selectedItem.Value} zlata.");
                Thread.Sleep(1500);
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Předmět '{selectedItem.Name}' nelze prodat.");
                Thread.Sleep(1500);
                Console.Clear();
            }
        }



        private static void BuyItems(Mag player)
        {
            Console.Clear();

            DisplayInventorySectionWithNumbers("Předměty v obchodě", shopInventory, (item, index) =>
                $"{index + 1}. {item.Name,-20}\u001b[0m |\u001b[32m  {item.Value}\u001b[0m  |\u001b[32m    {item.Experience}      ");

            Console.WriteLine("|\u001b[31m 0. Odejít z nákupu\u001b[0m                          |");
            Console.WriteLine("-----------------------------------------------");

            int itemIndex;
            while (!int.TryParse(Console.ReadLine(), out itemIndex) || itemIndex < 0 || itemIndex > shopInventory.Count)
            {
                Console.WriteLine("Zadejte platné číslo předmětu.");
            }

            if (itemIndex == 0)
            {
                Console.WriteLine("Návrat do hlavní nabídky obchodu.");
                Thread.Sleep(1500);
                Console.Clear();
                return;
            }

            Item selectedItem = shopInventory[itemIndex - 1];

            Console.WriteLine("Zadejte počet kusů, které chcete zakoupit:");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Zadejte platné množství (více než 0).");
            }

            ProcessPurchase(player, selectedItem, quantity);
        }




        private static void SellItem(Mag player)
        {
            Console.Clear();

            while (true)
            {
                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("Váš inventář je prázdný. Nemáte co prodat.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return;
                }
                Console.WriteLine($"\u001b[32mPeníze hráče: {player.Money}\u001b[0m");

                Console.WriteLine("Vyberte předmět k prodeji:");

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {player.Inventory[i].Name} - Cena: {player.Inventory[i].Value} peněz");
                }

                Console.WriteLine($"0. \u001b[31mOdejít z prodeje\u001b[0m");

                int itemIndex;
                while (!int.TryParse(Console.ReadLine(), out itemIndex) || itemIndex < 0 || itemIndex > player.Inventory.Count)
                {
                    Console.WriteLine($"Zadejte platný index předmětu nebo číslo 0 pro odejít z prodeje.");
                }

                if (itemIndex == 0)
                {
                    Console.WriteLine("Návrat do hlavní nabídky obchodu.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
                }

                Item selectedItem = player.Inventory[itemIndex - 1];

                ProcessSale(player, selectedItem);

            }
        }







        private static void BuyItem(Mag player, Item item)
        {
            Console.Clear();

            if (player.Money >= item.Value)
            {

                int requiredExperience = 0;

                if (item is Sword)
                {
                    requiredExperience = 2;
                }

                else if (item is HealthPotion)
                {
                    requiredExperience = 2;
                }


                if (player.Experience >= requiredExperience)
                {
                    player.Money -= item.Value;

                    if (item is Sword sword)
                    {
                        BuySword(player, sword);
                    }
                    else if (item is HealthPotion healthPotion)
                    {
                        BuyHealthPotion(player, healthPotion);
                    }
                    else
                    {
                        player.Inventory.Add(item);
                    }

                    Console.WriteLine($"Zakoupili jste: \u001b[33m{item.Name}\u001b[0m");
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine($"Zbývající zlataky: \u001b[32m{player.Money}\u001b[0m\n");

                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Nemáte dostatek zkušeností na tento nákup.");
                }
            }
            else
            {
                Console.WriteLine("Nemáte dostatek peněz na tento nákup.");
            }

            Console.Clear();
        }




        private static void BuySword(Mag player, Sword swordItem)
        {
            Sword newSword = new Sword(swordItem.Name, swordItem.Value, swordItem.IsSellable, swordItem.CanDisassemble, swordItem.Experience);

            player.Inventory.Add(newSword);
        }


        private static void BuyHealthPotion(Mag player, HealthPotion healthPotionItem)
        {
            HealthPotion newHealthPotion = new HealthPotion(healthPotionItem.Name, healthPotionItem.Value, healthPotionItem.IsSellable, healthPotionItem.Experience);

            player.Inventory.Add(newHealthPotion);
        }



    }
}
