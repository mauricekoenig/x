


using System;

namespace tcg
{
    public interface ICommand
    {
        string Name { get; }
        bool RequiresTurnPlayer { get; }
        GameActionResult Execute (ICommandContext context);
    }

    public interface ICommandContext
    {
        IGameState State { get; }
        GameEvents Events { get; }
        TurnSystem TurnSystem { get; }
    }

    public class CommandContext : ICommandContext
    {
        public IGameState State { get; }
        public GameEvents Events { get; }
        public TurnSystem TurnSystem { get; }

        public CommandContext (GameState state, GameEvents events, TurnSystem turnSystem)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (events == null) throw new ArgumentNullException(nameof(events));
            if (turnSystem == null) throw new ArgumentNullException(nameof(turnSystem));

            State = state;
            Events = events;
            TurnSystem = turnSystem;
        }
    }
}