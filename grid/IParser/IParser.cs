


namespace tcg
{
    public interface IParser
    {
        GameActionResult TryParseDeckCode(string deckCode, out Deck deck);
        GameActionResult CreateCardFromData(CardData cardData, out ICard card);
        ICard CreateCardFromId(int id);
        ICard CreateCardFromName(string name);

    }
}