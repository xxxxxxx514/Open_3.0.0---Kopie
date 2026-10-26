using Open_0._0._0.Utils;
using Open_0._0._0.World;
using SFML.System;

namespace Open_0._0._0
{
    internal class PathFinding
    {
        internal class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Node Parent { get; set; }
            public float G { get; set; } // Cost from start to this node
            public Node(int x, int y, Node parent = null)
            {
                X = x;
                Y = y;
                Parent = parent;
                G = 0;
            }
        }

        private Map _map;

        public Vector2i Start { get; set; } = new Vector2i();
        public Vector2i End { get; set; } = new Vector2i();

        public bool ReachedEnd { get; set; } = false;

        public LinkedList<Node> Path = new LinkedList<Node>();

        public PathFinding(Map map)
        {
            _map = map;
        }

        public void CreatingPath(Vector2i start, Vector2i end)
        {
            Start = start;
            End = end;
            Vector2i dir = end - start;

            Vector2i nextnode;
            Vector2i lastnode;
            while (true)
            {
                nextnode = MathHelper.CalculateVectorI(start, dir);
                if (IsWalkable(nextnode))
                {
                    AddPath(new Node(nextnode.X, nextnode.Y));
                    lastnode = start;
                    start = nextnode;
                }
                else
                {
                    nextnode = MathHelper.CalculateVectorI(start, new Vector2i(dir.X, 0));
                }
            }
        }
        private bool IsWalkable(int x, int y)
        {
            return _map.IsTileAtWalkable(x, y);
        }
        private bool IsWalkable(Vector2i position)
        {
            return _map.IsTileAtWalkable(position);
        }

        private void AddPath(Node node)
        {
            if (node == null)
                return;
            Path.AddFirst(node);
        }
    }
}
