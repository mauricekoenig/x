

using System;
using System.Runtime.InteropServices;

namespace tcg
{

    public struct GridPosition
    {
        public int x { get; set; }
        public int y { get; set;  }

        public static readonly GridPosition Up = GridPosition.New(0, 1);
        public static readonly GridPosition Down = GridPosition.New(0, -1);
        public static readonly GridPosition Left = GridPosition.New(-1, 0);
        public static readonly GridPosition Right = GridPosition.New(1, 0);
        public static readonly GridPosition Zero = GridPosition.New(0, 0);


        private GridPosition (int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static GridPosition operator +(GridPosition a, GridPosition b) => new GridPosition(a.x + b.x, a.y + b.y);
        public static GridPosition operator -(GridPosition a, GridPosition b) => new GridPosition(a.x - b.x, a.y - b.y);

        public override string ToString() => $"({x}, {y})";

        public override bool Equals(object obj) => obj is GridPosition p && x == p.x && y == p.y;
        public override int GetHashCode() => HashCode.Combine(x, y);

        public static bool operator ==(GridPosition a, GridPosition b) => a.Equals(b);
        public static bool operator !=(GridPosition a, GridPosition b) => !a.Equals(b);

        public static GridPosition New (int x, int y)
        {
            return new GridPosition(x, y);
        }
    }
}