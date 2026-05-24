using Open_0._0._0.Utils;
using SFML.Graphics;

namespace Open_0._0._0.Actors
{
     class Oark:Enemy
    {
        public Oark(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Oark";
            monsterType = MonsterType.Oark;
            Body_Componente.ChangeBodyColor(CustomColors.Ork);
        }
        public Oark() : base()
        {
            Name = "Oark";
            monsterType = MonsterType.Oark;
            Body_Componente.ChangeBodyColor(CustomColors.Ork);

            Body_Componente.Texture = new Texture("Slime.png");
            Body_Componente.Sprite.Texture = Body_Componente.Texture;
        }
    }
}
