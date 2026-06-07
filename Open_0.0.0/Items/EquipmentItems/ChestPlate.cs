using Open_0._0._0.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Items.EquipmentItems
{
     class Chestplate : Item
    {
        public int Armor = 6;

        public Chestplate() : base(2, "Chestplate", "Eine Brustplatte aus Metall")
        {
            Type = ItemType.Armor;
            EquipmentType = EquipmentType.Chestplate;
        }

        public override void Use(Player player)
        {
            player.Equip(this);
            Console.WriteLine("Chest");

        }
    }

}
