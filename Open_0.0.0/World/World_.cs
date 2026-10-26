using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.World
{
    internal class World_
    {
        private float _unloadTimer = 0f;
        private const float _unloadInterval = 2f;
        private int _lastCamChunkX = 0;
        private int _lastCamChunkY = 0;

        public Dictionary<Vector2i, Chunk> Chunks { get; private set; } = new();
        public int TotalChunksX { get; } = 200;
        public int TotalChunksY { get; } = 200;

        public World_() { }

        public void Update(float dt)
        {
            _unloadTimer += dt;
            if (_unloadTimer >= _unloadInterval)
            {
                UnloadDistantChunks(_lastCamChunkX, _lastCamChunkY);
                _unloadTimer = 0f;
            }
        }

        public bool IsTileAtWalkable(int gridX, int gridY)
        {
            int chunkX = gridX / Chunk.Size;
            int chunkY = gridY / Chunk.Size;
            int localX = gridX % Chunk.Size;
            int localY = gridY % Chunk.Size;

            var key = new Vector2i(chunkX, chunkY);
            if (!Chunks.TryGetValue(key, out Chunk chunk)) return false;

            return chunk.Field[localX, localY].isWalkable;
        }

        public bool DoesTileExist(int gridX, int gridY)
        {
            int chunkX = gridX / Chunk.Size;
            int chunkY = gridY / Chunk.Size;
            return Chunks.ContainsKey(new Vector2i(chunkX, chunkY));
        }

        public Tile GetTileAt(int gridX, int gridY)
        {
            int chunkX = gridX / Chunk.Size;
            int chunkY = gridY / Chunk.Size;
            int localX = gridX % Chunk.Size;
            int localY = gridY % Chunk.Size;

            var key = new Vector2i(chunkX, chunkY);
            if (!Chunks.TryGetValue(key, out Chunk chunk)) return null;

            return chunk.Field[localX, localY];
        }

        public Vector2i WorldToTile(Vector2f pixelPos)
        {
            return new Vector2i(
                (int)pixelPos.X / Tile.TILE_SIZE,
                (int)pixelPos.Y / Tile.TILE_SIZE
            );
        }

        public Vector2f TileToWorld(Vector2i gridPos)
        {
            return new Vector2f(
                gridPos.X * Tile.TILE_SIZE,
                gridPos.Y * Tile.TILE_SIZE
            );
        }

        public void Render(RenderWindow window, Vector2f camPos)
        {
            int camChunkX = (int)camPos.X / Tile.TILE_SIZE / Chunk.Size;
            int camChunkY = (int)camPos.Y / Tile.TILE_SIZE / Chunk.Size;

            _lastCamChunkX = camChunkX;
            _lastCamChunkY = camChunkY;

            for (int x = -2; x <= 2; x++)
                for (int y = -2; y <= 2; y++)
                {
                    var key = new Vector2i(camChunkX + x, camChunkY + y);

                    if (!Chunks.ContainsKey(key))
                        LoadChunk(key.X, key.Y);

                    if (Chunks.TryGetValue(key, out Chunk chunk))
                        chunk.Render(window);
                }
        }

        private void LoadChunk(int cx, int cy)
        {
            if (cx < 0 || cy < 0 || cx >= TotalChunksX || cy >= TotalChunksY)
                return;

            var tiles = new Tile[Chunk.Size, Chunk.Size];

            for (int x = 0; x < Chunk.Size; x++)
                for (int y = 0; y < Chunk.Size; y++)
                {
                    int gridX = cx * Chunk.Size + x;
                    int gridY = cy * Chunk.Size + y;

                    var tile = new Tile(gridX, gridY, TileType.Grass);
                    tile.Shape.Position = new Vector2f(
                        gridX * Tile.TILE_SIZE,
                        gridY * Tile.TILE_SIZE
                    );
                    tiles[x, y] = tile;
                }

            var chunk = new Chunk(cx, cy, tiles);
            chunk.BuildVertexArray(); // ← einmal beim Laden
            Chunks[new Vector2i(cx, cy)] = chunk;
        }

        private void UnloadDistantChunks(int camChunkX, int camChunkY)
        {
            foreach (var key in Chunks.Keys.ToList())
            {
                int dx = Math.Abs(key.X - camChunkX);
                int dy = Math.Abs(key.Y - camChunkY);

                if (dx > 4 || dy > 4)
                    Chunks.Remove(key);
            }
        }
    }
}