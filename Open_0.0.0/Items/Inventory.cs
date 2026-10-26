using SFML.Graphics;

namespace Open_0._0._0.Items
{
    internal class Inventory
    {
        public Slot[] Slots = new Slot[MaxSlots];
        public static int MaxSlots = 20;

        public Slot SelectedSlot { get; set; } = null; // ✅ NULL statt Temp-Slot
        public Slot OptionSlot = null;
        public Inventory()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new Slot();
            }
        }
        public bool TryAddItem(Item item)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].Item == null)
                {
                    Slots[i].TryAddItem(item);
                    if (item != null)
                        Console.WriteLine(item.ToString() + " wurde im inventar hinzugefügt");
                    return true;
                }
            }
            Console.WriteLine("Inventar ist voll");
            return false;
        }
        public void RemoveItemFromInventory(Slot slot)
        {
            if (slot != null)
            {
                Console.WriteLine($"Entferne Item aus Slot {slot.ID}. Vorher: {slot.Item?.Name ?? "null"}");
                slot.Item = null;
                Console.WriteLine($"Nachher: {slot.Item?.Name ?? "null"}");
            }
        }
        public void Remove(Item item)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].Item == item)
                {
                    Slots[i].Clear();
                    return;
                }
            }
        }

        public bool CanSwop(Slot slot1, Slot slot2)
        {
            if (slot1.IsSlotFree && slot2.IsSlotFree)
                return false;

            return true;
        }
        public void SwapItems(Slot slot1, Slot slot2)
        {

            if (slot1 == null || slot2 == null) return;
            if (slot1 == slot2) return; // Gleicher Slot

            Item temp = slot1.Item;
            slot1.Item = slot2.Item;
            slot2.Item = temp;
            Console.WriteLine($"Getauscht: Slot{slot1.ID} ↔ Slot{slot2.ID}");
        }
        #region Events
        public void OnButtonClick(RenderWindow window)
        {

        }
        #endregion
    }
}