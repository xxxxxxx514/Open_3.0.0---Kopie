using Open_0._0._0.Items;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    internal class Slot
    {
        private Item _item;

        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                UpdateVisual();
            }
        }


        public bool IsSlotFree => (_item == null);
        public RectangleShape Body { get; set; }
        private static int _ID = 0;
        public int ID { get; private set; } = 0;
        public const int Size = 64;
        public Slot(int tempslot, Item item = null)
        {
            //für temporer slot ohne Id zu erhöhen parameter 0 mitgeben
            if (tempslot == 0)
            {
                Item = item;

                Body = new RectangleShape();
                Body.Size = new Vector2f(Size, Size);
                Body.FillColor = CustomColors.Orange;
                Body.OutlineColor = CustomColors.Black;
                Body.OutlineThickness = 3;
            }
            else
            {
                Item = item;
                ID = _ID;
                _ID++;
                Body = new RectangleShape();
                Body.Size = new Vector2f(Size, Size);
                Body.FillColor = CustomColors.Orange;
                Body.OutlineColor = CustomColors.Black;
                Body.OutlineThickness = 3;
            }
        }
        public Slot(Item item = null)
        {
            Item = item;
            ID = _ID;
            _ID++;
            Body = new RectangleShape();
            Body.Size = new Vector2f(Size, Size);
            Body.FillColor = CustomColors.Orange;
            Body.OutlineColor = CustomColors.Black;
            Body.OutlineThickness = 3;
        }
        public Slot(Vector2f position)
        {
            ID = _ID;
            ID++;
            _ID = ID;
            Body = new RectangleShape();
            Body.Size = new Vector2f(Size, Size);
            Body.FillColor = CustomColors.Orange;
            Body.OutlineColor = CustomColors.Black;
            Body.OutlineThickness = 3;
            SetSlotPosition(position);
        }
        public Slot(float x, float y)
        {
            ID = _ID;
            ID++;
            _ID = ID;
            Body = new RectangleShape();
            Body.Size = new Vector2f(Size, Size);
            Body.FillColor = CustomColors.Orange;
            Body.OutlineColor = CustomColors.Black;
            Body.OutlineThickness = 3;
            SetSlotPosition(x, y);
        }
        public void Clear()
        {
            Item = null;
        }
        public void AddItem(Item item)
        {
            Item = item;
        }
        public void RemoveItem(Slot slot)
        {
            Item = null;
        }
        public Vector2f GetPostionAsVector2() => Body.Position;
        public (float, float) GetPositionAsTuple()
        {
            return (Body.Position.X, Body.Position.Y);
        }
        public int GetID() => ID;
        public void SetSlotPosition(Vector2f vector2F)
        {
            Body.Position = vector2F;
        }
        public void SetSlotPosition(float x, float y)
        {
            Vector2f vector2F = new Vector2f(x, y);
            Body.Position = vector2F;
        }
        public bool SlotFree() => IsSlotFree;
        private void UpdateVisual()
        {
            if (Body == null) return;

            Body.FillColor = (_item == null)
                ? CustomColors.Green
                : CustomColors.Orange;
        }
    }
}