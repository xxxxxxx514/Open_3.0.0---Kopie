using Open_0._0._0.Actors;
using SFML.System;

namespace Open_0._0._0
{
    class DashAbility : IAbility
    {
        public string Name { get; }
        public string Description { get; }
        public float ManaCost { get; }
        public float StaminaCost { get; }
        public float Cooldown { get; }
        public float CurrentCooldown { get; set; }
        public AbilityType Type { get; }

        private Actor _user;

        private float _dashDuration = 0.15f;
        private float _dashTimeLeft = 0f;
        private Vector2f _dashDirection;

        private float _dashSpeedMultiplier = 4f;

        public DashAbility()
        {
            Name = "Dash";
            Description = "Führt einen schnellen Sprint aus.";
            Cooldown = 3f;
            CurrentCooldown = 0f;
            Type = AbilityType.Dash;
        }

        public bool TryExecute(Actor user, Actor target)
        {
            if (CurrentCooldown > 0)
                return false;

            _user = user;

            // Richtung beim Start locken
            _dashDirection = _user.Movement_Component.GetInputDirection();

            // Wenn keine Richtung gedrückt → kein Dash
            if (_dashDirection.X == 0 && _dashDirection.Y == 0)
                return false;

            _dashTimeLeft = _dashDuration;
            CurrentCooldown = Cooldown;

            return true;
        }

        public void Update(float dt)
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= dt;
                if (CurrentCooldown < 0)
                    CurrentCooldown = 0;
            }

            if (_dashTimeLeft > 0)
            {
                _dashTimeLeft -= dt;
                if (_dashTimeLeft < 0)
                    _dashTimeLeft = 0;
            }
        }

        public void FixedUpdate(float fixedDelta)
        {
            if (_dashTimeLeft > 0)
            {
                Vector2f movement = _dashDirection *
                                    _user.Movement_Component.MoveSpeed *
                                    _dashSpeedMultiplier *
                                    fixedDelta;

                _user.Movement_Component.TryMove(movement, _user.Body_Componente._size);
            }
        }

        public void OnCooldownComplete() { }
    }

}
