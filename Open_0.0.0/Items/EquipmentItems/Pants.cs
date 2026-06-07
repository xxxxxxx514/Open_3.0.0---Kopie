using Open_0._0._0.Actors;
namespace Open_0._0._0.Items.EquipmentItems
{
     class Pants : Item
    {
        public int Armor = 4;

        public Pants() : base(4, "Pants", "Gibt Schutz für die Beine")
        {
            Type = ItemType.Armor;
            EquipmentType = EquipmentType.Pants;
        }

        public override void Use(Player player)
        {
            player.Equip(this);
            Console.WriteLine("Pants");

        }
    }

}
