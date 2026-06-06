using Open_0._0._0.Actions;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0
{
    internal class Item
    {
        public RectangleShape Shape { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IAction Action { get; set; }
        public Item(int level, string name, string descrption)
        {
            Shape = new RectangleShape();
            Shape.FillColor = Color.Yellow;
            Shape.Size = new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE);
            Level = level;
            Name = name;
            Description = descrption;
        }
        public Item()
        {
            Shape = new RectangleShape();
            Shape.FillColor = Color.Yellow;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Level {Level}, Description {Description}";
        }
    }
}