


namespace tcg
{
    public class EndTurnCommand : CommandBase
    {
        public EndTurnCommand()
        {
            requiresTurnPlayer = true;
        }

        public override GameActionResult Execute (ICommandContext context)
        {
            ITurnSystem turnSystem = context.TurnSystem;
            var result = turnSystem.EndTurn();
            if (result != GameActionResult.Success) return result;

            EndTurnEventArgs args = EndTurnEventArgs.New(turnSystem.TurnPlayer, turnSystem.TurnCounter);
            context.Events.OnEndTurn(args);

            return GameActionResult.Success;
        }
    }
}