


using System;
using System.Collections.Generic;

namespace tcg
{
    public class PlayCardEventArgs : EventArgs
    {
        public ICard Card { get; set; }
        public GridPosition Position { get; set; }
        public static PlayCardEventArgs New (ICard card, GridPosition position)
        {
            return new PlayCardEventArgs(card, position);
        }
        private PlayCardEventArgs(ICard card, GridPosition position)
        {
            if (card == null || position == null) throw new ArgumentNullException();

            Card = card;
            Position = position;
        }
    }
}