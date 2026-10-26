using Open_0._0._0.Combat;
using Open_0._0._0.Components;
using Open_0._0._0.Core;
using Open_0._0._0.Enums;
using Open_0._0._0.Quests;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.Actors
{
    class Enemy : Actor
    {
        public const int ENEMY_SIZE = 28;
        public MonsterType Type = MonsterType.None;
        public int ID = 0;


        private AI_Component AI_Component { get; set; }
        private float _detectionRange = 280f;
        private CircleShape _visualRange = new CircleShape();
        private Reward _reward;
        private const int basicXpAmount = 100;
        public Detector Detector { get; set; }

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
            Died += Enemy_Died;
            Attack_Component.SetDamage(5);

            InitializeFeedback();


        }

        private void Enemy_Died(Actor obj)
        {
            if (GameWorld.Player.QuestManager.Quest != null)
            {
                if (GameWorld.Player.QuestManager.Quest is KillQuest kill)
                {
                    if (kill.target_Type == Type)
                    {
                        kill.IncreaseKills(1);
                    }
                }
                GivePlayerReward(GameWorld.Player);
            }
        }


        public Enemy(int gridX, int gridY, int damage = 5) : this()
        {
            Position_Component.SetPosition(Position_Component.ToPixelPosition(gridX, gridY));
            ID++;
        }

        public override void Update(float deltatime)
        {
            base.Update(deltatime);
            OnDeath();
            if (!IsAlive) return;

            // Visual Range Position aktualisieren (Zentrum auf Enemy)
            _visualRange.Position = new Vector2f(
                Position_Component.PixelPosition.X + ENEMY_SIZE / 2 - _detectionRange,
                Position_Component.PixelPosition.Y + ENEMY_SIZE / 2 - _detectionRange
            );
            AI_Component.Update(deltatime);

        }

        public void SetToken(MonsterType token)
        {
            _reward.Token = token;
        }
        public override void FixedUpdate(float fixedDelta)
        {
            AI_Component.FixedUpdate(fixedDelta);
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
                base.OnDeath();
            }
        }

        public override void Render(RenderWindow window, Vector2f cameraPos)
        {
            window.Draw(_visualRange);
            window.Draw(Body_Componente.Sprite);
        }


        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}";
        }
    }
}