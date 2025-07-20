


namespace tcg
{
    public interface ICanMove
    {
        IMovementBehaviour MoveBehaviour { get; set; }
        int StepsPerTurn { get; set; }
        int StepsLeft { get; set; }
    }
}