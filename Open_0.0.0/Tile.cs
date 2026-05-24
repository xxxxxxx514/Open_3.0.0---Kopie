using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    internal class Tile : GameObject, ICollidable
    {
        public RectangleShape Shape = new RectangleShape();
        public const int TILE_SIZE = 32;
        public Vector2f PixelPosition;
        public Vector2i GridPosition;
        public TileType Tiletype;
        public bool isWalkable;

        public Tile() { }
        public Tile(TileType type)
        {
            Shape = new RectangleShape();
            Shape.Size = new Vector2f(TILE_SIZE, TILE_SIZE);
            Shape.FillColor = GetTileColorByType(type);

            Tiletype = type;

            PixelPosition = Shape.Position;
            GridPosition = ToGridPosition();
        }
        public Tile(int gridX, int gridY, TileType type)
        {
            Shape = new RectangleShape();
            Shape.Size = new Vector2f(TILE_SIZE, TILE_SIZE);
            Shape.Position = ToPixelPosition(gridX, gridY);
            Shape.FillColor = GetTileColorByType(type);

            Tiletype = type;

            PixelPosition = Shape.Position;
            GridPosition = ToGridPosition();
        }
        public static Tile CreateTileForDesign(int gridX, int gridY)
        {
            var tile = new Tile();  // NEUE Instanz!
            tile.Shape = new RectangleShape();
            tile.Shape.Size = new Vector2f(TILE_SIZE, TILE_SIZE);
            tile.Shape.Position = tile.ToPixelPosition(gridX, gridY);
            tile.Shape.OutlineThickness = 2;
            tile.Shape.OutlineColor = Color.Blue;

            tile.Tiletype = TileType.mapdesigndefault;
            tile.PixelPosition = tile.Shape.Position;
            tile.GridPosition = tile.ToGridPosition();
            tile.isWalkable = true;
            return tile;
        }
        public void ChangeTilePropertys(TileType type)
        {
            Shape.FillColor = GetTileColorByType(type);

            Tiletype = type;
        }
        public Vector2i ToGridPosition()
        {
            return new Vector2i((int)PixelPosition.X / TILE_SIZE, (int)PixelPosition.Y / TILE_SIZE);
        }
        public Vector2i ToGridPosition(Vector2f vector2F)
        {
            return new Vector2i((int)vector2F.X / TILE_SIZE, (int)vector2F.Y / TILE_SIZE);
        }
        public Vector2i ToGridPosition(Vector2i vector2i)
        {
            return new Vector2i((int)vector2i.X / TILE_SIZE, (int)vector2i.Y / TILE_SIZE);
        }
        public Vector2f ToPixelPosition(int gridX, int gridY)
        {
            return new Vector2f(gridX * TILE_SIZE, gridY * TILE_SIZE);
        }
        public Color GetTileColorByType(TileType type)
        {
            switch (type)
            {
                case TileType.Grass: isWalkable = true; return CustomColors.ForestGreen;
                case TileType.Water: isWalkable = false; return Color.Blue;
                case TileType.Sand: isWalkable = true; return Color.Yellow;
                case TileType.Black: isWalkable = true; return Color.Black;
                case TileType.Wall: isWalkable = false; return CustomColors.FireBrick;
                case TileType.Fence: isWalkable = false; return CustomColors.WoodBrown;
                case TileType.Road: isWalkable = true; return CustomColors.LightSalmon;
                case TileType.Test1: isWalkable = true; return CustomColors.DimGray;
                case TileType.Test2: isWalkable = true; return CustomColors.SlateGray;
                default: isWalkable = true; return Color.White;
            }
        }
        public override string ToString()
        {
            return $"Tile[Grid:({GridPosition.X},{GridPosition.Y}) Pixel:({PixelPosition.X},{PixelPosition.Y}) Type:{Tiletype} Walkable:{isWalkable} Size:{TILE_SIZE}]";
        }
    }
}