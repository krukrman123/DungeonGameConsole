using DungeonGameConsole.Interface;
using DungeonGameConsole.Mechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DungeonGameConsole.Role
{
    public class Mag : Fighter, IUpgradeable
    {
        public int mana;
        public int maxMana;
        public int magickAttack;
        public int money;
        public int experience;


        public int Defense { get; set; }
        public int Attacks { get; set; }
        public List<Item> Inventory { get; private set; }





        public Mag(string name, int health, int attack, int defense, Cube cube, int mana, int magickAttack, int money, int experience)
    : base(name, health, attack, defense, cube)
        {
            this.mana = mana;
            this.maxMana = mana;
            this.magickAttack = magickAttack;
            this.money = money;
            this.experience = experience;
            Inventory = new List<Item>();
        }






        public int Money
        {
            get { return money; }
            set { money = value; }
        }


        public void ReceiveDamage(int damage)
        {
            Health -= damage;

            // We will verify that the player's health has not dropped below 0
            if (Health < 0)
            {
                Health = 0; // Verify that health is not negative
                Console.WriteLine("Jsi poražen! Hra skončila.");

            }
        }


        public int Health
        {
            get { return base.health; }
            set
            {
                base.health = value;
                if (base.health > base.maxHealth)
                {
                    base.health = base.maxHealth;
                }
            }
        }

        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }


        public int MaxHealth
        {
            get { return base.maxHealth; }
            set { base.maxHealth = value; }
        }



        public void ApplyUpgrade()
        {
            Console.WriteLine("Vylepsil jste si zbroj!");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public void RestartHealth()
        {
            Health = MaxHealth;
        }


        public void BuyMissingHealth()
        {
            int missingHealth = MaxHealth - Health;
            int cost = 8;
            int maxCost = 25;

            if (health > 500 && Money >= maxCost)
            {
                Health = MaxHealth;
                Money -= maxCost;
                Console.WriteLine($"Zdraví bylo doplněno na maximum za \u001b[32m({maxCost})\u001b[0m zlatých.");
            }
            else if (missingHealth > 0 && Money >= cost)
            {
                Health = MaxHealth;
                Money -= cost;
                Console.WriteLine($"Zdraví bylo doplněno na maximum za \u001b[32m({cost})\u001b[0m zlatých.");
            }
            else if (missingHealth > 0)
            {
                Console.WriteLine("Nemáte dost peněz na kompletní doplnění zdraví.");
            }
            else
            {
                Console.WriteLine("Zdraví je již na maximu, není potřeba doplňovat.");
            }
        }



        public void BuyArmor()
        {
            if (Money >= 10)
            {
                MaxHealth += 10;
                Money -= 10;


                // Check if the player was already recovery  health
                if (health > 80 % maxHealth)
                {
                    RestartHealth();

                }
            }
            else
            {
                Console.WriteLine("Nemáte dost peněz na zvýšení zbroje.");
            }
        }







        public string GraphicMana()
        {
            return GraphicIndicator(mana, maxMana);
        }

        public override void Combat(Fighter enemy)
        {
            if (mana < maxMana)
            {
                mana += 10;
                if (mana > maxMana)
                    mana = maxMana;
                base.Combat(enemy);
            }
            else // Magic attack
            {
                int upgradedMagickAttack = magickAttack + 5; // Every upgrade magick attack is up 5
                int strike = upgradedMagickAttack + cube.ThrowIt();
                SetMessage(String.Format("{0} použil magii za {1} hp", name, strike));
                enemy.Defensive(strike);
                mana = 0;
            }
        }



        public void AddRandomItemToInventory()
        {
            Item randomItem = GenerateRandomItem();
            Inventory.Add(randomItem);
            Console.WriteLine($"Získal jste předmět: {randomItem.Name} (Hodnota: {randomItem.Value})");
        }



        private Item GenerateRandomItem()
        {
            Random random = new Random();
            ItemType randomType = (ItemType)random.Next(Enum.GetValues(typeof(ItemType)).Length);

            Dictionary<ItemType, string> itemNames = new Dictionary<ItemType, string>
    {
        { ItemType.ManaElixir, "Mana lektvar" },
        { ItemType.MagicScroll, "Magický svitek" },
        { ItemType.EnchantedAmulet, "Kouzelný amulet" },
        { ItemType.Shield, "Štít" },
        { ItemType.Ring, "Prsten" },
        { ItemType.Robe, "Plášť" },
        { ItemType.Book, "Kniha kouzel" },
        { ItemType.Dagger, "Dýka temnoty" },
        { ItemType.Helmet, "Kouzelná přilba" },
        { ItemType.AmuletOfWisdom, "Amulet moudrosti" },
        { ItemType.Gauntlets, "Rukavice odolnosti" },
        { ItemType.AncientScroll, "Starodávný svitek" },
        { ItemType.Spellbook, "Kniha zaklínadel" },
        { ItemType.InvisibilityCloak, "Plášť neviditelnosti" },
        { ItemType.PoisonedArrow, "Otrávená šípka" },
        { ItemType.LuckyCharm, "Štěstíčko" }



    };

            // Definition range of price for every item
            Dictionary<ItemType, (int min, int max)> itemPrices = new Dictionary<ItemType, (int min, int max)>
    {

    { ItemType.ManaElixir, (15, 25) },
    { ItemType.MagicScroll, (30, 50) },
    { ItemType.EnchantedAmulet, (40, 60) },
    { ItemType.Shield, (40, 70) },
    { ItemType.Ring, (25, 40) },
    { ItemType.Robe, (35, 55) },
    { ItemType.Book, (20, 30) },
    { ItemType.Dagger, (30, 35) },
    { ItemType.Helmet, (28, 38) },
    { ItemType.AmuletOfWisdom, (32, 40) },
    { ItemType.Gauntlets, (25, 37) },
    { ItemType.AncientScroll, (30, 40) },
    { ItemType.Spellbook, (28, 36) },
    { ItemType.InvisibilityCloak, (35, 40) },
    { ItemType.PoisonedArrow, (26, 39) },
    { ItemType.LuckyCharm, (30, 35) }


    };

            int randomPrice = random.Next(itemPrices[randomType].min, itemPrices[randomType].max + 1);

            // Created item with name, price and new argument
            Item item = new Item(
                name: itemNames[randomType],
                type: randomType,
                value: randomPrice,
                canDisassemble: true,
                isSellable: true
            );

            return item;
        }


    }
}
