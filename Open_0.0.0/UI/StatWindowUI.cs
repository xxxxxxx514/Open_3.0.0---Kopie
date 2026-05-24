using Open_0._0._0.Actors;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.UI
{
     class StatWindowUI
    {
        // Größe
        private float _width = 1920 * 0.7f;
        private float _height = 1080 * 0.6f;
        public RectangleShape Background;

        public bool toggle = false;
        public RectangleShape Shape { get; private set; }
        private string _fontpath = @"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf";
        private Text _titleText;
        private Text _healthText;
        private Text _levelText;
        private Text _experienceText;
        private Text _positionText;
        private Text _inventoryText;
        private Font _font;

        private Player _player; // Referenz zum Player

        // Konstruktor mit Player-Referenz
        public StatWindowUI(Player player)
        {
            _player = player;

            Background = new RectangleShape();
            Background.Position = new Vector2f(0, 0);               // Oben links
            Background.Size = new Vector2f(1920, 1080);             // Ganzer Bildschirm
            Background.FillColor = CustomColors.Background;         // Schwarz mit 70% Transparenz

            // Hintergrund-Rechteck
            Shape = new RectangleShape();
            Shape.Position = new Vector2f((1920 - _width) / 2f, (1080 - _height) / 2f);
            Shape.Size = new Vector2f(_width, _height);
            Shape.FillColor = CustomColors.DarkOlive;
            Shape.OutlineThickness = 5f;
            Shape.OutlineColor = CustomColors.BurlyWood;

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
            float lineHeight = 40;

            // Titel
            _titleText = new Text("PLAYER INFORMATION", _font, 36);
            _titleText.Position = new Vector2f(startX, startY);
            _titleText.FillColor = CustomColors.BloodRed;
            _titleText.Style = Text.Styles.Bold;

            // Health
            _healthText = new Text("", _font, 28);
            _healthText.Position = new Vector2f(startX, startY + lineHeight * 2);
            _healthText.FillColor = CustomColors.AcidGreen;

            // Level
            _levelText = new Text("", _font, 28);
            _levelText.Position = new Vector2f(startX, startY + lineHeight * 3);
            _levelText.FillColor = CustomColors.Violet;

            // Experience
            _experienceText = new Text("", _font, 28);
            _experienceText.Position = new Vector2f(startX, startY + lineHeight * 4);
            _experienceText.FillColor = CustomColors.OrangeRed; // Orange

            // Position
            _positionText = new Text("", _font, 28);
            _positionText.Position = new Vector2f(startX, startY + lineHeight * 5);
            _positionText.FillColor = CustomColors.Aqua;

            // Inventory (Beispiel)
            _inventoryText = new Text("", _font, 28);
            _inventoryText.Position = new Vector2f(startX, startY + lineHeight * 7);
            _inventoryText.FillColor = CustomColors.Azure;
        }

        public void UpdatePlayerInfo()
        {

            _healthText.DisplayedString = $"Health: {_player.Health_Component.Health} / {_player.Health_Component._maxHealth}";
            _levelText.DisplayedString = $"Level: {_player.Level_Component.Level}";
            _experienceText.DisplayedString = $"XP: {_player.Level_Component.Experience}/{_player.Level_Component.ExperienceToNextLevel}";

            // Beispiel für Inventory (angenommen du hast eine Items-Liste)
            // _inventoryText.DisplayedString = $"Items: {_player.Inventory.Count}";
            _inventoryText.DisplayedString = "Inventory: Sword, Potion, Shield";
        }

        public void Render(RenderWindow window)
        {
            if (toggle)
            {
                UpdatePlayerInfo();
                // Erst das Hintergrund-Rechteck
                window.Draw(Background);
                window.Draw(Shape);

                // Dann alle Texte
                window.Draw(_titleText);
                window.Draw(_healthText);
                window.Draw(_levelText);
                window.Draw(_experienceText);
                window.Draw(_positionText);
                window.Draw(_inventoryText);
            }
        }
    }
}