using Open_0._0._0.Actors;
using Open_0._0._0.Core;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.UI
{
    class BasicHealthUI
    {
        private float _width = 1920 * 0.2f;
        private float _height = 1080 * 0.2f;

        public RectangleShape Shape { get; private set; }

        private string _fontpath = @"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf";
        private Font _font;

        private Text _healthText;
        private Text _hungerText;
        private Text _thirstText;
        private Text _timeText;

        private Player _player;

        public BasicHealthUI(Player player)
        {
            _player = player;

            Shape = new RectangleShape();
            Shape.Position = new Vector2f(0, 0);
            Shape.Size = new Vector2f(_width, _height/2);
            Shape.FillColor = CustomColors.BabyBlue;

            _font = new Font(_fontpath);

            CreateTextObjects();
            UpdatePlayerInfo();
        }

        private void CreateTextObjects()
        {
            _healthText = new Text("", _font, 28);
            _healthText.Position = new Vector2f(10, 0);
            _healthText.FillColor = CustomColors.HealthBar;

            _hungerText = new Text("", _font, 28);
            _hungerText.Position = new Vector2f(10, 35);
            _hungerText.FillColor = CustomColors.NESRed;

            _thirstText = new Text("", _font, 28);
            _thirstText.Position = new Vector2f(10, 70);
            _thirstText.FillColor = CustomColors.NESRed;

            _timeText = new Text("", _font, 28);
            _timeText.Position = new Vector2f(10, 105);
            _timeText.FillColor = CustomColors.PlayerColor;
        }

        public void UpdatePlayerInfo()
        {
            _healthText.DisplayedString = $"Health: {_player.Health_Component.Health}/{_player.Health_Component.MaxHealth}";
            _hungerText.DisplayedString = $"Hunger: {_player.DesireManager.Desire.Hunger}/100";
            _thirstText.DisplayedString = $"Thirst: {_player.DesireManager.Desire.Thirst}/100";
        }

        public void Render(RenderWindow window)
        {
            window.SetView(window.DefaultView);
            window.Draw(Shape);
            window.Draw(_healthText);
            window.Draw(_hungerText);
            window.Draw(_thirstText);
            window.Draw(_timeText);
        }
    }
}