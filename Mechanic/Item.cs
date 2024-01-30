using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonGame.Mechanic
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int Value { get; set; }
        public bool IsSellable { get; set; }
        public MaterialType Material { get; set; }
        public int Experience {get; set;}

        public bool CanDisassemble { get; set; }

        public Item(string name, ItemType type, int value, bool canDisassemble, MaterialType material, bool isSellable)
        {
            Name = name;
            Type = type;
            Value = value;
            IsSellable = isSellable;
            Material = material;
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




    




    public enum MaterialType
    {
        Iron,
        Leather,
        Wood,
        Crystal,
        Steel,
        Glass
    }



}
