

namespace tcg
{
    public interface IPlayer
    {
        int Id { get; }
        string Name { get; set; }
        string DeckCode { get; set; }
        Deck Deck { get; set; }
    }

}