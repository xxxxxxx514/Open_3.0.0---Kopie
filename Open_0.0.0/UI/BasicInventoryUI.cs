using Open_0._0._0.Items;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
namespace Open_0._0._0.UI
{
    class BasicInventoryUI
    {
        public bool isSlotDragging = false;
        public Slot[] Slots { get; set; }

        private string _fontpath = @"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf";
        public bool toggle = false;
        public bool toogleOptions = false;

        private RectangleShape Background;

        public RectangleShape DropButton;
        public RectangleShape UseButton;

        private Font _font;
        public Text dropText;
        public Text useText;

        public Slot SelectedSlot;

        public BasicInventoryUI(Slot[] slots)
        {
            _font = new Font(_fontpath);

            Slots = slots;
            PositionAllSlots();

            Background = new RectangleShape();
            Background.Position = new Vector2f(0, 0);
            Background.Size = new Vector2f(1920, 1080);
            Background.FillColor = CustomColors.Background;

            // DROP BUTTON
            DropButton = new RectangleShape();
            DropButton.FillColor = CustomColors.Beige;
            DropButton.Size = new Vector2f(Slot.Size, Slot.Size / 2);
            DropButton.OutlineColor = CustomColors.Black;

            dropText = new Text("Drop", _font, 16);
            dropText.OutlineColor = CustomColors.White;

            // USE BUTTON
            UseButton = new RectangleShape();
            UseButton.FillColor = CustomColors.Beige;
            UseButton.Size = new Vector2f(Slot.Size, Slot.Size / 2);
            UseButton.OutlineColor = CustomColors.Black;

            useText = new Text("Use", _font, 16);
            useText.OutlineColor = CustomColors.White;
        }

        public void TurnOffAllToggle()
        {
            toggle = false;
            toogleOptions = false;
        }

        private void PositionAllSlots()
        {
            Vector2f startpoint = new Vector2f(780, 300);
            Vector2f umbruch = startpoint;

            Slots[0].SetSlotPosition(startpoint);

            for (int i = 1; i < Slots.Length; i++)
            {
                if (i % 5 == 0)
                {
                    umbruch = new Vector2f(startpoint.X, umbruch.Y + Slot.Size);
                    Slots[i].SetSlotPosition(umbruch);
                }
                else
                {
                    Slots[i].SetSlotPosition(
                        Slots[i - 1].Body.Position.X + Slot.Size,
                        Slots[i - 1].Body.Position.Y
                    );
                }
            }
        }

        public void PositionOptionButtons(Slot slot)
        {
            SelectedSlot = slot;

            Vector2f pos = slot.Body.Position;

            // Drop Button
            DropButton.Position = new Vector2f(pos.X + Slot.Size, pos.Y);
            dropText.Position = DropButton.Position;

            // Use Button
            UseButton.Position = new Vector2f(pos.X + Slot.Size, pos.Y + Slot.Size / 2);
            useText.Position = UseButton.Position;
        }

        public void Update()
        {
            CheckSlots();
        }

        public void CheckSlots()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].SlotFree() || Slots[i].Item == null)
                    Slots[i].Body.FillColor = CustomColors.Green;
                else
                    Slots[i].Body.FillColor = CustomColors.Orange;
            }
        }

        private void Renderr(RenderWindow window)
        {
            if (toggle)
            {
                window.Draw(Background);

                foreach (var slot in Slots)
                    window.Draw(slot.Body);
            }

            if (toogleOptions)
            {
                window.Draw(DropButton);
                window.Draw(dropText);

                window.Draw(UseButton);
                window.Draw(useText);
            }
        }

        public void Render(RenderWindow window)
        {
            if (toggle)
            {
                window.Draw(Background);

                foreach (var slot in Slots)
                    window.Draw(slot.Body);
            }

            if (toogleOptions)
            {
                window.Draw(DropButton);
                window.Draw(dropText);

                window.Draw(UseButton);
                window.Draw(useText);
            }
        }
    }
}