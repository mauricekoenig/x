


using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
    public class Deck
    {
        private List<ICard> cards;
        public int Count => cards.Count;
        private Deck (List<ICard> cards)
        {
            this.cards = cards;
        }

        public static Deck New (List<ICard> cards)
        {
            return new Deck(cards);
        }
        public GameActionResult AddCard (ICard card)
        {
            if (card == null) return GameActionResult.CardIsNull;
            cards.Add(card);
            return GameActionResult.Success;
        }
        public GameActionResult RemoveAt (int index)
        {
            if (index < 0 || index >= cards.Count)
            return GameActionResult.IndexOutOfRange;

            cards.RemoveAt(index);
            return GameActionResult.Success;
        }
        public GameActionResult RemoveAllById (int id)
        {
            int removed = cards.RemoveAll(x => x.Id == id);
            if (removed == 0) return GameActionResult.CardDoesNotExistInDeck;
            return GameActionResult.Success;
        }
        public bool Contains (ICard card)
        {
            return cards.Any(x => x.Id == card.Id);
        }
        public GameActionResult DrawTopCard (out ICard card)
        {
            card = null;
            if (cards.Count == 0) return GameActionResult.DeckIsEmpty;
            card = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return GameActionResult.Success;
        }
        public GameActionResult PeekTopCard(out ICard card)
        {
            card = null;
            if (cards.Count == 0) return GameActionResult.DeckIsEmpty;
            card = cards[^1]; // top = last
            return GameActionResult.Success;
        }
        public GameActionResult Shuffle (Random rng = null)
        {
            rng ??= new Random();

            for (int i = cards.Count - 1; i > 0; i--) {
                int j = rng.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            return GameActionResult.Success;
        }
        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < cards.Count; i++)
            {
                ICard card = cards[i];
                s += $"[Id: {card.Id}, Name: {card.Name}, Cost: {card.Cost}]\n";
            }

            return s;
        }

    }
}