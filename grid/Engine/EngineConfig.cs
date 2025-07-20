namespace tcg
{
    public class EngineConfig
    {
        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }
        public GridConfig GridConfig { get; set; }

        private EngineConfig (IPlayer p1, IPlayer p2, GridConfig gc)
        {
            Player1 = p1;
            Player2 = p2;
            GridConfig = gc;
        }

        public EngineConfig New (IPlayer p1, IPlayer p2, GridConfig gc)
        {
            return new EngineConfig(p1, p2, gc);
        }

        public static EngineConfig Mock()
        {
            IPlayer p1 = HumanPlayer.New(1, "Yugi", DeckRecipes.Humans);
            IPlayer p2 = HumanPlayer.New(2, "Kaiba", DeckRecipes.Humans);
            GridConfig gc = GridConfig.New(5, 5);

            return new EngineConfig(p1, p2, gc);
        }
    }
}