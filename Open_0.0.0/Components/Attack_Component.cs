using Open_0._0._0.Actors;
using SFML.System;

namespace Open_0._0._0.Components
{
     class Attack_Component
    {
        public int Damage { get; private set; } = 100;
        private Clock _clock;
        public AttackTypeEnum AttackType { get; set; }
        private float _attackCooldown = 2;

        public Attack_Component()
        {
            _clock = new Clock();
        }
        public void Attack(Actor actor)
        {
            if (CanAttack())
            {
                actor.Health_Component.TakeDamage(Damage);
            }
        }
        private bool CanAttack()
        {
            if (_clock.ElapsedTime.AsSeconds() >= _attackCooldown)
            {
                _clock.Restart();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RaiseDamage(int amount)
        {
            if (amount > 0)
            {
                Damage += amount;
            }
            else { Console.WriteLine($"du kannst nicht Schaden erhöhen um {amount}"); }
        }
        public void DecreseDamage(int amount)
        {
            if (amount > 0)
            {
                Damage -= amount;
            }
            else { Console.WriteLine($"du kannst nicht Schaden verniedrigen um {amount}"); }
        }
        public void SetDamage(int amount) => Damage = amount;
    }
}
