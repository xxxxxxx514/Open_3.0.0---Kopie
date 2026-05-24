using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
using System;

namespace Open_0._0._0.Components
{
     class Body_Componente
    {
        public Sprite Sprite;
        public Texture Texture;
        public RectangleShape Shape { get; set; }
        public Position_Component Position_Component { get; set; }
        public int _size;

        public Body_Componente(Position_Component position_Component)
        {
            Shape = new RectangleShape();
            Position_Component = position_Component;
            Sprite = new Sprite();
            Initialise();

        }
        public void Initialise(int size = 28)
        {
            _size = size;

            Shape.Size = new Vector2f(size, size);
            Shape.Position = Position_Component.PixelPosition;
            Shape.FillColor = CustomColors.Basic_Body;

            Sprite.Position = Position_Component.PixelPosition;
        }
        public void SetBodyPostion(Vector2f position)
        {
            Position_Component.PixelPosition = position;
        }
        public void ChangeBodyColor(Color color)
        {
            Shape.FillColor = color;
        }
        public Vector2f ToPixelPosition(int gridX, int gridY)
        {
            return new Vector2f(gridX * _size, gridY * _size);
        }
        public Vector2i ToGridPosition()
        {
            return new Vector2i((int)Position_Component.PixelPosition.X / _size, (int)Position_Component.PixelPosition.Y / _size);
        }
    }
}