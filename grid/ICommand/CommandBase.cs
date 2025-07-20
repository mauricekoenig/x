


namespace tcg
{
    public abstract class CommandBase : ICommand
    {

        protected string name;
        public string Name => name;

        protected bool requiresTurnPlayer;
        public bool RequiresTurnPlayer => requiresTurnPlayer;

        public abstract GameActionResult Execute(ICommandContext context);
    }
}