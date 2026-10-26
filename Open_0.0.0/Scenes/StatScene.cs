using Open_0._0._0.Core;
using Open_0._0._0.UI;
using Open_0._0._0.Utils;
using SFML.Window;


namespace Open_0._0._0.Scenes
{
    class StatScene : Scene
    {
        StatWindowUI StatWindowUI { get; set; }
        public StatScene(SceneManager sceneManager) : base(sceneManager)
        {
            StatWindowUI = new StatWindowUI(GameWorld.Player);
            StatWindowUI.toggle = true;

            KeyBoard_Input.OnKeyPressed += OnEscClick;
        }
        public override void Update(float deltaTime)
        {
        }
        public override void FixedUpdate(float fixedDelta)
        {
        }
        public override void Render()
        {
            StatWindowUI.Render(Window);
        }
        private void OnEscClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Escape)
            {
                Console.WriteLine("Pressed Escape zu main game von Stat");
                StatWindowUI.toggle = !StatWindowUI.toggle;
                SceneManager.ChangeToGameScene();
                KeyBoard_Input.OnKeyPressed -= OnEscClick;

            }
        }
    }
}
