
namespace tcg
{
    public interface ITurnSystem
    {
        int TurnCounter { get; }
        IPlayer TurnPlayer { get; }
        IPlayer WaitingPlayer { get; }

        GameActionResult EndTurn();
        GameActionResult IsTurnPlayer(IPlayer player);
    }
}