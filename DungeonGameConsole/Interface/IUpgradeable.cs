using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGameConsole.Interface
{
    public interface IUpgradeable
    {
        int MaxHealth { get; set; }
        int Defense { get; set; }
        int Attacks { get; set; }
        int Experience { get; set; }
        int Money { get; set; }
        int Health { get; set; }

        string GraphicHealth();
        string GraphicMana();

        void ApplyUpgrade();
    }
}
