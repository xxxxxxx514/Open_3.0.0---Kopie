using SFML.System;

namespace Open_0._0._0
{
    internal class WorldItem : GameObject
    {
        public Collison2D Collison2D { get; set; }
        public Item Item { get; set; }
        public Vector2f Position { get; set; }
        public string DroppedBy = "";

        public WorldItem(Item item, Vector2f position, string dropedBy = "")
        {
            Item = item;
            Position = position;
            DroppedBy = dropedBy;
        }
        public WorldItem() { }
    }
}