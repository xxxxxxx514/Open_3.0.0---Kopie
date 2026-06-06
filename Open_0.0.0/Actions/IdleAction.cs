using Open_0._0._0.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Actions
{
    internal class IdleAction:IAction
    {
        public void Execute(Actor actor, float deltaTime, Map map)
        {
            return;
        }
        public int GetPriority(Actor actor, Desire desire)
        {
            return 100 - 40;
        }
    }
}
