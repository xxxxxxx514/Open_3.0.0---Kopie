using Open_0._0._0.Scenes;
using Open_0._0._0.Utils;
using SFML.System;
namespace Open_0._0._0.Core
{
    static class EngineLoop
    {
        private static float _takt = 1f / 50f;//= 0,02 pro sekunde aufruf also 50mal pro sec
        private static float _buffer = 0f;

        private static float _fpsTimer = 0f;
        private static int _frameCount = 0;

        private static float deltaTime;
        static SceneManager SceneManager = new SceneManager();

        public static void GameLoop(Game game)
        {

            Clock clock = new Clock();

            while (game.IsRunning)
            {
                deltaTime = clock.ElapsedTime.AsSeconds();
                clock.Restart();

                FpsCounter(deltaTime);

                KeyBoard_Input.Update();
                Mouse_Input.Update(SceneManager.Window);

                SceneManager.UpdateScene(deltaTime);
                SceneManager.RenderScene();

                //game.Update(deltaTime);

                _buffer += deltaTime;
                if (_takt <= _buffer)
                {
                    //  game.FixedUpdate(_takt);
                    SceneManager.FixedUpdate(_takt);

                    _buffer -= _takt;
                }
                //if (GameWorld.Player != null) {
                //    View view = new View(new FloatRect(0, 0, 1920, 1080));
                //    view.Center = new Vector2f(GameWorld.Player.Position_Component.PixelPosition.X, GameWorld.Player.Position_Component.PixelPosition.Y);

                //    SceneManager.Window.SetView(view);
                //}

            }
        }
        private static void FpsCounter(float deltaTime)
        {
            _fpsTimer += deltaTime;
            _frameCount++;
            if (_fpsTimer >= 1f)
            {
                float fps = _frameCount / _fpsTimer;
                Console.Title = $"FPS: {fps:F1}";
                _fpsTimer = 0f;
                _frameCount = 0;
            }
        }

    }
}
