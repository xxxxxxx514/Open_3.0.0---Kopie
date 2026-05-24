using Open_0._0._0.Actors;

namespace Open_0._0._0.Actions
{
    interface IAction
    {
        public void Execute(Actor actor, float deltaTime, Map map);

        public int GetPriority(Actor actor, Desire desire);
    }
}
