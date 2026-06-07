using Open_0._0._0.Actors;

namespace Open_0._0._0.Items.Food
{
    internal class Apple:Item
    {
        public int healamount = 2;

        public Apple(): base(1, "Apfel", "Heilt 2 HP",ItemType.Food)
        { }
        public override void Use(Player player)
        {
            player.Health_Component.ApplyHeal(healamount); 
            Console.WriteLine("Aplle");

        }
    }
}
