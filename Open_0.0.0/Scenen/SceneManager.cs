using SFML.Graphics;
using SFML.Window;
namespace Open_0._0._0.Scenen
{
    class SceneManager
    {
        public RenderWindow Window = new RenderWindow(new VideoMode(1920, 1080), "My Game");
        private GameScene _cachedGameScene;
        public Scene currentScene;

        public SceneManager ()
        {
            currentScene = new MainMenueScene(this);
            currentScene.SetWindow(Window);
            Window.Closed += (_, __) => Window.Close();


        }
        public void ChangeScene(Scene scene)
        {
            currentScene=scene;
            currentScene.SetWindow(Window);
        }
        public void ChangeToGameScene()
        {
            if (_cachedGameScene == null)
            {
                _cachedGameScene = new GameScene(this);
                _cachedGameScene.SetWindow(Window);
            }

            currentScene = _cachedGameScene;
        }
        public void SetGameSceneToNull() => _cachedGameScene = null;
        public void UpdateScene(float deltatime)
        {
            Window.DispatchEvents();
            currentScene.Update(deltatime);
            Window.Display();
        }
        public void FixedUpdate(float fixedDelta)
        {
            currentScene.FixedUpdate(fixedDelta);
        }
        public void RenderScene() 
        {
            currentScene.Render();
        }
    }
}
