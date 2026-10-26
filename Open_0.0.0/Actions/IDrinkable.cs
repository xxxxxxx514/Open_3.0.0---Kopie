using Open_0._0._0.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Actions
{
     interface IDrinkable
    {
        int DrinkAmount { get; set; }
        void Drink(Actor actor);
    }
}
