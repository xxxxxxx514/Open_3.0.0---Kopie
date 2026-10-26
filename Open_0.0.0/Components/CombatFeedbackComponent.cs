using Open_0._0._0.Actors;
using SFML.Graphics;

namespace Open_0._0._0.Components
{
    class CombatFeedbackComponent : ITickable
    {
        private Actor _actor; // Actor direkt speichern
        private float _flashTimer = 0f;

        public CombatFeedbackComponent(Actor actor)
        {
            _actor = actor;

            actor.Health_Component.OnDamage += (amount) =>
            {
                _flashTimer = 0.15f;
                _actor.Body_Componente.Sprite.Color = Color.Red;
            };
        }

        public void Tick(float dt)
        {
            if (_flashTimer > 0f)
            {
                    Console.WriteLine(_actor.Body_Componente.Sprite.GetHashCode());

                _flashTimer -= dt;
                if (_flashTimer <= 0f)
                {
                    _actor.Body_Componente.Sprite.Color = Color.White;
                }
            }
        }
    }
}

