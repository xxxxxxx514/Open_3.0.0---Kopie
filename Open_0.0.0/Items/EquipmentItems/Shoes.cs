using Open_0._0._0.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Items.EquipmentItems
{
    class Shoes : Item
    {
        public int Armor = 1;

        public Shoes() : base(5, "Shoes", "Einfache Schuhe")
        {
            Type = ItemType.Armor;
            EquipmentType = EquipmentType.Shoes;
        }

        public override void Use(Actor charackter)
        {
            charackter.Equip(this);
            Console.WriteLine("Shoe");
        }
    }

}
