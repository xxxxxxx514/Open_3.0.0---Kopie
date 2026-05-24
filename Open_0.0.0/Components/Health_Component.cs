using Open_0._0._0.Actors;

namespace Open_0._0._0.Components
{
     class Health_Component
    {
            public Action HealthChange;

            // Referenz auf den Actor, dem diese Health gehört
            public Actor Owner { get; set; }

            private int _health = 100;
            public int _maxHealth = 100;

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
                _maxHealth = newmaxHealth;
                Health = newmaxHealth;
            }

            private void SetHealth(int health)
            {
                // Clamped, damit Health nie < 0 oder > maxHealth wird
                Health = Math.Clamp(health, 0, _maxHealth);
            }

            public void TakeDamage(int amount)
            {
                SetHealth(Health - amount);
                HealthChange?.Invoke();

                if (Health <= 0)
                {
                    Owner.IsAlive = false;
                    Owner.OnDeath();
                }
            }

            public void TakeHeal(int amount)
            {
                SetHealth(Health + amount);
                HealthChange?.Invoke();
            }

            public bool IsDead()
            {
                return Health <= 0;
            }
        }
    }
