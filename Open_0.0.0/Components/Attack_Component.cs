using Open_0._0._0.Actors;
using SFML.System;

namespace Open_0._0._0.Components
{
    class Attack_Component:ITickable
    {
        public int Damage { get; private set; } = 100;
        private float _attackTimer = 2f;
        public AttackTypeEnum AttackType { get; set; }
        private float _attackCooldown = 2;
        public void Tick(float dt)
        {
            if (_attackTimer > 0)
                _attackTimer -= dt;
        }

        public void Attack(Actor target)
        {
            if (!CanAttack())
            {
                return;
            }
            else
            {
                target.Health_Component.TakeDamage(Damage);
                _attackTimer = _attackCooldown;
            }
        }
        public bool CanAttack() => _attackTimer <= 0f;
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
