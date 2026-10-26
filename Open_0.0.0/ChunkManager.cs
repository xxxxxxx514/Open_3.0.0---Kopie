using Open_0._0._0;
using Open_0._0._0.Core;
using Open_0._0._0.World;
using SFML.Graphics;
using SFML.System;

class ChunkManager
{
    public static Dictionary<Vector2i, Chunk> Chunks = new();

    public ChunkManager()
    {
        FillChunks();
    }

    private void FillChunks()
    {
        // Die Tile-Map holen
        var field = GameWorld.CurrentMap.Field;

        int mapWidth = field.GetLength(0);
        int mapHeight = field.GetLength(1);

        int chunkCountX = mapWidth / Chunk.Size;
        int chunkCountY = mapHeight / Chunk.Size;

        for (int cx = 0; cx < chunkCountX; cx++)
        {
            for (int cy = 0; cy < chunkCountY; cy++)
            {
                Chunk chunk = new Chunk(cx, cy);

                int startX = cx * Chunk.Size;
                int startY = cy * Chunk.Size;

                for (int x = 0; x < Chunk.Size; x++)
                {
                    for (int y = 0; y < Chunk.Size; y++)
                    {
                        Tile tile = field[startX + x, startY + y];

                        // Pixel-Position setzen
                        tile.Shape.Position = new Vector2f(
                            (startX + x) * Tile.TILE_SIZE,
                            (startY + y) * Tile.TILE_SIZE
                        );

                        chunk.Field[x, y] = tile;
                    }
                }

                Chunks[new Vector2i(cx, cy)] = chunk;
            }
        }
    }

    public void Render(RenderWindow renderWindow, Vector2f campos)
    {
        Vector2i camTile = PixelToTile(campos);
        Vector2i camChunk = TileToChunk(camTile);

        for (int x = -2; x <= 2; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2i key = new Vector2i(camChunk.X + x, camChunk.Y + y);

                if (Chunks.TryGetValue(key, out Chunk chunk))
                    chunk.Render(renderWindow);
            }
        }
    }

    public static Chunk GetChunk()
    {
        Vector2i pos = TileToChunk(GameWorld.Player.Position_Component.GridPosition);
        return Chunks[pos];
    }

    public static Vector2i PixelToTile(Vector2f pixel)
    {
        return new Vector2i(
            (int)(pixel.X / Tile.TILE_SIZE),
            (int)(pixel.Y / Tile.TILE_SIZE)
        );
    }

    public static Vector2i TileToChunk(Vector2i tilePos)
    {
        return new Vector2i(
            tilePos.X / Chunk.Size,
            tilePos.Y / Chunk.Size
        );
    }
}
