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


        #region Properties & Fields
        private float _attackCooldown = 2;
        private Clock _clock;
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


            _clock = new Clock();
            healthUI = new BasicHealthUI(this);
            deathUI = new BasicDeathUI(this);
            inventoryUI = new BasicInventoryUI(Inventory.Slots);
            Item item = new Item(3, "schwert", "leichte waffe");
            Item item3 = new Item(3, "schwert", "sgas waffe");
            Item item2 = new Item(3, "Bogen", " waffe");
            Item item4 = new Item(3, "Hand", " waffe");
            Item item5 = new Item(3, "Magie", "sgas waffe");


            Inventory.TryAddItem(item3);
            Inventory.TryAddItem(item);
            Inventory.TryAddItem(item2);
            Inventory.TryAddItem(item4);
            Inventory.TryAddItem(item5);

            convex2.SetPoint(0, new Vector2f(0, 0));   // Oben mitte
            convex2.SetPoint(1, new Vector2f(50, 0)); // Rechts oben
            convex2.SetPoint(2, new Vector2f(25, 100));
            convex2.FillColor = new Color(255, 99, 71); // Tomatenrot
            convex2.OutlineColor = Color.Black;
            convex2.OutlineThickness = 2f;
            convex2.Position = Body_Componente.Position_Component.PixelPosition; // Position setzen

            Health_Component.IncreaseMaxHealth(1000000000);// nicht vergessen incrsae health wieder zurück ändern
        }
        #endregion

        #region Public Methods
        public override void Update(float dt, Map map)
        {
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
                    Attack_Component.Attack(enemy);
                    Console.WriteLine($"{enemy.Name} getroffen");
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

                // RECHTSKLICK - Optionen anzeigen/ausblenden
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
                                inventoryUI.PostionDropButton(slot);
                                Inventory.OptionSlot = slot;
                                Console.WriteLine($"Optionen für Slot {slot.ID} angezeigt");
                            }
                            else // Slot ist leer
                            {
                                inventoryUI.toogleOptions = false;
                                Inventory.OptionSlot = null;
                            }
                            break; // Slot gefunden - Schleife beenden
                        }
                    }

                    // Wenn NICHT auf Slot geklickt wurde
                    if (!clickedOnSlot)
                    {
                        inventoryUI.toogleOptions = false;
                        Inventory.OptionSlot = null;
                    }
                    return; // Nach Rechtsklick nichts mehr machen
                }

                // LINKSKLICK
                if (e.Button == Mouse.Button.Left)
                {
                    // 1. Prüfe Drop-Button (wenn Optionen sichtbar sind)
                    if (inventoryUI.toogleOptions &&
                        inventoryUI.DropButton.GetGlobalBounds().Contains(mousePos) &&
                        Inventory.OptionSlot != null)
                    {
                        Console.WriteLine($"Entferne Item aus Slot {Inventory.OptionSlot.ID}");
                        DropItem(Inventory.OptionSlot.Item);

                        Inventory.RemoveItemFromInventory(Inventory.OptionSlot);
                        inventoryUI.Update();
                        inventoryUI.toogleOptions = false;
                        Inventory.OptionSlot = null;
                        return; // Drop erledigt - nichts mehr machen
                    }

                    // 2. Prüfe Slots (für Auswahl/Tausch)
                    foreach (var slot in inventoryUI.Slots)
                    {
                        if (slot.Body.GetGlobalBounds().Contains(mousePos))
                        {
                            if (Inventory.SelectedSlot == null) // Slot auswählen
                            {
                                Inventory.SelectedSlot = slot;
                                slot.Body.OutlineColor = CustomColors.BloodRed;
                                slot.Body.OutlineThickness = 4;
                            }
                            else // Items tauschen
                            {
                                Inventory.SwapItems(Inventory.SelectedSlot, slot);
                                Inventory.SelectedSlot.Body.OutlineColor = CustomColors.Black;
                                Inventory.SelectedSlot.Body.OutlineThickness = 3;
                                Inventory.SelectedSlot = null;
                            }
                            break;
                        }
                    }

                    // 3. Optionen ausblenden bei Klick außerhalb
                    inventoryUI.toogleOptions = false;
                }
            };
        }
        #endregion

    }
}