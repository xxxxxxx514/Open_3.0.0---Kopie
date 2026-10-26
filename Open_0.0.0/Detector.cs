using Open_0._0._0.Actors;
using Open_0._0._0.Core;
using Open_0._0._0.Utils;
using SFML.System;

namespace Open_0._0._0
{
    internal class Detector
    {
        public Actor Owner;
        public Detector(Actor character)
        {
            Owner = character;
        }


        public bool IsInRange(Vector2f targetPos, float range = 60f)
        {
            float dx = targetPos.X - Owner.Position_Component.PixelPosition.X;
            float dy = targetPos.Y - Owner.Position_Component.PixelPosition.Y;

            return dx * dx + dy * dy <= range * range;
        }

        public Actor GetActorsInRange(float range)
        {
            List<Actor> allenteties = GameWorld.Entities;
            float distanz = 0;
            for (int i = 0; i < allenteties.Count; i++)
            {
                if (allenteties[i] == Owner)
                    continue;
                distanz = MathHelper.Distance(Owner.Position_Component.PixelPosition, allenteties[i].Position_Component.PixelPosition);
                if (distanz <= range)
                {
                    return allenteties[i];
                }
            }
            return null;
        }
    }
}
