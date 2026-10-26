using Open_0._0._0.Actors;

namespace Open_0._0._0.Components
{
    class Health_Component : ITickable
    {
        public event Action<int> OnDamage;
        public event Action<int> OnHeal;
        public event Action<int> OnHealthChanged;

        // Referenz auf den Actor, dem diese Health gehört
        public Actor Owner { get; set; }

        private int _health = 100;
        public int MaxHealth = 100;
        private float _timer = 0;


        public Health_Component(Actor owner)
        {
            Owner = owner;
        }
        public int Health
        {
            get => _health;
            private set => _health = value;
        }

        public void IncreaseMaxHealth(int newmaxHealth)
        {
            MaxHealth = newmaxHealth;
            //Health = newmaxHealth;
        }

        private void SetHealth(int health)
        {
            // Clamped, damit Health nie < 0 oder > maxHealth wird
            Health = Math.Clamp(health, 0, MaxHealth);
        }

        public void ApplyDamage(int amount)
        {
            SetHealth(Health - amount);
            OnHealthChanged?.Invoke(amount);
            OnDamage?.Invoke(amount);

            if (Health <= 0)
            {
                Owner.IsAlive = false;
                Owner.OnDeath();
            }
        }

        public void ApplyHeal(int healamount)
        {
            SetHealth(Health + healamount);

            OnHealthChanged?.Invoke(healamount);
            OnHeal?.Invoke(healamount);
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
        public void Tick(float dt)
        {
            if (_timer > 0)
                _timer -= dt;
        }
    }
}
