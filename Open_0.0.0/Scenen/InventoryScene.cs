using Open_0._0._0.Core;
using Open_0._0._0.Items;
using Open_0._0._0.UI;
using Open_0._0._0.Utils;
using SFML.System;
using SFML.Window;
namespace Open_0._0._0.Scenen
{
    class InventoryScene : Scene
    {
        private BasicInventoryUI BasicInventoryUI { get; set; }
        public InventoryScene(SceneManager sceneManager, Slot[] slots) : base(sceneManager)
        {
            BasicInventoryUI = new BasicInventoryUI(slots);
            BasicInventoryUI.toggle = true;

            KeyBoard_Input.OnKeyPressed += OnEscClick;
        }
        public override void Update(float deltaTime)
        {
            CheckSlots();
            OnRightClick();
            OnLeftClick();
        }
        public override void FixedUpdate(float fixedDelta)
        {

        }
        public override void Render()
        {
            BasicInventoryUI.Render(Window);
        }
        private void CheckSlots()
        {
            var Slots = BasicInventoryUI.Slots;
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].SlotFree() || Slots[i].Item == null)
                    Slots[i].Body.FillColor = CustomColors.Green;
                else
                    Slots[i].Body.FillColor = CustomColors.Orange;
            }
        }
        private void OnRightClick()
        {
            if (!BasicInventoryUI.toggle) return; // Inventar muss sichtbar sein
            Vector2i mousePos = Mouse.GetPosition(Window); //RECHTSKLICK → Optionen anzeigen

            if (Mouse_Input.RightPressed)
            {
                Console.WriteLine("Clicked Options");
                bool clickedOnSlot = false;

                foreach (var slot in BasicInventoryUI.Slots)
                {
                    if (slot.Body.GetGlobalBounds().Contains(mousePos))
                    {
                        clickedOnSlot = true;

                        if (!slot.IsSlotFree) // Slot hat Item
                        {
                            BasicInventoryUI.toogleOptions = true;
                            BasicInventoryUI.PositionOptionButtons(slot);
                            GameWorld.Player.Inventory.OptionSlot = slot;

                            Console.WriteLine($"Optionen für Slot {slot.ID} angezeigt");
                        }
                        else
                        {
                            BasicInventoryUI.toogleOptions = false;
                            GameWorld.Player.Inventory.OptionSlot = null;
                        }

                        break;
                    }
                }
                if (!clickedOnSlot)
                {
                    BasicInventoryUI.toogleOptions = false;
                    GameWorld.Player.Inventory.OptionSlot = null;
                }

                return;

            }
        }
        private void OnLeftClick()
        {
            if (Mouse_Input.LeftPressed)
            {
                
                Vector2i mousePos = Mouse.GetPosition(Window);

                if (BasicInventoryUI.toogleOptions &&
                            BasicInventoryUI.UseButton.GetGlobalBounds().Contains(mousePos) &&
                            GameWorld.Player.Inventory.OptionSlot != null)
                {
                    var slot = GameWorld.Player.Inventory.OptionSlot;

                    if (slot.Item != null)
                    {
                        Console.WriteLine($"Benutze Item aus Slot {slot.ID}: {slot.Item.Name}");

                        slot.Item.Use(GameWorld.Player); // Player ist "this" in Player-Klasse
                        GameWorld.Player.Inventory.RemoveItemFromInventory(slot);
                        slot.Item = null;
                    }

                    BasicInventoryUI.toogleOptions = false;
                    GameWorld.Player.Inventory.OptionSlot = null;
                    return;
                }
                if (BasicInventoryUI.toogleOptions &&
                        BasicInventoryUI.DropButton.GetGlobalBounds().Contains(mousePos) &&
                        GameWorld.Player.Inventory.OptionSlot != null)
                {
                    var slot = GameWorld.Player.Inventory.OptionSlot;

                    Console.WriteLine($"Droppe Item aus Slot {slot.ID}");

                    GameWorld.Player.DropItem(slot.Item);
                    GameWorld.Player.Inventory.RemoveItemFromInventory(slot);
                    slot.Item = null;

                    BasicInventoryUI.toogleOptions = false;
                    GameWorld.Player.Inventory.OptionSlot = null;
                    return;
                }

                // ------------------------------------------------------------
                // 3. SLOT AUSWÄHLEN / ITEMS TAUSCHEN
                // ------------------------------------------------------------
                foreach (var slot in BasicInventoryUI.Slots)
                {
                    if (slot.Body.GetGlobalBounds().Contains(mousePos))
                    {
                        if (GameWorld.Player.Inventory.SelectedSlot == null)
                        {
                            GameWorld.Player.Inventory.SelectedSlot = slot;
                            slot.Body.OutlineColor = CustomColors.BloodRed;
                            slot.Body.OutlineThickness = 4;
                        }
                        else
                        {
                            GameWorld.Player.Inventory.SwapItems(GameWorld.Player.Inventory.SelectedSlot, slot);

                            GameWorld.Player.Inventory.SelectedSlot.Body.OutlineColor = CustomColors.Black;
                            GameWorld.Player.Inventory.SelectedSlot.Body.OutlineThickness = 3;
                            GameWorld.Player.Inventory.SelectedSlot = null;
                        }

                        break;
                    }
                }

                // ------------------------------------------------------------
                // 4. Optionen schließen, wenn außerhalb geklickt
                // ------------------------------------------------------------
                BasicInventoryUI.toogleOptions = false;
            }

        }
        private void OnEscClick(Keyboard.Key key)
        {
            if(key == Keyboard.Key.Escape)
            {
                Console.WriteLine("Pressed Escape");
                BasicInventoryUI.toggle = !BasicInventoryUI.toggle;
                if (!BasicInventoryUI.toggle)
                    BasicInventoryUI.TurnOffAllToggle();

                SceneManager.ChangeToGameScene();
            }
        }
    }
}

