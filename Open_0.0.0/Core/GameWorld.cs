using Open_0._0._0.Actors;
using Open_0._0._0.Items;
using Open_0._0._0.World;

namespace Open_0._0._0.Core
{
    static class GameWorld
    {
        public static Player Player { get; set; }
        public static Map CurrentMap { get; set; }
        public static List<WorldItem> WorldItems { get; set; } = new();
        public static List<Actor> Entities { get; set; } = new();
        public static TimeSystem TimeSystem { get; set; } = new();
        public static World_ World { get; set; } = new World_();

        // Optional: Hilfsmethoden
        public static void Clear()
        {
            Player = null;
            CurrentMap = null;
            Entities.Clear();
            WorldItems.Clear();
        }
    }
}
