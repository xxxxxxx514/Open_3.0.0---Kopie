using Open_0._0._0.Actors;
namespace Open_0._0._0.Items.EquipmentItems
{
     class Armguard : Item
    {
        public int Armor = 2;

        public Armguard() : base(3, "Armguard", "Schützt die Arme")
        {
            Type = ItemType.Armor;
            EquipmentType = EquipmentType.Armguard;
        }

        public override void Use(Player player)
        {
            player.Equip(this);
            Console.WriteLine("Arm");
        }
    }

}
