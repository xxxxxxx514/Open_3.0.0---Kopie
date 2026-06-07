using Open_0._0._0.Actors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Items.Potions
{
     class HealthPotion:Item
     {
        public int HealAmount = 50;
        public HealthPotion() : base(1, "Health Potion", "Heilt 25 HP")
        {
        }

        public override void Use(Player player)
        {
            player.Health_Component.ApplyHeal(HealAmount);
        }
    }
}
