using Open_0._0._0.Actors;
using Open_0._0._0.Utils;
using SFML.System;

namespace Open_0._0._0.Actions
{
    class WanderAction : IAction
    {
            private Vector2f _direction;
            private float _changeDirTimer = 0f;
            private Random _rng = new Random();

            public WanderAction()
            {
                // Start mit zufälliger Richtung
                _direction = RandomDirection();
            }

            public void Execute(Actor actor, float deltaTime, Map map)
            {
                _changeDirTimer -= deltaTime;

                // Alle 1–3 Sekunden neue Richtung
                if (_changeDirTimer <= 0f)
                {
                    _direction = RandomDirection();
                    _changeDirTimer = (float)(_rng.NextDouble() * 2.0 + 1.0); // 1–3 Sekunden
                }

                // Bewegung ausführen
                actor.Movement_Component.TryMove(_direction, map, actor.Body_Componente._size);
            }

            private Vector2f RandomDirection()
            {
                float x = (float)(_rng.NextDouble() * 2 - 1); // -1 bis 1
                float y = (float)(_rng.NextDouble() * 2 - 1);
                var dir = new Vector2f(x, y);

                // Normieren
                MathHelper.Normalize(dir);
                float length = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                if (length == 0) return new Vector2f(0, 0);

                return new Vector2f(dir.X / length, dir.Y / length);
            }

        public int GetPriority(Actor actor, Desire desire)
        {
            return 100-50;
        }
    }
}
