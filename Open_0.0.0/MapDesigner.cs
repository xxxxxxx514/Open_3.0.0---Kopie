using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Open_0._0._0
{
    internal class MapDesigner
    {
        private RenderWindow window;
        private const int MAXWIDTHPIXEL = 1920;
        private const int MAXHEIGHTPIXEL = 1080;

        private const int MAXWIDTH = 1920 / Tile.TILE_SIZE;
        private const int MAXHEIGHT = 1080 / Tile.TILE_SIZE;
        public Room Room;

        private Vector2i mousepos { get; set; }

        private Tile SelectedTile;
        private Clock clock;
        public MapDesigner()
        {
            window = new RenderWindow(new VideoMode(1920, 1080), "My Leveldesign");

            clock = new Clock();
            SelectedTile = Tile.CreateTileForDesign(3, 2);

            Room = new Room();
            CreateStarterRoom();

        }
        public Room Run()
        {
            Menu();
            while (window.IsOpen)
            {
                window.DispatchEvents();

                // Input
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    Console.WriteLine("Wie Heißt die Map");
                    Room.SetName(Console.ReadLine());

                    Console.WriteLine("Map wurde zurückgegeben");
                    window.Close();
                    for (int x = 0; x < MAXWIDTH; x++)
                    {
                        for (int y = 0; y < MAXHEIGHT; y++)
                        {
                            if (Room.Field[x, y].Tiletype == TileType.mapdesigndefault)
                            {
                                Room.Field[x, y] = new Tile(x, y, TileType.Grass);
                            }
                        }
                    }
                    return Room;
                }
                // Update
                Update();
                // Render
                window.Clear();

                RenderField(window);

                RenderCurrentTile(window);

                window.Display();

            }
            return Room;
        }
        private void Update()
        {
            Menu();
            FollowMouse(window);
            OnMouseClick();
            Console.WriteLine(mousepos);

        }
        private void CreateStarterRoom()
        {
            SelectedTile.ChangeTilePropertys(TileType.Grass);
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {
                    // Field[x, y] = Tile.CreateTileForDesign(x, y);
                    Room.Field[x, y] = Tile.CreateTileForDesign(x, y);

                }
            }
        }
        private void RenderField(RenderWindow window)
        {
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {
                    window.Draw(Room.Field[x, y].Shape);
                }
            }
        }
        private void FollowMouse(RenderWindow renderWindow)
        {
            ChangeTilePosition(Mouse.GetPosition(renderWindow));
        }
        private void ChangeTilePosition(Vector2i mouseposition)
        {
            SelectedTile.Shape.Position = new Vector2f(mouseposition.X - 20, mouseposition.Y - 25f);
            mousepos = (Vector2i)SelectedTile.Shape.Position;
            Console.WriteLine(SelectedTile.Shape.Position);
        }
        private void RenderCurrentTile(RenderWindow renderWindow)
        {
            renderWindow.Draw(SelectedTile.Shape);
        }
        private Mouse.Button OnMouseClick()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine("Preesed mouse");
                PlaceTile();
                return Mouse.Button.Left;
            }
            else return Mouse.Button.ButtonCount;
        }
        private void Menu()
        {
            Console.WriteLine($"Welchen block möchtest du platzieren\n" +
                $"(1) Grass Block\n" +
                $"(2) Wall Block\n" +
                $"(3) Water Block\n" +
                $"(4) Fence Block\n" +
                $"(5) Sand Block");
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
            {
                SelectedTile = GetTileByNum(1);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
            {
                SelectedTile = GetTileByNum(2);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
            {
                SelectedTile = GetTileByNum(3);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
            {
                SelectedTile = GetTileByNum(4);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
            {
                SelectedTile = GetTileByNum(5);
            }
        }
        private void PlaceTile()
        {

            Vector2i mouseGridPos = Mouse.GetPosition(window);

            int gridX = mouseGridPos.X / Tile.TILE_SIZE;
            int gridY = mouseGridPos.Y / Tile.TILE_SIZE;

            if (gridX < 0 || gridX >= MAXWIDTH || gridY < 0 || gridY >= MAXHEIGHT)
                return;

            if (Room.Field[gridX, gridY].Tiletype == TileType.Corridor)
                return;
            Tile newTile = new Tile(SelectedTile.Tiletype);

            newTile.Shape.Position = new Vector2f(gridX * Tile.TILE_SIZE, gridY * Tile.TILE_SIZE);
            Room.Field[gridX, gridY] = newTile;
        }

        private Tile GetTileByNum(int blocknr)
        {
            switch (blocknr)
            {
                case 1:
                    return new Tile(TileType.Grass);

                case 2:
                    return new Tile(TileType.Wall);

                case 3:
                    return new Tile(TileType.Water);

                case 4:
                    return new Tile(TileType.Fence);

                case 5:
                    return new Tile(TileType.Sand);

                default:
                    Console.WriteLine("Keine gültige eingabe");
                    return new Tile();
            }
        }
        private void TileHower(RenderWindow renderWindow)
        {
            renderWindow.Draw(SelectedTile.Shape);
        }
    }
}