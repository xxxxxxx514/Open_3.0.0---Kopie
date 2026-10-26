using Open_0._0._0.Core;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
using System;

namespace Open_0._0._0.Components
{
     class Body_Component:ITickable
    {
        public Sprite Sprite;
        public Texture Texture;

        public Position_Component Position_Component { get; set; }
        public int _size;
        private float _timer = 0;
        public void Tick(float dt)
        {
            if (_timer > 0)
                _timer -= dt;
        }
        public Body_Component(Position_Component position_Component)
        {
            Position_Component = position_Component;
            Texture = new Texture("Slime.png");
            Sprite = new Sprite(Texture);

            Initialise();

        }
        public void SetSpriteColor(Color color)
        {
            var sprite = Sprite;
            sprite.Color = color;
            Sprite = sprite;
        }
        public void SetTexture(string path, int size)
        {
            Texture = new Texture(path);
            Sprite = new Sprite(Texture);
            Sprite.Scale = new Vector2f(
                (float)size / Texture.Size.X,
                (float)size / Texture.Size.Y
            );
            Sprite.Position = Position_Component.PixelPosition;
        }
        public void Initialise(int size = 28)
        {
            _size = size;

            Sprite.Position = Position_Component.PixelPosition;
        }
        public void SetBodyPostion(Vector2f pixelPosition)
        {
            Position_Component.PixelPosition = pixelPosition;
        }
        public void SetBodyPostion(Vector2i tilePosition)
        {
            Position_Component.PixelPosition = GameWorld.World.TileToWorld(tilePosition);
;
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