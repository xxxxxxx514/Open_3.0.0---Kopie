using Open_0._0._0.Actors;
using Open_0._0._0.Relationships.Desires;

namespace Open_0._0._0.Actions
{
    interface IAction
    {
        public void Execute(Actor actor, float deltaTime);

        public int GetPriority(Actor actor, Desire desire);
    }
}
