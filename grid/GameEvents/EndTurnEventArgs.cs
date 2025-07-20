using System;

namespace tcg
{
    public class EndTurnEventArgs : EventArgs
    {
        public int Turn { get; set; }
        public IPlayer Requester { get; set; }

        private EndTurnEventArgs (IPlayer requester, int turn)
        {
            if (requester == null) throw new ArgumentNullException(nameof(requester));

            Requester = requester;
            Turn = turn;
        }

        public static EndTurnEventArgs New (IPlayer requester, int turn)
        {
            return new EndTurnEventArgs (requester, turn);
        }
    }
}