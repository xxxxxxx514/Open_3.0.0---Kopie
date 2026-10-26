using Open_0._0._0.Actors;
using Open_0._0._0.Items;
using Open_0._0._0.Relationships.Desires;

namespace Open_0._0._0.Actions
{
    class DrinkAction : IAction
    {
        public void Execute(Actor actor, float deltaTime)
        {
            if(!SearchInventoryForDrink(actor))
            {
                Console.WriteLine($"{actor.Name} hat nichts zu trinken.");
                return;
            }
            Console.WriteLine($"{actor.Name} hat getrunken.");

            actor.DesireManager.RemoveAction<DrinkAction>();
        }
        public int GetPriority(Actor actor, Desire desire)
        {
            return 100 - desire.Thirst;
        }
        private bool SearchInventoryForDrink(Actor character)
        {
            foreach (var slot in character.Inventory.Slots)
            {
                if (slot.Item is IDrinkable drinkitem)
                {
                    // Found a drinkable item, perform the drinking action
                    drinkitem.Drink(character);
                    character.Consume(slot.Item);

                    return true;
                }
            }
            return false; // No drinkable item found in the inventory
        }
    }
}
