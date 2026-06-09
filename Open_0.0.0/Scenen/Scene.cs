using SFML.Graphics;
namespace Open_0._0._0.Scenen
{
    abstract class Scene
    {
        // Wird vom SceneManager gesetzt
        protected RenderWindow Window;
        protected SceneManager SceneManager;

        public Scene(SceneManager sceneManager)
        {
             SceneManager = sceneManager;
        }
        public void SetWindow(RenderWindow window)
        {
            Window = window;
        }

        public abstract void Update(float deltaTime);
        public abstract void FixedUpdate(float fixedDelta);
        public abstract void Render();
    }

}
