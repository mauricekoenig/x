


using System.Collections.Generic;

namespace tcg
{
    public abstract class BaseMovementBehaviour : IMovementBehaviour
    {
        public string Name { get; set; }
        public int Range { get; set; }
        public List<GridPosition> Directions { get; set; }

        public BaseMovementBehaviour (int range)
        {
            Range = range;
            Directions = new List<GridPosition>();
        }
    }
}