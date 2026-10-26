using Open_0._0._0.UI;
using SFML.Window;
using SFML.System;
using Open_0._0._0.Utils;

namespace Open_0._0._0.Scenes
{
    class MainMenuScene : Scene
    {
        public BasicGameStart_UI BasicGameStart_UI { get; set; }

        public MainMenuScene(SceneManager sceneManager) : base(sceneManager)
        {
            BasicGameStart_UI = new BasicGameStart_UI();
        }
        public override void Update(float deltaTime)
        {
            OnStartClick();
            OnMapDesignClick();
            OnSettingClick();
        }
        public override void FixedUpdate(float fixedDelta)
        {
        }
        public override void Render()
        {
            BasicGameStart_UI.Render(Window);
        }
        private void OnStartClick()
        {
            if (!BasicGameStart_UI.toggle) return;
            if (!Mouse_Input.LeftPressed) return;

            Vector2i mousePos = Mouse.GetPosition(SceneManager.Window);

            if (BasicGameStart_UI.StartButton.GetGlobalBounds().Contains(mousePos))
            {
                BasicGameStart_UI.toggle = false;
                Console.WriteLine("Clicked StartButton");

               SceneManager.ChangeToGameScene();
            }
        }
        private void OnMapDesignClick()
        {
            if (!BasicGameStart_UI.toggle) return;
            if (!Mouse_Input.LeftPressed) return;

            Vector2i mousePos = Mouse.GetPosition(SceneManager.Window);

            if (BasicGameStart_UI.MapDesignButton.GetGlobalBounds().Contains(mousePos))
            {
                BasicGameStart_UI.toggle = false;
                //      RunMapDesign();
                Console.WriteLine("Clicked MapDesigner");

            }

        }
        private void OnSettingClick()
        {
            if (!BasicGameStart_UI.toggle) return;
            if (!Mouse_Input.LeftPressed) return;

            Vector2i mousePos = Mouse.GetPosition(SceneManager.Window);

            if (BasicGameStart_UI.MapDesignButton.GetGlobalBounds().Contains(mousePos))
            {
                    Console.WriteLine("Settings wurde geklickt");
            }
        }
    }
}