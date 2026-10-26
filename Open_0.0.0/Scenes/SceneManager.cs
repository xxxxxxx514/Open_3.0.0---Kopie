using Open_0._0._0.Actors;
using Open_0._0._0.Core;
using SFML.Graphics;
using SFML.Window;
namespace Open_0._0._0.Scenes
{
    class SceneManager
    {
        //public RenderWindow Window = new RenderWindow (VideoMode.FullscreenModes[0],
        //                                                "My Game",
        //                                                Styles.Fullscreen
        //                                              );
        public RenderWindow Window = new RenderWindow(new VideoMode(1920, 1080), "My Game");
        public Camera Camara;

        private GameScene _cachedGameScene;
        public Scene currentScene;

        public SceneManager()
        {
            currentScene = new MainMenuScene(this);
            //currentScene = new DialogueScene(this);

            currentScene.SetWindow(Window);

            Window.Closed += (_, __) => Window.Close();

        }
        public void RegisteNpc(NPC nPC)
        {
                    nPC.OnInteract += ChangeToGameScene;
        }
        public void ChangeToGameScene(NPC nPC)
        {
                currentScene = new DialogueScene(this);
                currentScene.SetWindow(Window);
        }
        public void ChangeScene(Scene scene)
        {
            currentScene = scene;
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
