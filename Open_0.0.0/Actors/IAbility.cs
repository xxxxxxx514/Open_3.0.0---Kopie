using Open_0._0._0.Components;

namespace Open_0._0._0.Actors
{
    interface IAbility
    {
        string Name { get; }
        string Description { get; }
        float Cooldown { get; }
        float CurrentCooldown { get; set; }
        float ManaCost { get; }
        float StaminaCost { get; }
        AbilityType Type { get; }

        bool TryExecute(Actor user,Actor target);
        void Update(float dt);
        void OnCooldownComplete();
    }
}
