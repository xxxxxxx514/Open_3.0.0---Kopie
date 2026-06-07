using Open_0._0._0.Actors;
namespace Open_0._0._0.Items.EquipmentItems
{
    class Helm : Item
    {
        public int Armor = 3;

        public Helm() : base(1, "Helm", "Ein einfacher Helm")
        {
            Type = ItemType.Armor;
            EquipmentType = EquipmentType.Helm;
        }

        public override void Use(Player player)
        {
            player.Equip(this);
            Console.WriteLine("Helm");

        }
    }

}
