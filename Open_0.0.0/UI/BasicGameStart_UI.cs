using Open_0._0._0.Scenes;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
namespace Open_0._0._0.UI
{
     class BasicGameStart_UI
    {
        private float _width = 1920 * 0.4f;
        private float _height = 1080 * 0.10f;
        public bool toggle = true;

        private float _poswidth = 1920 / 5f + 1080 / 7.6f;
        public RectangleShape StartButton { get; private set; }
        public RectangleShape MapDesignButton { get; private set; }
        public RectangleShape SettingsButton { get; private set; }
        private string _fontpath = @"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf";
        private Text Start;
        private Text Mapdesign;
        private Text Setting;

        private Font _font;
        public BasicGameStart_UI()
        {

            // Hintergrund-Rechteck
            StartButton = new RectangleShape();
            StartButton.Position = new Vector2f(_poswidth, 200);
            StartButton.Size = new Vector2f(_width, _height);
            StartButton.FillColor = CustomColors.BabyBlue;
            StartButton.OutlineColor = CustomColors.Mint;
            StartButton.OutlineThickness = 3;


            MapDesignButton = new RectangleShape();
            MapDesignButton.Position = new Vector2f(_poswidth, 300);
            MapDesignButton.Size = new Vector2f(_width, _height);
            MapDesignButton.FillColor = CustomColors.BabyBlue;
            MapDesignButton.OutlineColor = CustomColors.Mint;
            MapDesignButton.OutlineThickness = 3;



            SettingsButton = new RectangleShape();
            SettingsButton.Position = new Vector2f(_poswidth, 400);
            SettingsButton.Size = new Vector2f(_width, _height);
            SettingsButton.FillColor = CustomColors.BabyBlue;
            SettingsButton.OutlineColor = CustomColors.Mint;
            SettingsButton.OutlineThickness = 3;

            _font = new Font(_fontpath);

            // Text-Objekte erstellen
            CreateTextObjects();
        }
        private void CreateTextObjects()
        {
            float startX = StartButton.Position.X + 40;
            float startY = StartButton.Position.Y + 40;

            // Health
            Start = new Text("Start Game", _font, 60);
            Start.Position = StartButton.Position;
            Start.FillColor = CustomColors.HealthBar;

            Mapdesign = new Text("Design Map", _font, 60);
            Mapdesign.Position = MapDesignButton.Position;
            Mapdesign.FillColor = CustomColors.HealthBar;

            Setting = new Text("Settings", _font, 60);
            Setting.Position = SettingsButton.Position;
            Setting.FillColor = CustomColors.HealthBar;

        }
        private void Renderr(RenderWindow window)
        {
            // Erst das Hintergrund-Rechteck

            if (toggle)
            {
                window.Draw(StartButton);
                window.Draw(MapDesignButton);
                window.Draw(SettingsButton);

                // Dann alle Texte
                window.Draw(Start);
                window.Draw(Mapdesign);
                window.Draw(Setting);
            }

        }
        public void Render(RenderWindow window)
        {
            if (toggle)
            {
                window.Draw(StartButton);
                window.Draw(MapDesignButton);
                window.Draw(SettingsButton);

                // Dann alle Texte
                window.Draw(Start);
                window.Draw(Mapdesign);
                window.Draw(Setting);
            }
        }
    }
}