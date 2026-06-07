using Open_0._0._0.Actors;
namespace Open_0._0._0.Items.EquipmentItems
{
    internal class Sword:Item
    {
        public int Damage {  get; set; }
        public Sword():base(1, "Sword","Ein Schwert",ItemType.Weapon,EquipmentType.Righthand) {}
        public override void Use(Player player)
        {
            player.Equip(this);
            Console.WriteLine("Sword");
        }
    }
}
