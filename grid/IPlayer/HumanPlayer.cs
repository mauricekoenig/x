


namespace tcg
{
    public sealed class HumanPlayer : BasePlayer
    {
        private HumanPlayer(int id, string name, string deckCode) : base(id, name, deckCode)
        {

        }

        public static HumanPlayer New (int id, string name, string deckCode)
        {
            return new HumanPlayer(id, name, deckCode);
        }
    }
}