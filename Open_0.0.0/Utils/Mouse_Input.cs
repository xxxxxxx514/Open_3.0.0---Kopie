using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Open_0._0._0.Utils
{
    public static class Mouse_Input
    {
        public static bool LeftPressed { get; private set; }
        public static bool LeftReleased { get; private set; }
        public static bool LeftHeld { get; private set; }

        public static bool RightPressed { get; private set; }
        public static bool RightReleased { get; private set; }
        public static bool RightHeld { get; private set; }

        public static Vector2i MousePosition { get; private set; }
        public static int ScrollDelta { get; private set; }

        private static bool prevLeftHeld;
        private static bool prevRightHeld;

        public static void Update(RenderWindow window)
        {
            MousePosition = Mouse.GetPosition(window);

            LeftHeld = Mouse.IsButtonPressed(Mouse.Button.Left);
            RightHeld = Mouse.IsButtonPressed(Mouse.Button.Right);

            LeftPressed = LeftHeld && !prevLeftHeld;
            LeftReleased = !LeftHeld && prevLeftHeld;

            RightPressed = RightHeld && !prevRightHeld;
            RightReleased = !RightHeld && prevRightHeld;

            ScrollDelta = 0;

            prevLeftHeld = LeftHeld;
            prevRightHeld = RightHeld;
        }

        public static void OnWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            ScrollDelta = (int)e.Delta;
        }
    }

}
