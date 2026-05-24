using Open_0._0._0.Actors;

namespace Open_0._0._0.Actions
{
    class DrinkAction : IAction
    {
        public void Execute(Actor actor, float deltaTime, Map map)
        {
            actor.DesireManager.Desire.RestoreNeeds(0,30);//später mit tränke items
            actor.DesireManager.RemoveAction<DrinkAction>();
        }
        public int GetPriority(Actor actor, Desire desire)
        {
            return 100 - desire.Hunger;
        }

    }
}
