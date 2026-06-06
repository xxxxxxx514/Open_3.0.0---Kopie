using Open_0._0._0.Actions;
using Open_0._0._0.Actors;

namespace Open_0._0._0.Components
{
    class DecisionComponent:ITickable
    {
        private float _timer = 0;
        public void Tick(float dt)
        {
            if (_timer > 0)
                _timer -= dt;
        }
        public IAction Decide(Actor character)
        {
            if (character.DesireManager.Actions.Count <= 0) return new IdleAction();
            DesireState state = character.DesireManager.Desire.State;
            DesireManager desireManager = character.DesireManager;

            return desireManager.Actions.OrderBy(a => a.GetPriority(character, desireManager.Desire)).First();
        }
    }
}
