using Open_0._0._0.Utils;
using SFML.Graphics;

namespace Open_0._0._0.Actors
{
     class Slime : Enemy
    {
        public Slime(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Slime";
            monsterType = MonsterType.Slime;
            Body_Componente.ChangeBodyColor(CustomColors.Slime);
        }
        public Slime() : base()
        {
            Name = "Slime";
            monsterType = MonsterType.Slime;
            Body_Componente.ChangeBodyColor(CustomColors.Slime);

            Body_Componente.Texture = new Texture("Slime.png");
            Body_Componente.Sprite.Texture =Body_Componente.Texture;
        }
    }
}