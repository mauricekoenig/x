


using System;
using System.Collections.Generic;

namespace tcg
{

    public class Hand
    {
        private List<ICard> cards;
        public int Count => cards.Count;

        private Hand()
        {
            cards = new List<ICard>();
        }
        public static Hand New()
        {
            return new Hand();
        }

        public GameActionResult Add (ICard card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));
            if (cards.Count >= Constants.HandLimit) return GameActionResult.AlreadyMaxCardsInHand;
            cards.Add(card);
            return GameActionResult.Success;
        }
        public GameActionResult Contains (ICard card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));

            foreach (ICard c in cards)
            {
                if (c.Id == card.Id) return GameActionResult.Success;
            }

            return GameActionResult.HandDoesNotContainCard;
        }
        public GameActionResult Remove (ICard card)
        {
           if (card == null) throw new ArgumentNullException(nameof(card));
            var result = Contains(card);
            if (result != GameActionResult.Success) return result;
            cards.Remove(card);
            return GameActionResult.Success;
        }

        private void Clear()
        {
            if(cards == null) throw new ArgumentNullException(nameof(cards));
            cards.Clear();
        }
    }
}