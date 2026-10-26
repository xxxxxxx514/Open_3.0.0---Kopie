using Open_0._0._0.Actors;
using Open_0._0._0.Core;
using Open_0._0._0.UI;
using Open_0._0._0.Utils;
namespace Open_0._0._0.Scenes
{
    class DeathScene:Scene
    {
        private BasicDeathUI BasicDeathUI {  get; set; }
        public DeathScene(SceneManager sceneManager) : base(sceneManager)
        {
            BasicDeathUI=new BasicDeathUI(GameWorld.Player);
        }

        public override void Update(float deltaTime)
        {
            OnMouseClick();
        }
        public override void FixedUpdate(float fixedDelta)
        {
        }
        public override void Render()
        {
            BasicDeathUI.Render(Window);
        }
        private void OnMouseClick()
        {
            if(Mouse_Input.LeftPressed||Mouse_Input .RightPressed)
            {
                SceneManager.ChangeScene(new MainMenuScene(SceneManager));

            }
        }
    }
}
