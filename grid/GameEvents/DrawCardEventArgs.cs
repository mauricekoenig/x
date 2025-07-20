using System;

namespace tcg
{
    public class DrawCardEventArgs : EventArgs
    {
        public IPlayer Player { get; set; }
        public ICard Card { get; set; }

        public static DrawCardEventArgs New (IPlayer player, ICard card)
        {
            return new DrawCardEventArgs (player, card);
        }
        private DrawCardEventArgs (IPlayer player, ICard card)
        {
            if (player == null) throw new ArgumentNullException(nameof (player));
            if (card == null) throw new ArgumentNullException(nameof (card));

            Player = player;
            Card = card;
        }
    }
}