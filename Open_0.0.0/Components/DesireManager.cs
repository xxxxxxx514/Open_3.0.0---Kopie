using Open_0._0._0.Actions;
using Open_0._0._0.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Components
{
    internal class DesireManager:ITickable
    {
        private float _timer = 2;
        public Desire Desire { get; set; }
        public List<IAction> Actions { get; set; }

        public DesireManager() 
        {
            Desire = new Desire();
            Actions = new List<IAction>
            {
                new IdleAction()
            };
        }
        public void Tick(float dt)
        {
            if (_timer > 0)
                _timer -= dt;
        }
        public void AddAction(IAction action)
        {
            if (Actions.Any(a => a.GetType() == action.GetType()))//prüft ob es doppelte einträge gibt
                return;
            Actions.Add(action); 
        }
        public void RemoveAction<T>()where T : IAction
        {
            Actions.RemoveAll(a => a is T);
        }
    }
}
