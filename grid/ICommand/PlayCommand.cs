




namespace tcg
{
    public class PlayCommand : CommandBase
    {
        public ICard Card { get; set; }
        GridPosition Position { get; set; }

        public PlayCommand (ICard card, GridPosition spawnPosition)
        {
            Card = card;
            Position = spawnPosition;
            requiresTurnPlayer = true;
        }

        public override GameActionResult Execute(ICommandContext context)
        {
            var result = context.State.Grid.PlaceCard(Card, Position);
            if (result != GameActionResult.Success) return result;

            PlayCardEventArgs args = PlayCardEventArgs.New(Card, Position);
            context.Events.OnPlayed(args);
            return result;
        }
    }
}