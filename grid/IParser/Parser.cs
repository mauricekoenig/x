
using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
    public class Parser : IParser
    {
        private readonly string DeckCodeEntrySeparator = ",";
        private readonly string DeckCodeEntrySectionSeparator = "=";

        private Parser()
        {
            
        }
        public static Parser New()
        {
            return new Parser();
        }
        public GameActionResult TryParseDeckCode (string deckCode, out Deck deck)
        {
            deck = null;
            var cards = new List<ICard>();

            if (string.IsNullOrWhiteSpace(deckCode)) return GameActionResult.EmptyString;
            if (deckCode.Length < 3) return GameActionResult.InvalidDeckCode;

            var entries = deckCode.Split(DeckCodeEntrySeparator);
            var cardsAlreadyParsed = new Dictionary<int, int>();

            foreach (var entry in entries)
            {
                var entryTrimmed = entry.Trim();
                var keyValue = entryTrimmed.Split(DeckCodeEntrySectionSeparator);

                if (keyValue.Length != 2) return GameActionResult.InvalidDeckCodeFormat;

                bool canParseId = int.TryParse(keyValue[0], out int id);
                if (!canParseId) return GameActionResult.ParseErrorDeckCodeKeyValuePair;

                bool canParseAmount = int.TryParse(keyValue[1], out int amount);
                if (!canParseAmount) return GameActionResult.ParseErrorDeckCodeKeyValuePair;

                if (id < 0) return GameActionResult.TryToAddCardWithNegativeId;
                if (amount <= 0 || amount > Constants.CopiesPerDeck) return GameActionResult.ExceedingCardLimitPerDeck;

                if (!cardsAlreadyParsed.ContainsKey(id))
                    cardsAlreadyParsed[id] = 0;

                if (cardsAlreadyParsed[id] + amount > Constants.CopiesPerDeck) return GameActionResult.ExceedingCardLimitPerDeck;

                for (int i = 0; i < amount; i++)
                {
                    ICard card = CreateCardFromId(id);
                    if (card == null) return GameActionResult.CanNotFindCardById;
                    cards.Add(card);
                    cardsAlreadyParsed[id]++;
                }
            }

            if (cards.Count != Constants.RequiredDeckSize) return GameActionResult.InvalidDeckSize;

            deck = Deck.New(cards);
            return GameActionResult.Success;
        }
        public ICard CreateCardFromId (int id)
        {
            return ResourceLoader.LoadEmbeddedJsonCards().FirstOrDefault(x => x.Id == id);
        }
        public ICard CreateCardFromName (string name)
        {
            return ResourceLoader.LoadEmbeddedJsonCards().FirstOrDefault(x => x.Name.Trim().ToLowerInvariant() == name.Trim().ToLowerInvariant());
        }
        public GameActionResult CreateCardFromData (CardData cardData, out ICard card)
        {

            card = null;
            if (cardData == null) return GameActionResult.PassedNullArgument;

            if (Enum.TryParse<CardType>(cardData.CardType, out CardType cardType))
            { 
                switch (cardType)
                {
                    case CardType.Unit:

                        var parseKeywordResult = ParseKeywords(cardData, out var keywords);
                        if (parseKeywordResult != GameActionResult.Success) return parseKeywordResult;
                        var parseMovementBehaviourResult = ParseMovementBehaviour(cardData, out var movementBehaviour);
                        if (parseMovementBehaviourResult != GameActionResult.Success) return parseMovementBehaviourResult;

                        Unit unit = Unit.New(cardData.Id, cardData.Name, cardData.Hp, cardData.Attack, cardData.Defense, cardData.Steps, cardData.Range, keywords, movementBehaviour);
                        card = unit;
                        return GameActionResult.Success;

                    case CardType.Spell:
                        return GameActionResult.Success;

                    default:
                        return GameActionResult.WeirdEdgeCase;
                }
            }

            return GameActionResult.CanNotParseCardTypeToEnum;
        }

        protected GameActionResult ParseKeywords (CardData cardData, out List<IKeyword> keywords)
        {
            keywords = new List<IKeyword>();

            if (cardData.Keywords != null && cardData.Keywords.Length > 0)
            {
                foreach (var word in cardData.Keywords)
                {
                    string fullTypeName = $"tcg.Keyword_{word}";
                    Type type = Type.GetType(fullTypeName);
                    if (type == null) continue;
                    var instance = Activator.CreateInstance(type);
                    if (!(instance is IKeyword)) continue;
                    IKeyword keyword = (IKeyword)instance;
                    keywords.Add(keyword);
                }
            }

            return GameActionResult.Success;
        }

        protected GameActionResult ParseMovementBehaviour (CardData cardData, out IMovementBehaviour movementBehaviour)
        {
            movementBehaviour = null;


            if (cardData.MovementBehaviour != null && cardData.MovementBehaviour.Length == 2)
            {
                bool canParseRange = int.TryParse(cardData.MovementBehaviour[1], out int range);
                if (!canParseRange) return GameActionResult.ParseErrorMovementBehavior;

                string behaviorName = cardData.MovementBehaviour[0].Trim().ToLowerInvariant();
                if (behaviorName.Length < 2) return GameActionResult.ParseErrorMovementBehavior;
                string fullTypeName = $"tcg.Movement_{behaviorName.First().ToString().ToUpper()}{behaviorName.Substring(1)}";

                Type type = Type.GetType(fullTypeName);
                if (type == null) return GameActionResult.MovementBehaviourDoesNotExist;
                var instance = Activator.CreateInstance(type, range);
                if (!(instance is IMovementBehaviour)) return GameActionResult.MovementBehaviourDoesNotExist;
                movementBehaviour = instance as IMovementBehaviour;
                return GameActionResult.Success;
            }

            return GameActionResult.ParseErrorMovementBehavior;
        }

        public ICard GetTestCard()
        {
            return CreateCardFromId(1);
        }

    }

}