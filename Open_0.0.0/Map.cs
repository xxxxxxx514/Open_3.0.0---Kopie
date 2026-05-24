using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    internal class Map
    {
        public const int MAXWIDTHPIXEL = 1920;
        public const int MAXHEIGHTPIXEL = 1080;
        public const int MAXWIDTH = 1920 / Tile.TILE_SIZE;
        public const int MAXHEIGHT = 1080 / Tile.TILE_SIZE;

        public Tile[,] Field { get; private set; }
        public string Name { get; set; } = "DefaultMap";

        public Map()
        {
            Field = new Tile[MAXWIDTH, MAXHEIGHT];
            CreateDefaultMap();
        }

        public void CreateDefaultMap()
        {
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {
                    // Rand = Wand
                    if (x == 0 || x == MAXWIDTH - 1 || y == 0 || y == MAXHEIGHT - 1)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Wall);
                    }
                    else
                    {
                        // Schachbrettmuster für Abwechslung
                        if ((x + y) % 2 == 0)
                            Field[x, y] = new Tile(x, y, TileType.Grass);
                        else
                            Field[x, y] = new Tile(x, y, TileType.Sand);
                    }
                }
            }
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {

                    Field[x, y] = new Tile(x, y, TileType.Sand);
                }
            }
        }

        public bool IsTileAtWalkable(int x, int y)
        {
            if (x < 0 || x >= MAXWIDTH || y < 0 || y >= MAXHEIGHT)
                return false;
            return Field[x, y].isWalkable;
        }

        public bool IsTileAtWalkable(Vector2i gridPos)
        {
            return IsTileAtWalkable(gridPos.X, gridPos.Y);
        }

        public Tile GetTileAt(int x, int y)
        {
            if (x >= 0 && x < MAXWIDTH && y >= 0 && y < MAXHEIGHT)
                return Field[x, y];
            return null;
        }

        public Tile GetTileAt(Vector2i gridPos)
        {
            return GetTileAt(gridPos.X, gridPos.Y);
        }

        public Vector2i WorldToTile(Vector2f pixelPos)
        {
            return new Vector2i((int)pixelPos.X / Tile.TILE_SIZE, (int)pixelPos.Y / Tile.TILE_SIZE);
        }

        public void RenderMap(RenderWindow window)
        {
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {
                    window.Draw(Field[x, y].Shape);
                }
            }
        }

        public void Save(string filepath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                writer.Write(MAXWIDTH);
                writer.Write(MAXHEIGHT);

                for (int x = 0; x < MAXWIDTH; x++)
                {
                    for (int y = 0; y < MAXHEIGHT; y++)
                    {
                        writer.Write((byte)Field[x, y].Tiletype);
                    }
                }
            }
        }

        public void Load(string filepath)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(filepath)))
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();

                for (int x = 0; x < width && x < MAXWIDTH; x++)
                {
                    for (int y = 0; y < height && y < MAXHEIGHT; y++)
                    {
                        TileType type = (TileType)reader.ReadByte();
                        Field[x, y] = new Tile(x, y, type);
                    }
                }
            }
        }
    }
}