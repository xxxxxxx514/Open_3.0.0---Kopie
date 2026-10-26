using SFML.Graphics;
using SFML.System;
using System.Text;

namespace Open_0._0._0.UI
{
    class DialogueUI
    {
        private RectangleShape _background;
        private RectangleShape _nameBar;

        private Text _nameText;
        private Text _dialogueText;

        private Font _font;

        private float _width = 1920 * 0.6f;
        private float _height = 1080 * 0.25f;

        public DialogueUI()
        {
            _font = new Font(@"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf");

            CreateBackground();
            CreateNameBar();
            CreateTextObjects();
        }

        private void CreateBackground()
        {
            _background = new RectangleShape();
            _background.Size = new Vector2f(_width, _height);
            _background.Position = new Vector2f(1920 * 0.2f, 1080 * 0.65f);

            _background.FillColor = new Color(0, 0, 0, 180); // halbtransparent schwarz
            _background.OutlineColor = new Color(255, 255, 255, 80);
            _background.OutlineThickness = 2f;
        }

        private void CreateNameBar()
        {
            _nameBar = new RectangleShape();
            _nameBar.Size = new Vector2f(_width, 40);
            _nameBar.Position = new Vector2f(_background.Position.X, _background.Position.Y - 40);

            _nameBar.FillColor = new Color(30, 30, 30, 220);
            _nameBar.OutlineColor = new Color(255, 255, 255, 80);
            _nameBar.OutlineThickness = 2f;
        }

        private void CreateTextObjects()
        {
            _nameText = new Text("", _font, 24);
            _nameText.FillColor = Color.White;
            _nameText.Position = new Vector2f(_nameBar.Position.X + 10, _nameBar.Position.Y + 5);

            _dialogueText = new Text("", _font, 22);
            _dialogueText.FillColor = Color.White;
            _dialogueText.Position = new Vector2f(_background.Position.X + 20, _background.Position.Y + 20);
        }

        // Nur Optik – du setzt den Text später selbst
        public void SetDialogue(string speaker, string text)
        {
            _nameText.DisplayedString = speaker;

            string wrapped = WrapText(
                text,
                (uint)(_width - 40),   // Innenabstand
                _font,
                22                     // Schriftgröße
            );

            _dialogueText.DisplayedString = wrapped;
        }

        private string WrapText(string text, uint maxWidth, Font font, uint characterSize)
        {
            string[] words = text.Split(' ');
            StringBuilder result = new StringBuilder();

            string currentLine = "";
            Text measure = new Text("", font, characterSize);

            foreach (string word in words)
            {
                string testLine = currentLine + word + " ";
                measure.DisplayedString = testLine;

                if (measure.GetLocalBounds().Width > maxWidth)
                {
                    result.AppendLine(currentLine);
                    currentLine = word + " ";
                }
                else
                {
                    currentLine = testLine;
                }
            }

            result.Append(currentLine);
            return result.ToString();
        }

        public void Render(RenderWindow window)
        {
            window.SetView(window.DefaultView);

            window.Draw(_background);
            window.Draw(_nameBar);

            window.Draw(_nameText);
            window.Draw(_dialogueText);
        }
    }
}
