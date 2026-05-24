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

        private Font _font;
        public Text dropText;

        public BasicInventoryUI(Slot[] slots)
        {
            _font = new Font(_fontpath);
            dropText = new Text("Drop", _font, 16);

            Slots = slots;
            // Text-Objekte erstellen
            PositionAllSlots();
            Background = new RectangleShape();
            Background.Position = new Vector2f(0, 0);               // Oben links
            Background.Size = new Vector2f(1920, 1080);             // Ganzer Bildschirm
            Background.FillColor = CustomColors.Background;

            DropButton = new RectangleShape();
            DropButton.FillColor = CustomColors.Beige;
            DropButton.Size = new Vector2f(Slot.Size, Slot.Size / 2);
            DropButton.OutlineColor = CustomColors.Black;

            dropText.OutlineColor = CustomColors.White;
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
                    // Zur nächsten Zeile wechseln
                    umbruch = new Vector2f(startpoint.X, umbruch.Y + Slot.Size);
                    Slots[i].SetSlotPosition(umbruch);
                }
                else
                {
                    // In derselben Zeile bleiben
                    Slots[i].SetSlotPosition(Slots[i - 1].Body.Position.X + Slot.Size,
                                        Slots[i - 1].Body.Position.Y);
                }
            }
        }
        public void PostionDropButton(Slot slot)
        {
            Vector2f poison = new Vector2f(slot.Body.Position.X + Slot.Size, slot.Body.Position.Y);
            DropButton.Position = poison;
            dropText.Position = DropButton.Position;
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
                {
                    Slots[i].Body.FillColor = CustomColors.Green;
                }
                else Slots[i].Body.FillColor = CustomColors.Orange;
            }
        }
        private void Renderr(RenderWindow window)
        {
            if (toggle)
            {
                window.Draw(Background);
                foreach (var slot in Slots)
                {
                    window.Draw(slot.Body);
                }
            }
            if (toogleOptions)
            {
                window.Draw(DropButton);
                window.Draw(dropText);
            }
        }

        public void Render(RenderWindow window)
        {
            Renderr(window);
        }
    }
}