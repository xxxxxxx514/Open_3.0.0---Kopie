using Open_0._0._0.World;
namespace Open_0._0._0.Core
{
    class Game
    {
        public bool IsRunning = true;

        private MapDesigner _mapDesigner;


        private int t = 0;

        public Game()
        {
            EngineLoop.GameLoop(this);
        }


        // ---------------------------------------------------------
        // UPDATE (FPS-abhängig)
        // ---------------------------------------------------------
        public void Update(float deltaTime)
        {

        }


        // ---------------------------------------------------------
        // FIXED UPDATE (Physik, 50 Hz)
        // ---------------------------------------------------------
        public void FixedUpdate(float fixedDelta)
        {
        }

        private void RunMapDesign()
        {
        
        }
    }
}