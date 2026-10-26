using Open_0._0._0.Actors;
using Open_0._0._0.Relationships.Desires;
using Open_0._0._0.Utils;
using SFML.System;

namespace Open_0._0._0.Actions
{
    class WanderAction : IAction
    {
        private Vector2f _direction;
        private float _changeDirTimer = 0f;
        private Random _rng = new Random();
        private float alterSpeed = 0.7f;

        public WanderAction()
        {
            // Start mit zufälliger Richtung
            _direction = RandomDirection();
        }

        public void Execute(Actor actor, float deltaTime)
        {
            _changeDirTimer -= deltaTime;

            // Alle 1–3 Sekunden neue Richtung
            if (_changeDirTimer <= 0f)
            {
                _direction = RandomDirection();
                _changeDirTimer = (float)(_rng.NextDouble() * 2.0 + 1.0); // 1–3 Sekunden
            }

            // Bewegung ausführen
            actor.Movement_Component.TryMove(_direction, actor.Body_Componente._size);
            //actor.DesireManager.RemoveAction<WanderAction>();
            //Console.WriteLine("hat sich bewegt");

        }

        // NEU (richtig)
        private Vector2f RandomDirection()
        {
            float x = (float)(_rng.NextDouble() * 2 - 1);
            float y = (float)(_rng.NextDouble() * 2 - 1);
            Vector2f dir = MathHelper.Normalize(new Vector2f(x, y)); // Ergebnis behalten
            return dir * alterSpeed;
        }

        public int GetPriority(Actor actor, Desire desire)
        {
            return 100 - 50;
        }
    }
}
