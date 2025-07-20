using System.Collections.Generic;

namespace tcg
{
    public interface IMovementBehaviour
    {
        string Name { get; set; }
        int Range { get; set; }
        List<GridPosition> Directions { get; set; }
    }
}