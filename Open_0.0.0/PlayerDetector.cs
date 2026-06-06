using Open_0._0._0.Actors;
using SFML.System;

namespace Open_0._0._0
{
    internal class PlayerDetector
    {   
        public Actor Owner;
        public PlayerDetector(Actor character)
        {
            Owner = character;
        }


        public bool IsInRange(Vector2f targetPos, float range=60f)
        {
            float dx = targetPos.X - Owner.Position_Component.PixelPosition.X;
            float dy = targetPos.Y - Owner.Position_Component.PixelPosition.Y;

            return dx * dx + dy * dy <= range * range;
        }
    }
}
