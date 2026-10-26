using Open_0._0._0.Components;
using Open_0._0._0.Core;
using Open_0._0._0.Enums;
using Open_0._0._0.Items;
using Open_0._0._0.Items.EquipmentItems;
using Open_0._0._0.Items.Food;
using Open_0._0._0.Items.Potions;
using Open_0._0._0.Utils;
using Open_0._0._0.World;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Text;
namespace Open_0._0._0.Actors
{
    class Player : Actor
    {
        public WorldItem dropItem;

        private Detector detector;

        #region Properties & Fields

        public bool isAlive = true;
        public PlayerState PlayerState;

        public RectangleShape Shape;
        public const int PLAYER_SIZE = 28;

        public QuestManager QuestManager = new QuestManager();
        public DashAbility DashAbility = new DashAbility();
        public int hungerProSec = 2;
        public int thirstProSec = 2;

        private float _upthungertimer = 1f;

        private int _upthungerCooldown = 1;

        private float _dmgtimer = 1f;
        private int _dmgCooldown = 1;

        private int _dmgfromDesire = 1;
        #endregion

        #region Constructor
        public Player(int gridX, int gridY)
        {
            Name = "Player";
            SetTag(Tag.Player);
            RestrictAcces = RestrictAccesEnum.Player;

            Body_Componente.SetBodyPostion(new Vector2i(gridX, gridY));

            // Keine Skalierung — lass die Textur in Originalgröße
            // oder nur leicht skalieren
            Body_Componente.SetTexture("Player.png", PLAYER_SIZE);
            Console.WriteLine("beim constructor hash"+Body_Componente.Sprite.GetHashCode());

            detector = new Detector(this);

            // Item item = new Item(3, "schwert", "leichte waffe");

            Equipment = new Equipment();
            Item item = new Apple();
            Item potion = new HealthPotion();

            Inventory.TryAddItem(item);
            Inventory.TryAddItem(potion);

            Item helm = new Helm();
            Item chestplate = new Chestplate();
            Item armguard = new Armguard();
            Item pants = new Pants();
            Item shoes = new Shoes();
            Item sword = new Sword();         // Righthand
            Inventory.TryAddItem(helm);
            Inventory.TryAddItem(chestplate);
            Inventory.TryAddItem(armguard);
            Inventory.TryAddItem(pants);
            Inventory.TryAddItem(shoes);
            Inventory.TryAddItem(sword);

            Health_Component.IncreaseMaxHealth(1000);// nicht vergessen incrsae health wieder zurück ändern
            Health_Component.ApplyHeal(1000);

            Attack_Component.SetDamage(5);

            InitializeFeedback();

        }
        #endregion

        #region Public Methods


        public void UpdateHunger(float deltatime)
        {
            _upthungertimer -= deltatime;//timer runter zählen
            _dmgtimer -= deltatime;//timer runter zählen;

            if (_upthungertimer <= 0)
            {
                DesireManager.Desire.DrainNeeds(hungerProSec, thirstProSec);
                _upthungertimer = _upthungerCooldown;

                Console.WriteLine(ToString());

            }

            if (DesireManager.Desire.IsStarving())
            {
                if (_dmgtimer <= 0)
                {
                    Health_Component.ApplyDamage(_dmgfromDesire);
                    _dmgtimer = _dmgCooldown;
                }
            }
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Attack_Component.Tick(deltaTime);
            if (!isAlive) return;

            // 4) Items aufsammeln
            HandleItemPickup();


            UpdateHunger(deltaTime);
            //UpdateHunger();

            // Bedürfnisse aktualisieren → Actions aktivieren/deaktivieren
            //DesireManager.Desire.Update(deltaTime, DesireManager);

            // 5) Enemy-Kollisionen
            Attack(deltaTime);
            QuestManager.Update();
            HandleEnemyCollisions();

            DashAbility.Update(deltaTime);
        }
        public override void FixedUpdate(float fixedDelta)
        {
            if (!isAlive) return;

            // ✅ Bewegung gehört in FixedUpdate (konstante Rate für Physik)
            Vector2f dir = Movement_Component.GetInputDirection();
            Vector2f movement = dir * fixedDelta * Movement_Component.MoveSpeed;
            Movement_Component.TryMove(movement, PLAYER_SIZE);
            Movement_Component.UpdatePosition();

            DashAbility.FixedUpdate(fixedDelta);
        }
        private void HandleItemPickup()
        {
            for (int i = GameWorld.WorldItems.Count - 1; i >= 0; i--)
            {
                var worldItem = GameWorld.WorldItems[i];

                if (Collison2D.CollisionPlayerItem(
                    Position_Component.PixelPosition,
                    worldItem.Position,
                    new Vector2f(0, 0),
                    PLAYER_SIZE,
                    Tile.TILE_SIZE))
                {
                    PickUpItem(worldItem);
                    GameWorld.WorldItems.RemoveAt(i);
                    Console.WriteLine($"Item aufgesammelt: {worldItem.Item.Name}");
                }
            }
        }

        private void HandleEnemyCollisions()
        {
            foreach (var enemy in GameWorld.Entities) // oder deine Liste
            {
                if (Collison2D.CollisionPlayerEnemy(
                    Position_Component.PixelPosition,
                    enemy.Position_Component.PixelPosition,
                    new Vector2f(0, 0),
                    PLAYER_SIZE,
                    Enemy.ENEMY_SIZE))
                {
                    // Attack_Component.Attack(enemy);
                    //Console.WriteLine($"{enemy.Name} getroffen");
                }
            }
        }
        private void Attack(float dt)
        {

            Actor actor = detector.GetActorsInRange(Attack_Component.attackRange);
            if (actor != null)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    Attack_Component.Attack(actor);

                    if (QuestManager.Quest != null)
                        Console.WriteLine(QuestManager.Quest.ToString());
                }
            }
        }
        public Vector2f ToPixelPosition(int gridX, int gridY)
        {
            return new Vector2f(gridX * PLAYER_SIZE, gridY * PLAYER_SIZE);
        }
        public void PickUpItem(WorldItem worldItem)
        {

            Inventory.TryAddItem(worldItem.Item);

        }
       
        #endregion

        #region Private Methods
        public override void OnDeath()
        {
            if (Health_Component.IsDead())
            {
                isAlive = false;
                return;
            }
        }
        public void DropItem(Item item)
        {
            if (item == null) return;

            Vector2f dropPosition = new Vector2f(
                Position_Component.PixelPosition.X + Tile.TILE_SIZE,
                Position_Component.PixelPosition.Y
            );

            dropItem = new WorldItem(item, dropPosition, Name);

            if (item.Shape != null)
            {
                item.Shape.Position = dropPosition;
            }
        }
        #endregion

        #region Rendering
        public override void Render(RenderWindow window, Vector2f camaraPos)
        {
            window.Draw(Body_Componente.Sprite);
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"NPC: {Name}");
            sb.AppendLine($"Health: {Health_Component.Health}");
            sb.AppendLine($"Hunger: {DesireManager.Desire.Hunger}, Thirst: {DesireManager.Desire.Thirst}, Happiness: {DesireManager.Desire.Happines}");
            sb.AppendLine($"Pos: {Position_Component.PixelPosition.X:F0}, {Position_Component.PixelPosition.Y:F0}");
            sb.AppendLine($"Grid: {Position_Component.GridPosition.X}, {Position_Component.GridPosition.Y}");

            return sb.ToString();
        }
    }

}

