using Open_0._0._0.Actions;
using Open_0._0._0.Actors;

namespace Open_0._0._0.Items.Food
{
    internal class Apple:Item, IEatable
    {
        public int HealAmount = 2;
        public int EatAmount { get; set; } = 5;
        public Apple(): base(1, "Apfel", "Heilt 2 HP",ItemType.Food)
        { }
        public override void Use(Actor actor)
        {
            Eat(actor); // Use ruft Eat auf — nicht doppelt schreiben
        }

        public void Eat(Actor actor)
        {
            actor.Health_Component.ApplyHeal(HealAmount);
            actor.DesireManager.Desire.RestoreNeeds(hungerAmount: EatAmount);
            Console.WriteLine($"{actor.Name} hat einen Apfel gegessen.");
        }
    }
}
