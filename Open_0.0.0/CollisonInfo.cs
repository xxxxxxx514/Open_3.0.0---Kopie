using Open_0._0._0.World;

namespace Open_0._0._0
{
    internal class CollisionInfo
    {
        public bool HasCollision { get; private set; }
        public Tile? CollidedTile { get; private set; }

        public CollisionInfo(bool hasCollision, Tile? tile = null)
        {
            HasCollision = hasCollision;
            CollidedTile = tile;
        }

        public override string ToString()
        {
            if (CollidedTile != null)
                return $"Collision: {HasCollision}, with Tile: {CollidedTile.Tiletype} at {CollidedTile.GridPosition}";
            return $"Collision: {HasCollision}";
        }
    }
}