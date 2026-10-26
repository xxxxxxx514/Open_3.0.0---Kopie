using Open_0._0._0.Actions;
using Open_0._0._0.Actors;

namespace Open_0._0._0.Items.Drinks
{
    internal class Juice : Item, IDrinkable
    {
        public int HealAmount = 2;
        public int DrinkAmount { get; set; } = 5;
        public Juice() : base(1, "Apfel", "Heilt 2 HP", ItemType.Food)
        { }
        public override void Use(Actor actor)
        {
            Drink(actor); // Use ruft Eat auf — nicht doppelt schreiben
        }

        public void Drink(Actor actor)
        {
            actor.Health_Component.ApplyHeal(HealAmount);
            actor.DesireManager.Desire.RestoreNeeds(thirstAmount: DrinkAmount);
            Console.WriteLine($"{actor.Name} hat einen Juice getrunkekn.");
        }
    }
}
