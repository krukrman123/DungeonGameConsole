using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Mechanic
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int Value { get; set; }
        public bool IsSellable { get; set; }
        public int Experience { get; set; }

        public bool CanDisassemble { get; set; }

        public Item(string name, ItemType type, int value, bool canDisassemble, bool isSellable)
        {
            Name = name;
            Type = type;
            Value = value;
            IsSellable = isSellable;
            CanDisassemble = canDisassemble;


        }


    }

    public enum ItemType
    {
        HealthPotion,
        ManaElixir,
        MagicScroll,
        EnchantedAmulet,
        Sword,
        Shield,
        Ring,
        Robe,
        Book,
        Dagger,
        Helmet,
        AmuletOfWisdom,
        Gauntlets,
        AncientScroll,
        Spellbook,
        InvisibilityCloak,
        PoisonedArrow,
        LuckyCharm

    }
}
