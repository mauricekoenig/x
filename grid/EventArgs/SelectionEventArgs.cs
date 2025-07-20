


using System;
using System.Collections.Generic;

namespace tcg
{
    public class SelectionEventArgs : EventArgs
    {
        public ICard SelectedCard { get; set; }
        public List<GridPosition> ReachablePositions { get; set; }
        public GridPosition Origin { get; set; }
        public static SelectionEventArgs New (ICard card, GridPosition origin, List<GridPosition> reachablePositions)
        {
            return new SelectionEventArgs (card, origin, reachablePositions);
        }
        private SelectionEventArgs (ICard card, GridPosition origin, List<GridPosition> reachablePositions)
        {
            if (reachablePositions == null) throw new ArgumentNullException();
            ReachablePositions = reachablePositions;
            SelectedCard = card;
            Origin = origin;
        }
    }
}