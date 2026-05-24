using Open_0._0._0.Utils;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Actors
{
     class Goblin : Enemy
    {
        public Goblin(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Goblin";
            monsterType = MonsterType.Goblin;
            Body_Componente.ChangeBodyColor(CustomColors.Goblin);
        }
        public Goblin() : base()
        {
            Name = "Goblin";
            monsterType = MonsterType.Goblin;
            Body_Componente.ChangeBodyColor(CustomColors.Goblin);

            Body_Componente.Texture = new Texture("Goblin.png");
            Body_Componente.Sprite.Texture = Body_Componente.Texture;
        }
    }
}
