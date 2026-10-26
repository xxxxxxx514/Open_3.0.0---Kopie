using Open_0._0._0.Core;
using Open_0._0._0.UI;
using Open_0._0._0.Utils;
using SFML.Window;

namespace Open_0._0._0.Scenes
{
    class DialogueScene:Scene
    {
        DialogueUI DialogueUI { get; set; }
        public DialogueScene(SceneManager sceneManager) : base(sceneManager)
        {
            DialogueUI = new DialogueUI();

            DialogueUI.SetDialogue(
                "NPC",
                "Welcome, traveler. I have a task for you. "
                + "These lands have grown dangerous, and the villagers are afraid to leave their homes. "
                + "Strange creatures have been seen near the old ruins, and we fear something ancient has awakened. "
                + "If you are willing, I need you to investigate the area and report back to me. "
                + "Be careful — the path ahead is not as peaceful as it once was."
            );

            KeyBoard_Input.OnKeyPressed += OnSpaceClick;
        }
        public override void Update(float deltaTime)
        {
        }
        public override void FixedUpdate(float fixedDelta)
        {
        }
        public override void Render()
        {
            DialogueUI.Render(Window);
        }
        private void OnSpaceClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Space)
            {
                Console.WriteLine("Pressed Space zu main game von Stat");
                SceneManager.ChangeToGameScene();
                KeyBoard_Input.OnKeyPressed -= OnSpaceClick;

            }
        }
    }
}
