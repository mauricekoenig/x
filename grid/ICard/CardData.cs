


using System.Text.Json.Serialization;

namespace tcg
{
    public class CardData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CardType { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Steps { get; set; }
        public int Range { get; set; }
        public string Tribe { get; set; }
        public string[] Keywords { get; set; }
        public string Rarity { get; set; }
        public string[] MovementBehaviour { get; set; }

    public static CardData MockCreature()
        {
            return new CardData()
            {
                Id = 1,
                Name = "FooBar",
                CardType = "Unit",
                Cost = 1,
                Hp = 1,
                Attack = 1,
                Defense = 1,
                Steps = 1,
                Range = 1,
                Tribe = "All",
                Keywords = new string[] { "Flying" },
                Rarity = "Common",
                MovementBehaviour = new string[] { "diagonal", "3" }
            };
        }

    }
}