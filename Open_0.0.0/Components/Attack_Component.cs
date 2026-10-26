using Open_0._0._0.Actors;
using Open_0._0._0.Combat;
namespace Open_0._0._0.Components
{
    class Attack_Component:ITickable
    {
        private float _attackTimer = 2f;

        private int _attackCooldown = 2;
        public int Damage { get; private set; } = 5;
        public float attackRange = 70f;

        public AttackTypeEnum AttackType { get; set; }

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
                target.Health_Component.ApplyDamage(Damage);
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
        public void SetAttackRange(float range)=> attackRange = range;
    }
}
