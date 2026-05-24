using Open_0._0._0.Actors;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.UI
{
     class BasicHealthUI
    {
        private float _width = 1920 * 0.2f;
        private float _height = 1080 * 0.05f;
        public RectangleShape Shape { get; private set; }
        private string _fontpath = @"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf";
        private Text _healthText;
        private Font _font;

        private Player _player;
        public BasicHealthUI(Player player)
        {
            _player = player;

            // Hintergrund-Rechteck
            Shape = new RectangleShape();
            Shape.Position = new Vector2f(0, 0);
            Shape.Size = new Vector2f(_width, _height);
            Shape.FillColor = CustomColors.BabyBlue;

            // Font laden (ändere den Pfad zu deiner Font-Datei)
            _font = new Font(_fontpath); // Oder "fonts/arial.ttf"

            // Text-Objekte erstellen
            CreateTextObjects();

            // Initiale Texte setzen
            UpdatePlayerInfo();
        }

        private void CreateTextObjects()
        {
            float startX = Shape.Position.X + 40;
            float startY = Shape.Position.Y + 40;

            // Health
            _healthText = new Text("", _font, 28);
            _healthText.Position = new Vector2f(0, 0);
            _healthText.FillColor = CustomColors.HealthBar;

        }
        public void UpdatePlayerInfo()
        {
            _healthText.DisplayedString = $"Health: {_player.Health_Component.Health}/{_player.Health_Component._maxHealth}";
        }

        public void Render(RenderWindow window)
        {
            // Erst das Hintergrund-Rechteck
            window.Draw(Shape);

            // Dann alle Texte
            window.Draw(_healthText);
        }
    }
}