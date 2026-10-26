using Open_0._0._0.World;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    class Chunk
    {
        public readonly int ChunkX;
        public readonly int ChunkY;

        public const int Size = 16;
        public Tile[,] Field;

        private VertexArray _vertexArray;

        public Chunk(int chunkX, int chunkY, int size = 16)
        {
            ChunkX = chunkX;
            ChunkY = chunkY;
            Field = new Tile[size, size];
        }

        public Chunk(int chunkX, int chunkY, Tile[,] field, int size = 16)
        {
            ChunkX = chunkX;
            ChunkY = chunkY;
            Field = field;
        }

        public void BuildVertexArray()
        {
            _vertexArray = new VertexArray(PrimitiveType.Quads, (uint)(Size * Size * 4));

            uint index = 0;
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {
                    var tile = Field[x, y];
                    var color = tile.Shape.FillColor;

                    float px = tile.Shape.Position.X;
                    float py = tile.Shape.Position.Y;
                    float s = Tile.TILE_SIZE;

                    _vertexArray[index++] = new Vertex(new Vector2f(px, py), color);
                    _vertexArray[index++] = new Vertex(new Vector2f(px + s, py), color);
                    _vertexArray[index++] = new Vertex(new Vector2f(px + s, py + s), color);
                    _vertexArray[index++] = new Vertex(new Vector2f(px, py + s), color);
                }
        }

        public void Render(RenderWindow renderWindow)
        {
            renderWindow.Draw(_vertexArray);
        }

        public Tile[] ToTileArray()
        {
            Tile[] arr = new Tile[Size * Size];
            int index = 0;

            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                    arr[index++] = Field[x, y];

            return arr;
        }
    }
}