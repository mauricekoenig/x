using System;
using System.Collections.Generic;
using System.Text;

namespace tcg
{
    public  class Movement_Diagonal : BaseMovementBehaviour {

        public Movement_Diagonal (int range) : base (range)
        {
            Directions.Add(GridPosition.New(1, 1));
            Directions.Add(GridPosition.New(1, -1));
            Directions.Add(GridPosition.New(-1, -1));
            Directions.Add(GridPosition.New(-1, 1));

            this.Name = "Diagonal Movement";
        }
    }
}
