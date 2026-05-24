using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    internal class Room
    {
        public string Name { get; private set; }

        public const int MAXWIDTHPIXEL = 1920;
        public const int MAXHEIGHTPIXEL = 1080;

        public const int MAXWIDTH = 1920 / Tile.TILE_SIZE;
        public const int MAXHEIGHT = 1080 / Tile.TILE_SIZE;
        public Tile[,] Field;
        public Room()
        {
            Field = new Tile[MAXWIDTH, MAXHEIGHT];
        }
        public Room(Tile[,] field)
        {
            Field = field;
        }
        public void SetName(string name)
        {
            if (Name != null || Name != string.Empty)
            {
                Name = name;
            }
        }
        public Room CreateRoom2()
        {
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {
                    // Field[x, y] = new Tile(x, y, TileType.Black);
                    if ((x - y) % 4 == 0)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Grass);
                    }
                    else
                    {
                        Field[x, y] = new Tile(x, y, TileType.Black);
                    }
                    if (x < 30 && x > 20 && y > 10 && y < 20)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Grass);

                    }
                    if (x == 2 && y == MAXHEIGHT / 2)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Corridor);
                    }
                    if (x == MAXWIDTH - 2 && y == MAXHEIGHT / 2)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Corridor);
                    }
                    if (x == MAXWIDTH / 2 && y == 2)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Corridor);
                    }
                    if (x == MAXWIDTH / 2 && y == MAXHEIGHT - 2)
                    {
                        Field[x, y] = new Tile(x, y, TileType.Corridor);
                    }
                }
            }
            return this;
        }

        public void RenderRoom(RenderWindow window)
        {
            for (int x = 0; x < MAXWIDTH; x++)
            {
                for (int y = 0; y < MAXHEIGHT; y++)
                {
                    window.Draw(Field[x, y].Shape);
                }
            }
        }
    }
}