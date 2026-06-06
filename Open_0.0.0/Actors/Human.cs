using Open_0._0._0.Components;
using Open_0._0._0.Utils;
using SFML.System;

namespace Open_0._0._0.Actors
{
     class Human : NPC
    {
        public Human(int gridX, int gridY) : base(gridX, gridY)
        {
            Name = "Human";
            Body_Componente.ChangeBodyColor(CustomColors.BurlyWood);
        }
        public Human() : base()
        {
            Name = "Human";
            Body_Componente.ChangeBodyColor(CustomColors.BurlyWood);

            Body_Componente.Sprite.Color = CustomColors.BurlyWood;
        }

        public void UpdatePositionHuman()
        {
            Body_Componente.Shape.Position = Position_Component.PixelPosition;
            Body_Componente.Sprite.Position = Position_Component.PixelPosition;
            Position_Component.GridPosition = Position_Component.ToGridPosition();
            Position_Component.PixelPosition = Body_Componente.Shape.Position;
        }
       
    }
}