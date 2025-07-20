


namespace tcg
{
    public abstract class BasePlayer : IPlayer
    {
        public int Id { get; }
        public string Name { get; set; }
        public string DeckCode { get; set; }
        public Deck Deck { get; set; }

        public BasePlayer(int id, string name, string deckCode)
        {
            Id = id;
            Name = name;
            DeckCode = deckCode;
        }
    }
}