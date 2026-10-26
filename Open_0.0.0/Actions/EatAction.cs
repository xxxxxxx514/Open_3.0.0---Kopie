using Open_0._0._0.Actors;
using Open_0._0._0.Relationships.Desires;

namespace Open_0._0._0.Actions
{
    class EatAction : IAction
    {
        public void Execute(Actor actor, float deltaTime)
        {
            if(!SearchInventoryForDrink(actor))
            {
                Console.WriteLine($"{actor.Name} hat nichts zu essen.");
                return;
            }
            Console.WriteLine($"{actor.Name} hat gegessen.");

            actor.DesireManager.RemoveAction<EatAction>();
        }
        public int GetPriority(Actor actor, Desire desire)
        {
            return 100 - desire.Hunger;
        }
        private bool SearchInventoryForDrink(Actor character)
        {
            foreach (var slot in character.Inventory.Slots)
            {
                Console.WriteLine( slot.ToString());
                if (slot.Item is IEatable eatitem)
                {
                    // Found a drinkable item, perform the drinking action
                    eatitem.Eat(character);
                    character.Consume(slot.Item);
                    return true;
                }
            }
            return false; // No drinkable item found in the inventory
        }
    }
}
