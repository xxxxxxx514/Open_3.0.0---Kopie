using Open_0._0._0.Actors;
using Open_0._0._0.Relationships.Desires;
namespace Open_0._0._0.Actions
{
    internal class IdleAction:IAction
    {
        public void Execute(Actor actor, float deltaTime)
        {
            return;
        }
        public int GetPriority(Actor actor, Desire desire)
        {
            return 100 - 40;
        }
    }
}
