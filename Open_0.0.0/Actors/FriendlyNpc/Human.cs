using Open_0._0._0.Components;
using Open_0._0._0.Utils;

namespace Open_0._0._0.Actors.FriendlyNpc
{
     class Human : NPC
    {
        public Human(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Human";
        }
        public Human() : base()
        {
            Name = "Human";

            Body_Componente.Sprite.Color = CustomColors.BurlyWood;
        }

        public void UpdatePositionHuman()
        {
            Body_Componente.Sprite.Position = Position_Component.PixelPosition;
            Position_Component.GridPosition = Position_Component.ToGridPosition();
        }
       
    }
}