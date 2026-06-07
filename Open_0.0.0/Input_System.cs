using SFML.Window;

namespace Open_0._0._0
{
    static class Input_System
    {
        // Event: Wird ausgelöst, wenn eine Taste *neu gedrückt* wird
        public static event Action<Keyboard.Key> OnKeyPressed;

        // Event: Wird ausgelöst, wenn eine Taste *losgelassen* wird
        public static event Action<Keyboard.Key> OnKeyReleased;

        // Speichert alle Tasten, die aktuell gedrückt sind
        // → Damit können wir unterscheiden zwischen:
        //    - neu gedrückt (KeyDown)
        //    - gehalten (kein Event)
        //    - losgelassen (KeyUp)
        private static HashSet<Keyboard.Key> pressed = new();

        public static void Update()
        {
            foreach (Keyboard.Key key in Enum.GetValues(typeof(Keyboard.Key)))
            {
                bool keyPressed = Keyboard.IsKeyPressed(key);

                if (pressed.Contains(key) && keyPressed == false)
                {
                    pressed.Remove(key);
                    OnKeyReleased?.Invoke(key);
                }
                if (keyPressed == true && !pressed.Contains(key))
                {
                    pressed.Add(key);
                    OnKeyPressed?.Invoke(key);

                }
            }
        }
    }
}
