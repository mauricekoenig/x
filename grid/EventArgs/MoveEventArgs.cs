


using System;

namespace tcg
{
    public class MoveEventArgs : EventArgs
    {
        public GridPosition Destination { get; set; }
        public Unit ObjectToMove { get; set; }

        private MoveEventArgs (Unit objectToMove, GridPosition toPosition)
        {
            Destination = toPosition;
            ObjectToMove = objectToMove;
        }

        public static MoveEventArgs New (Unit objectToMove, GridPosition toPosition)
        {
            return new MoveEventArgs (objectToMove, toPosition);
        }
    }
}