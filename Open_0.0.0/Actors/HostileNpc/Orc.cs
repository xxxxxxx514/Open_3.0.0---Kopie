using Open_0._0._0.Enums;
using Open_0._0._0.Utils;
using SFML.Graphics;
namespace Open_0._0._0.Actors.HostileNpc
{
    class Orc:Enemy
    {
        public Orc(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Oark";
            Type = MonsterType.Oark;
            SetToken(Type);

        }
        public Orc() : base()
        {
            Name = "Oark";
            Type = MonsterType.Oark;

            Body_Componente.Texture = new Texture("Slime.png");
            Body_Componente.Sprite.Texture = Body_Componente.Texture;
            SetToken(Type);

        }
    }
}
