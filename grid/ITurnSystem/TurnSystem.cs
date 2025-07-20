


using System;

namespace tcg
{
    public class TurnSystem : ITurnSystem
    {

        private IPlayer turnPlayer;
        public IPlayer TurnPlayer => turnPlayer;

        private IPlayer waitingPlayer;
        public IPlayer WaitingPlayer => waitingPlayer;

        private int turnCounter;
        public int TurnCounter => turnCounter;

        private TurnSystem (IPlayer turnPlayer, IPlayer waitingPlayer)
        {
            if (turnPlayer == null) throw new ArgumentNullException(nameof(turnPlayer));
            if (waitingPlayer == null) throw new ArgumentNullException(nameof(waitingPlayer));

            this.turnPlayer = turnPlayer;
            this.waitingPlayer = waitingPlayer;

            turnCounter = 1;
        }

        public static TurnSystem New (IPlayer turnPlayer, IPlayer waitingPlayer)
        {
            return new TurnSystem (turnPlayer, waitingPlayer);
        }

        public GameActionResult IsTurnPlayer (IPlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (player.Id == TurnPlayer.Id) return GameActionResult.Success;

            return GameActionResult.PlayerIsNotTurnPlayer;
        }
        public GameActionResult EndTurn ()
        {
            GameActionResult result = GameActionResult.Success;

            IPlayer temp = turnPlayer;
            turnPlayer = waitingPlayer;
            waitingPlayer = temp;

            turnCounter++;

            return result;
        }
    }
}