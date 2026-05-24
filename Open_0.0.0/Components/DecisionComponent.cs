using Open_0._0._0.Actions;
using Open_0._0._0.Actors;

namespace Open_0._0._0.Components
{
    class DecisionComponent
    {
        public IAction Decide(Actor character)
        {
            DesireState state = character.DesireManager.Desire.State;
            DesireManager desireManager = character.DesireManager;

            return desireManager.Actions.OrderBy(a => a.GetPriority(character, desireManager.Desire)).First();
        }
    }
}
