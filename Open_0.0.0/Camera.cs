using Open_0._0._0.Core;
using Open_0._0._0.World;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    class Camera
    {
        private RenderWindow RenderWindow;
        View view = new View(new FloatRect(0, 0, 1920, 1080));
        public Vector2f lastvalidposition;

        public Camera(RenderWindow renderWindow)
        {
            lastvalidposition = GameWorld.Player.Position_Component.PixelPosition;
            RenderWindow = renderWindow;
        }
        public void Update()
        {
            CheckCameraBounds();
            RenderWindow.SetView(view);
        }


        public bool CheckCameraBounds()
        {
            bool lockX = false;
            bool lockY = false;

            var pixel = GameWorld.Player.Position_Component.PixelPosition;

            // Kamera-Ränder in Pixeln
            float camLeft = pixel.X - view.Size.X / 2 - 32;
            float camRight = pixel.X + view.Size.X / 2+32;
            float camTop = pixel.Y - view.Size.Y / 2-32;
            float camBottom = pixel.Y + view.Size.Y / 2+32;

            // X prüfen
            if (!MapContainsPixel(camLeft, pixel.Y)) lockX = true;
            if (!MapContainsPixel(camRight, pixel.Y)) lockX = true;

            // Y prüfen
            if (!MapContainsPixel(pixel.X, camTop)) lockY = true;
            if (!MapContainsPixel(pixel.X, camBottom)) lockY = true;

            // Neue Kamera-Position vorbereiten
            Vector2f newCenter = pixel;

            if (lockX)
                newCenter.X = lastvalidposition.X;

            if (lockY)
                newCenter.Y = lastvalidposition.Y;

            // Nur speichern, wenn mindestens eine Achse frei ist
            if (!lockX || !lockY)
                lastvalidposition = newCenter;

            view.Center = newCenter;
            return true;
        }

        private bool MapContainsPixel(float px, float py)
        {
            // Pixel → Tile umrechnen
            int tileX = (int)(px / Tile.TILE_SIZE);
            int tileY = (int)(py / Tile.TILE_SIZE);

            return GameWorld.World.DoesTileExist(tileX, tileY);
        }
    }
}