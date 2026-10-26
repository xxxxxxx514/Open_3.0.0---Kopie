using Open_0._0._0.Enums;
using Open_0._0._0.Utils;
using SFML.Graphics;
namespace Open_0._0._0.Actors.HostileNpc
{
     class Goblin : Enemy
    {
        public Goblin(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Goblin";
            Type = MonsterType.Goblin;

            Body_Componente.Texture = new Texture("Goblin.png");
            Body_Componente.Sprite.Texture = Body_Componente.Texture;
            SetToken(Type);
        }
        public Goblin() : base()
        {
            Name = "Goblin";
            Type = MonsterType.Goblin;

            Body_Componente.Texture = new Texture("Goblin.png");
            Body_Componente.Sprite.Texture = Body_Componente.Texture;
            SetToken(Type);

        }
    }
}
