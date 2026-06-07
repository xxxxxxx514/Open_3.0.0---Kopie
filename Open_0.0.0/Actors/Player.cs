using Open_0._0._0.Items;
using Open_0._0._0.Items.EquipmentItems;
using Open_0._0._0.Items.Food;
using Open_0._0._0.Items.Potions;

using Open_0._0._0.UI;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Open_0._0._0.Actors
{
    class Player : Actor
    {
        private ConvexShape convex2 = new ConvexShape(3);

        private BasicHealthUI healthUI;
        private BasicDeathUI deathUI;
        public BasicInventoryUI inventoryUI;

        public WorldItem dropItem;

        private Detector detector;
        public Equipment Equipment { get; set; }

        #region Properties & Fields
        private float _pushPack = 15;

        public bool isAlive = true;
        public PlayerState PlayerState;

        public RectangleShape Shape;
        public const int PLAYER_SIZE = 28;

        #endregion

        #region Constructor
        public Player(int gridX, int gridY)
        {
            Name = "Player";
            SetTag(Tag.Player);
            RestrictAcces = RestrictAccesEnum.Player;

            Body_Componente.ChangeBodyColor(CustomColors.PlayerColor);
            Body_Componente.SetBodyPostion(new Vector2f(300, 300));


            Body_Componente.Shape.OutlineColor = CustomColors.Red;
            Body_Componente.Shape.FillColor = CustomColors.Transparent;
            Body_Componente.Shape.OutlineThickness = 5;

            Body_Componente.Texture = new Texture("Player.png");
            Body_Componente.Sprite.Texture = Body_Componente.Texture;

            detector = new Detector(this);

            healthUI = new BasicHealthUI(this);
            deathUI = new BasicDeathUI(this);
            inventoryUI = new BasicInventoryUI(Inventory.Slots);
            // Item item = new Item(3, "schwert", "leichte waffe");

            Equipment=new Equipment();
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


            convex2.SetPoint(0, new Vector2f(0, 0));   // Oben mitte
            convex2.SetPoint(1, new Vector2f(50, 0)); // Rechts oben
            convex2.SetPoint(2, new Vector2f(25, 100));
            convex2.FillColor = new Color(255, 99, 71); // Tomatenrot
            convex2.OutlineColor = Color.Black;
            convex2.OutlineThickness = 2f;
            convex2.Position = Body_Componente.Position_Component.PixelPosition; // Position setzen

            Health_Component.IncreaseMaxHealth(1000000000);// nicht vergessen incrsae health wieder zurück ändern
            Health_Component.ApplyHeal(1000000000);
        }
        #endregion

        #region Public Methods
        public override void Update(float dt, Map map)
        {
            Attack_Component.Tick(dt);
            if (!isAlive) return;

            // 1) Input
            Vector2f dir = Movement_Component.GetInputDirection();
            Vector2f movement = dir * dt * Movement_Component.MoveSpeed;

            // 2) Bewegung
            Movement_Component.TryMove(movement, map, PLAYER_SIZE);

            // 3) Position aktualisieren
            Movement_Component.UpdatePosition();

            // 4) Items aufsammeln
            HandleItemPickup();

            // 5) Enemy-Kollisionen
            Attack(dt, map);
            HandleEnemyCollisions();
            OnDeath();
            healthUI.UpdatePlayerInfo();
            inventoryUI.Update();
        }
        private void HandleItemPickup()
        {
            for (int i = Game.WorldItems.Count - 1; i >= 0; i--)
            {
                var worldItem = Game.WorldItems[i];

                if (Collison2D.CollisionPlayerItem(
                    Position_Component.PixelPosition,
                    worldItem.Position,
                    new Vector2f(0, 0),
                    PLAYER_SIZE,
                    Tile.TILE_SIZE))
                {
                    PickUpItem(worldItem);
                    Game.WorldItems.RemoveAt(i);
                    Console.WriteLine($"Item aufgesammelt: {worldItem.Item.Name}");
                }
            }
        }

        private void HandleEnemyCollisions()
        {
            foreach (var enemy in Game.Entities) // oder deine Liste
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
        private void Attack(float dt, Map map)
        {
            Actor actor = detector.GetActorsInRange(Attack_Component.attackRange);
            if (actor != null)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    Attack_Component.Attack(actor);
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
        private void ChangeColorByState()
        {
            switch (PlayerState)
            {
                case PlayerState.Hurt:
                    Shape.FillColor = CustomColors.NESRed;
                    break;
                case PlayerState.Normal:
                    Shape.FillColor = CustomColors.PlayerColor;
                    break;
            }
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
        private void DropItem(Item item)
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
        public void Equip(Item item)
        {
            if (item == null) return;

            switch (item.EquipmentType)
            {
                case EquipmentType.Helm:
                    Equipment.Helm = item;
                    break;

                case EquipmentType.Chestplate:
                    Equipment.Chestplate = item;
                    break;

                case EquipmentType.Armguard:
                    Equipment.Armguard = item;
                    break;

                case EquipmentType.Pants:
                    Equipment.Pants = item;
                    break;

                case EquipmentType.Shoes:
                    Equipment.Shoes = item;
                    break;

                case EquipmentType.Lefthand:
                    Equipment.Lefthand = item;
                    break;

                case EquipmentType.Righthand:
                    Equipment.Righthand = item;
                    break;

                default:
                    return;
            }

            // Optional: Stats aktualisieren
            UpdateStats();
        }
        private void UpdateStats()// nur beim equipen
        {
            int totalArmor = 0;
            int totalDamage = 0;

            // Armor
            if (Equipment.Helm is Helm helm)
                totalArmor += helm.Armor;

            if (Equipment.Chestplate is Chestplate chest)
                totalArmor += chest.Armor;

            if (Equipment.Armguard is Armguard arm)
                totalArmor += arm.Armor;

            if (Equipment.Pants is Pants pants)
                totalArmor += pants.Armor;

            if (Equipment.Shoes is Shoes shoes)
                totalArmor += shoes.Armor;

            // Weapons
            if (Equipment.Righthand is Sword sword)
                totalDamage += sword.Damage;

            // if (Equipment.Lefthand is Shield shield)
            //   totalArmor += shield.DefenseBonus;

            // Apply to components
            Health_Component.IncreaseMaxHealth(Health_Component.MaxHealth + totalArmor); // temporär nur health erhöhen
            Attack_Component.SetDamage(Attack_Component.Damage+totalDamage);
        }



        #endregion

        #region Rendering
        public override void Render(RenderWindow window)
        {
            window.Draw(Body_Componente.Shape);
            window.Draw(Body_Componente.Sprite);
            convex2.Position = Position_Component.PixelPosition;
            window.Draw(convex2);
            healthUI.Render(window);
            deathUI.Render(window);
        }
        #endregion
        #region Events

        public void OnMouseKlick(RenderWindow window)
        {
            window.MouseButtonPressed += (sender, e) =>
            {
                if (!inventoryUI.toggle) return; // Inventar muss sichtbar sein

                Vector2i mousePos = Mouse.GetPosition(window);

                // ============================================================
                // RECHTSKLICK → Optionen anzeigen
                // ============================================================
                if (e.Button == Mouse.Button.Right)
                {
                    bool clickedOnSlot = false;

                    foreach (var slot in inventoryUI.Slots)
                    {
                        if (slot.Body.GetGlobalBounds().Contains(mousePos))
                        {
                            clickedOnSlot = true;

                            if (!slot.IsSlotFree) // Slot hat Item
                            {
                                inventoryUI.toogleOptions = true;
                                inventoryUI.PositionOptionButtons(slot);
                                Inventory.OptionSlot = slot;

                                Console.WriteLine($"Optionen für Slot {slot.ID} angezeigt");
                            }
                            else
                            {
                                inventoryUI.toogleOptions = false;
                                Inventory.OptionSlot = null;
                            }

                            break;
                        }
                    }

                    if (!clickedOnSlot)
                    {
                        inventoryUI.toogleOptions = false;
                        Inventory.OptionSlot = null;
                    }

                    return;
                }

                // ============================================================
                // LINKSKLICK
                // ============================================================
                if (e.Button == Mouse.Button.Left)
                {
                    // ------------------------------------------------------------
                    // 1. USE BUTTON
                    // ------------------------------------------------------------
                    if (inventoryUI.toogleOptions &&
                        inventoryUI.UseButton.GetGlobalBounds().Contains(mousePos) &&
                        Inventory.OptionSlot != null)
                    {
                        var slot = Inventory.OptionSlot;

                        if (slot.Item != null)
                        {
                            Console.WriteLine($"Benutze Item aus Slot {slot.ID}: {slot.Item.Name}");

                            slot.Item.Use(this); // Player ist "this" in Player-Klasse
                            Inventory.RemoveItemFromInventory(slot);
                            slot.Item = null;
                        }

                        inventoryUI.toogleOptions = false;
                        Inventory.OptionSlot = null;
                        return;
                    }

                    // ------------------------------------------------------------
                    // 2. DROP BUTTON
                    // ------------------------------------------------------------
                    if (inventoryUI.toogleOptions &&
                        inventoryUI.DropButton.GetGlobalBounds().Contains(mousePos) &&
                        Inventory.OptionSlot != null)
                    {
                        var slot = Inventory.OptionSlot;

                        Console.WriteLine($"Droppe Item aus Slot {slot.ID}");

                        DropItem(slot.Item);
                        Inventory.RemoveItemFromInventory(slot);
                        slot.Item = null;

                        inventoryUI.toogleOptions = false;
                        Inventory.OptionSlot = null;
                        return;
                    }

                    // ------------------------------------------------------------
                    // 3. SLOT AUSWÄHLEN / ITEMS TAUSCHEN
                    // ------------------------------------------------------------
                    foreach (var slot in inventoryUI.Slots)
                    {
                        if (slot.Body.GetGlobalBounds().Contains(mousePos))
                        {
                            if (Inventory.SelectedSlot == null)
                            {
                                Inventory.SelectedSlot = slot;
                                slot.Body.OutlineColor = CustomColors.BloodRed;
                                slot.Body.OutlineThickness = 4;
                            }
                            else
                            {
                                Inventory.SwapItems(Inventory.SelectedSlot, slot);

                                Inventory.SelectedSlot.Body.OutlineColor = CustomColors.Black;
                                Inventory.SelectedSlot.Body.OutlineThickness = 3;
                                Inventory.SelectedSlot = null;
                            }

                            break;
                        }
                    }

                    // ------------------------------------------------------------
                    // 4. Optionen schließen, wenn außerhalb geklickt
                    // ------------------------------------------------------------
                    inventoryUI.toogleOptions = false;
                }
            };
        }

    }
};
#endregion
