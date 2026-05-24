using SFML.System;

namespace Open_0._0._0.Components
{
     class Position_Component
    {
        public Vector2f PixelPosition { get; set; }
        public Vector2i GridPosition { get; set; }
        private int _size;

        public Position_Component(int size = 28)  // Default auf PLAYER_SIZE
        {
            _size = size;
        }

        public Vector2f ToPixelPosition(int gridX, int gridY)
        {
            return new Vector2f(gridX * _size, gridY * _size);
        }

        public Vector2i ToGridPosition()
        {
            return new Vector2i((int)PixelPosition.X / _size, (int)PixelPosition.Y / _size);
        }

        public void SetPosition(Vector2f position)
        {
            PixelPosition = position;
            GridPosition = ToGridPosition();
        }

        public void Move(Vector2f movement)
        {
            PixelPosition += movement;
            GridPosition = ToGridPosition();
        }
    }
}