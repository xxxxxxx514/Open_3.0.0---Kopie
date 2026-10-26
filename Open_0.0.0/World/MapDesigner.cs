using Open_0._0._0.World;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Open_0._0._0.World
{
    internal class MapDesigner
    {
        private RenderWindow window;

        private int MapSize = 40;
        public Tile[,] Field;

        private Tile SelectedTile;
        private Vector2f cameraOffset = new Vector2f(0, 0);
        private const float CAMERA_SPEED = 10f;

        private Font font;

        private bool sizeChosen = false;
        private bool askForSaveName = false;
        private bool showLoadList = false;

        private List<string> savedFiles = new List<string>();
        private bool reloadListAfterDraw = false;

        private StructureName selectedStructureName = StructureName.Village;

        public MapDesigner()
        {
            window = new RenderWindow(new VideoMode(1920, 1080), "Structure Designer");
            window.Closed += (_, __) => window.Close();

            font = new Font(@"D:\Schule\3 Klasse\Visual Studio\Open_0.0.0\Open_0.0.0\fonts\arial.ttf");
            SelectedTile = new Tile(TileType.Grass);
        }

        public void Run()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new Color(30, 30, 30));

                if (!sizeChosen)
                {
                    DrawSizeSelection();
                    HandleSizeSelection();
                }
                else if (askForSaveName)
                {
                    DrawSaveNameSelection();
                }
                else if (showLoadList)
                {
                    DrawLoadList();
                }
                else
                {
                    Update();
                    RenderField();
                    DrawBlockPanel();
                    RenderCurrentTile();
                }

                window.Display();
            }
        }

        // ---------------- SIZE SELECTION ----------------
        private void DrawSizeSelection()
        {
            Text t = new Text("Strukturgröße wählen:\n\n1 = 25x25\n2 = 40x40\n3 = 60x60",
                font, 32);
            t.Position = new Vector2f(100, 100);
            window.Draw(t);
        }

        private void HandleSizeSelection()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                SetMapSize(25);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                SetMapSize(40);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                SetMapSize(60);
        }

        private void SetMapSize(int size)
        {
            MapSize = size;
            Field = new Tile[size, size];
            CreateStarterRoom();
            sizeChosen = true;
        }

        // ---------------- MAIN UPDATE ----------------
        private void Update()
        {
            UpdateCamera();
            FollowMouse();
            HandleTilePlacement();
        }

        private void UpdateCamera()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                cameraOffset.Y -= CAMERA_SPEED;

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                cameraOffset.Y += CAMERA_SPEED;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                cameraOffset.X -= CAMERA_SPEED;

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                cameraOffset.X += CAMERA_SPEED;
        }

        private void FollowMouse()
        {
            Vector2i mp = Mouse.GetPosition(window);

            SelectedTile.Shape.Position = new Vector2f(
                mp.X + cameraOffset.X - Tile.TILE_SIZE / 2,
                mp.Y + cameraOffset.Y - Tile.TILE_SIZE / 2
            );
        }

        private void HandleTilePlacement()
        {
            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
                return;

            Vector2i mp = Mouse.GetPosition(window);

            int gridX = (int)((mp.X + cameraOffset.X) / Tile.TILE_SIZE);
            int gridY = (int)((mp.Y + cameraOffset.Y) / Tile.TILE_SIZE);

            if (gridX < 0 || gridX >= MapSize || gridY < 0 || gridY >= MapSize)
                return;

            Field[gridX, gridY] = new Tile(SelectedTile.Tiletype);
        }

        // ---------------- STARTER ROOM ----------------
        private void CreateStarterRoom()
        {
            for (int x = 0; x < MapSize; x++)
            {
                for (int y = 0; y < MapSize; y++)
                {
                    Field[x, y] = Tile.CreateTileForDesign(x, y);
                }
            }
        }

        // ---------------- RENDER FIELD ----------------
        private void RenderField()
        {
            for (int x = 0; x < MapSize; x++)
            {
                for (int y = 0; y < MapSize; y++)
                {
                    Tile tile = Field[x, y];

                    tile.Shape.Position = new Vector2f(
                        x * Tile.TILE_SIZE - cameraOffset.X,
                        y * Tile.TILE_SIZE - cameraOffset.Y
                    );

                    window.Draw(tile.Shape);
                }
            }
        }

        // ---------------- BLOCK PANEL RIGHT ----------------
        private void DrawBlockPanel()
        {
            RectangleShape panel = new RectangleShape(new Vector2f(200, 1080));
            panel.FillColor = new Color(50, 50, 50);
            panel.Position = new Vector2f(1720, 0);
            window.Draw(panel);

            DrawButton("Grass", 1730, 20, TileType.Grass);
            DrawButton("Wall", 1730, 80, TileType.Wall);
            DrawButton("Black", 1730, 140, TileType.Black);
            DrawButton("Water", 1730, 200, TileType.Water);
            DrawButton("Rock", 1730, 260, TileType.Rock);
            DrawButton("Sand", 1730, 320, TileType.Sand);
            DrawButton("House", 1730, 380, TileType.House);
            DrawButton("Tree", 1730, 440, TileType.Tree);
            DrawButton("Road", 1730, 500, TileType.Road);
            DrawButton("Fence", 1730, 560, TileType.Fence);

            DrawSaveButton(1730, 640);
            DrawLoadButton(1730, 700);
        }

        private void DrawButton(string text, float x, float y, TileType type)
        {
            RectangleShape btn = new RectangleShape(new Vector2f(160, 40));
            btn.FillColor = new Color(100, 100, 100);
            btn.Position = new Vector2f(x, y);
            window.Draw(btn);

            Text t = new Text(text, font, 20);
            t.Position = new Vector2f(x + 10, y + 10);
            window.Draw(t);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mp = Mouse.GetPosition(window);

                if (mp.X > x && mp.X < x + 160 && mp.Y > y && mp.Y < y + 40)
                {
                    SelectedTile = new Tile(type);
                }
            }
        }

        private void DrawSaveButton(float x, float y)
        {
            RectangleShape btn = new RectangleShape(new Vector2f(160, 40));
            btn.FillColor = new Color(0, 120, 0);
            btn.Position = new Vector2f(x, y);
            window.Draw(btn);

            Text t = new Text("Save", font, 20);
            t.Position = new Vector2f(x + 45, y + 10);
            window.Draw(t);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mp = Mouse.GetPosition(window);

                if (mp.X > x && mp.X < x + 160 && mp.Y > y && mp.Y < y + 40)
                {
                    askForSaveName = true;
                }
            }
        }

        private void DrawLoadButton(float x, float y)
        {
            RectangleShape btn = new RectangleShape(new Vector2f(160, 40));
            btn.FillColor = new Color(0, 0, 120);
            btn.Position = new Vector2f(x, y);
            window.Draw(btn);

            Text t = new Text("Load", font, 20);
            t.Position = new Vector2f(x + 45, y + 10);
            window.Draw(t);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mp = Mouse.GetPosition(window);

                if (mp.X > x && mp.X < x + 160 && mp.Y > y && mp.Y < y + 40)
                {
                    LoadFileList();
                    showLoadList = true;
                }
            }
        }

        // ---------------- SELECTED TILE ----------------
        private void RenderCurrentTile()
        {
            window.Draw(SelectedTile.Shape);
        }

        // ---------------- ENUM SAVE SELECTION ----------------
        private void DrawSaveNameSelection()
        {
            RectangleShape bg = new RectangleShape(new Vector2f(600, 400));
            bg.FillColor = new Color(20, 20, 20, 230);
            bg.Position = new Vector2f(660, 300);
            window.Draw(bg);

            Text title = new Text("Struktur auswählen:", font, 32);
            title.Position = new Vector2f(680, 320);
            window.Draw(title);

            float yOffset = 380;

            foreach (StructureName name in Enum.GetValues(typeof(StructureName)))
            {
                DrawEnumEntry(name, 680, yOffset);
                yOffset += 50;
            }

            Text hint = new Text("ESC = Abbrechen", font, 20);
            hint.Position = new Vector2f(680, 680);
            window.Draw(hint);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                askForSaveName = false;
        }

        private void DrawEnumEntry(StructureName name, float x, float y)
        {
            RectangleShape entry = new RectangleShape(new Vector2f(560, 40));
            entry.FillColor = new Color(60, 60, 60);
            entry.Position = new Vector2f(x, y);
            window.Draw(entry);

            Text t = new Text(name.ToString(), font, 24);
            t.Position = new Vector2f(x + 10, y + 5);
            window.Draw(t);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mp = Mouse.GetPosition(window);

                if (mp.X > x && mp.X < x + 560 && mp.Y > y && mp.Y < y + 40)
                {
                    selectedStructureName = name;
                    SaveStructure(name.ToString());
                    askForSaveName = false;
                }
            }
        }

        // ---------------- SAVE STRUCTURE ----------------
        private void SaveStructure(string name)
        {
            Directory.CreateDirectory("Structures");
            string path = Path.Combine("Structures", $"{name}.txt");

            var data = new List<string>();

            for (int x = 0; x < MapSize; x++)
            {
                for (int y = 0; y < MapSize; y++)
                {
                    data.Add($"{x},{y},{Field[x, y].Tiletype}");
                }
            }

            File.WriteAllLines(path, data);
        }

        // ---------------- LOAD STRUCTURE ----------------
        public void LoadStructure(string name)
        {
            string path = $"Structures/{name}.txt";

            if (!File.Exists(path))
                return;

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                TileType type = Enum.Parse<TileType>(parts[2]);

                Field[x, y] = new Tile(type);
            }
        }

        // ---------------- LOAD FILE LIST UI ----------------
        private void LoadFileList()
        {
            savedFiles.Clear();

            Directory.CreateDirectory("Structures");
            string[] files = Directory.GetFiles("Structures", "*.txt");

            foreach (string file in files)
            {
                savedFiles.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void DrawLoadList()
        {
            RectangleShape bg = new RectangleShape(new Vector2f(600, 600));
            bg.FillColor = new Color(20, 20, 20, 230);
            bg.Position = new Vector2f(660, 200);
            window.Draw(bg);

            Text title = new Text("Gespeicherte Strukturen:", font, 32);
            title.Position = new Vector2f(680, 220);
            window.Draw(title);

            float yOffset = 280;

            foreach (string file in savedFiles)
            {
                DrawLoadEntry(file, 680, yOffset);
                yOffset += 50;
            }

            Text hint = new Text("ESC = Abbrechen", font, 20);
            hint.Position = new Vector2f(680, 780);
            window.Draw(hint);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                showLoadList = false;

            if (reloadListAfterDraw)
            {
                LoadFileList();
                reloadListAfterDraw = false;
            }
        }

        private void DrawLoadEntry(string name, float x, float y)
        {
            RectangleShape entry = new RectangleShape(new Vector2f(560, 40));
            entry.FillColor = new Color(60, 60, 60);
            entry.Position = new Vector2f(x, y);
            window.Draw(entry);

            Text t = new Text(name, font, 24);
            t.Position = new Vector2f(x + 10, y + 5);
            window.Draw(t);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mp = Mouse.GetPosition(window);

                if (mp.X > x && mp.X < x + 500 && mp.Y > y && mp.Y < y + 40)
                {
                    LoadStructure(name);
                    showLoadList = false;
                }
            }

            RectangleShape deleteBtn = new RectangleShape(new Vector2f(50, 40));
            deleteBtn.FillColor = new Color(150, 0, 0);
            deleteBtn.Position = new Vector2f(x + 510, y);
            window.Draw(deleteBtn);

            Text delText = new Text("X", font, 24);
            delText.Position = new Vector2f(x + 525, y + 5);
            window.Draw(delText);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mp = Mouse.GetPosition(window);

                if (mp.X > x + 510 && mp.X < x + 560 && mp.Y > y && mp.Y < y + 40)
                {
                    DeleteStructure(name);
                    reloadListAfterDraw = true;
                }
            }
        }

        private void DeleteStructure(string name)
        {
            string path = $"Structures/{name}.txt";

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
