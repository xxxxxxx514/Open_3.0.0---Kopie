using Open_0._0._0.Components;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.Actors
{
    class Enemy : Actor
    {
        public const int ENEMY_SIZE = 28;
        public MonsterType monsterType = MonsterType.None;
        public Reward reward;
        public int ID = 0;


        private AI_Component AI_Component { get; set; }
        private float _detectionRange = 280f;
        private CircleShape _visualRange = new CircleShape();
        private Reward _reward;
        private const int basicXpAmount = 100;



        public Enemy() : base()
        {
            Name = "Enemy";
            AI_Component = new AI_Component(this);
            RestrictAcces = RestrictAccesEnum.Monster;
            _reward = new Reward();

            // Visual Range korrekt initialisieren
            _visualRange = new CircleShape();
            _visualRange.Radius = _detectionRange;
            _visualRange.FillColor = CustomColors.HalbTransparentRot;
            _visualRange.OutlineThickness = 1;
            _visualRange.OutlineColor = CustomColors.Red;

        }

        public Enemy(int gridX, int gridY, int damage = 5) : this()
        {
            Position_Component.SetPosition(Position_Component.ToPixelPosition(gridX, gridY));
            ID++;
        }

        public override void Update(float deltatime, Map map)
        {
            base.Update(deltatime, map);
            OnDeath();
            if (!IsAlive) return;

            // Visual Range Position aktualisieren (Zentrum auf Enemy)
            _visualRange.Position = new Vector2f(
                Position_Component.PixelPosition.X + ENEMY_SIZE / 2 - _detectionRange,
                Position_Component.PixelPosition.Y + ENEMY_SIZE / 2 - _detectionRange
            );

            AI_Component.Update(deltatime, map, Game.Player);
        }

        private void CalculateReward(Player player)
        {
            if (player.Level_Component.Level + 5 < Level_Component.Level)
                _reward.XP_Amount = basicXpAmount * 2;
            else
                _reward.XP_Amount = basicXpAmount;
        }

        private void GivePlayerReward(Player player)
        {
            CalculateReward(player);
            player.Level_Component.AddExperience(_reward.XP_Amount);
            Console.WriteLine($"XP reward amount: {_reward.XP_Amount}");
        }

        public override void OnDeath()
        {
            if (Health_Component.IsDead())
            {
                GivePlayerReward(Game.Player);
                IsAlive = false;
            }
        }

        public override void Render(RenderWindow window)
        {
            window.Draw(_visualRange);  // Erst den Kreis zeichnen (hintergrund)
            window.Draw(Body_Componente.Sprite);  // Dann den Sprite
        }

        public override string ToString()
        {
            return $"Name: {Name}, Type: {monsterType}";
        }
    }
}