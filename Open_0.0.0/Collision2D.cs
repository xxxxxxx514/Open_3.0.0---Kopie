using Open_0._0._0.Core;
using Open_0._0._0.World;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    internal class Collision2D
    {
        public bool Collision(FloatRect bound1, FloatRect bound2)
        {
            return bound1.Intersects(bound2);
           
        }

        public CollisionInfo CollisionMap(Vector2f targetPosition, Vector2f movement, float entitySize)
        {
            if (movement.X == 0 && movement.Y == 0)
                return new CollisionInfo(false);

            FloatRect entityRect = new FloatRect(
                targetPosition.X + movement.X,
                targetPosition.Y + movement.Y,
                entitySize,
                entitySize
            );

            var world = GameWorld.World;  // ← eine Variable, überall gleich

            Vector2i topLeft = world.WorldToTile(new Vector2f(entityRect.Left, entityRect.Top));
            Vector2i bottomRight = world.WorldToTile(new Vector2f(
                entityRect.Left + entityRect.Width - 0.1f,
                entityRect.Top + entityRect.Height - 0.1f
            ));

            for (int x = topLeft.X; x <= bottomRight.X; x++)
            {
                for (int y = topLeft.Y; y <= bottomRight.Y; y++)
                {
                    if (!world.IsTileAtWalkable(x, y))
                    {
                        return new CollisionInfo(true, world.GetTileAt(x, y));
                    }
                }
            }

            return new CollisionInfo(false);
        }

        public bool CollisionEntityVsEntity(FloatRect entity1, FloatRect entity2)
        {
            return entity1.Intersects(entity2);
        }

        public bool CollisionPlayerEnemy(Vector2f playerPos, Vector2f enemyPos, Vector2f movement, float playerSize, float enemySize)
        {
            FloatRect playerRect = new FloatRect(
                playerPos.X + movement.X,
                playerPos.Y + movement.Y,
                playerSize,
                playerSize
            );
            FloatRect enemyRect = new FloatRect(enemyPos.X, enemyPos.Y, enemySize, enemySize);
            return Collision(playerRect, enemyRect);
        }

        public bool CollisionPlayerItem(Vector2f playerPos, Vector2f itemPos, Vector2f movement, float playerSize, float itemSize)
        {
            FloatRect playerRect = new FloatRect(
                playerPos.X + movement.X,
                playerPos.Y + movement.Y,
                playerSize,
                playerSize
            );
            FloatRect itemRect = new FloatRect(itemPos.X, itemPos.Y, itemSize, itemSize);
            return Collision(playerRect, itemRect);
        }
    }
}