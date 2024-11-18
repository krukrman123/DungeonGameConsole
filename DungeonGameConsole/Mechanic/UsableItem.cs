using DungeonGameConsole.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Mechanic
{
    public class UsableItem : Item
    {

        public ItemType ItemType { get; }

        public UsableItem(string name, ItemType type, int value, bool sellable, bool isUsable)
            : base(name, type, value, sellable, isUsable)
        {
            ItemType = type;
        }

        public virtual void Use(Mag player, Fighter opponent)
        {
            Console.WriteLine($"Předmět {Name} nelze použít.");
        }
    }

    public class Sword : UsableItem
    {
        public Sword(string name, int value, bool isSellable, bool canDisassemble, int experience)
            : base(name, ItemType.Sword, value, isSellable, canDisassemble)
        {

            Experience = experience;
        }

        public override void Use(Mag player, Fighter opponent)
        {
            int swordDamage = opponent.health / 2;

            // Check if the player has the sword in the inventory
            if (player.Inventory.Contains(this))
            {
                Console.WriteLine($"Používám meč {this.Name} s útokem {swordDamage}.");

                // Calculate the actual damage considering the opponent's defense
                int actualDamage = Math.Max(0, swordDamage - opponent.defense);
                Console.WriteLine($"Skutečné poškození: {actualDamage}");

                // Deal damage to the opponent, ensuring health doesn't go below 0
                opponent.health = Math.Max(0, opponent.health - actualDamage);

                Console.WriteLine($"{player.name} použil meč a způsobil {actualDamage} poškození hráči {opponent.name} a stav {opponent.health}.");

                Thread.Sleep(4000);

                // Remove the sword from the player's inventory
                player.Inventory.Remove(this);


            }
        }
    }



    public class HealthPotion : UsableItem
    {
        public HealthPotion(string name, int value, bool isSellable, int experience)
            : base(name, ItemType.HealthPotion, value, isSellable, true)
        {
            Experience = experience;
        }

        public override void Use(Mag player, Fighter opponent)
        {
            int healingAmount = player.health / 2; // Adjust the healing amount as needed

            player.Health += healingAmount;

            // Ensure that health does not exceed the maximum value
            if (player.Health > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }

            // Display the healing message
            Console.WriteLine($"{player.name} použil léčivý lektvar a obnovil {healingAmount} životů.");
            Thread.Sleep(4000);

            // Remove the health potion from the player's inventory
            player.Inventory.Remove(this);
        }
    }

}
