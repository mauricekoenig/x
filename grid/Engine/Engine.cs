

using System;
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
    public class Engine : IEngine
    {
        private Grid grid;
        private bool isRunning;
        private List<IPlayer> playerList;
        private IParser parser;
        private TurnSystem turnSystem;

        protected GameState state;
        internal GameState GameState => state;

        public GameEvents Events { get; set; }
        private Engine ()
        {
            parser = Parser.New();
            Events = new GameEvents();
            playerList = new List<IPlayer>(2);
        }

        public static Engine New ()
        {
            return new Engine();
        }

        public GameActionResult Init(EngineConfig engineConfig)
        {
            var config = engineConfig.GridConfig;
            var player1 = engineConfig.Player1;
            var player2 = engineConfig.Player2;

            if (config == null) throw new ArgumentNullException(nameof(config));

            if (isRunning) return GameActionResult.AlreadyRunning;
            if (config.Rows <= 0 || config.Columns <= 0) return GameActionResult.InvalidGridSize;
            if (config.Columns % 2 == 0 || config.Rows % 2 == 0) return GameActionResult.GridSizeMustBeOdd;

            if (player1 == null || player2 == null) return GameActionResult.EmptyPlayer;
            if (player1.Id == player2.Id) return GameActionResult.DuplicatePlayer;

            if (parser.TryParseDeckCode(player1.DeckCode, out Deck deck1) != GameActionResult.Success || parser.TryParseDeckCode(player2.DeckCode, out Deck deck2) != GameActionResult.Success) return GameActionResult.InvalidDeckCode;

            player1.Deck = deck1;
            player2.Deck = deck2;

            if (playerList.Count > 0) playerList.Clear();
            playerList.Add(player1);
            playerList.Add(player2);

            turnSystem = TurnSystem.New(player1, player2);

            grid = Grid.New(config.Rows, config.Columns);
            state = new GameState(player1, player2, grid);

            GridEventArgs args = GridEventArgs.New(config.Rows, config.Columns);

            Events.OnGridCreated(args);

            isRunning = true;
            return GameActionResult.Success;
        }

        public GameActionResult Stop()
        {
            if (!isRunning) return GameActionResult.NotRunning;

            playerList.Clear();
            grid = null;
            isRunning = false;
            return GameActionResult.Success;
        }
        public GameActionResult TryExecute (ICommand action, IPlayer requester)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (requester == null) throw new ArgumentNullException(nameof(requester));

            GameActionResult result = GameActionResult.Success;

            if (action.RequiresTurnPlayer)
            {
                result = turnSystem.IsTurnPlayer(requester);
                if (result != GameActionResult.Success) return result;
            }

            ICommandContext context = new CommandContext(GameState, Events, turnSystem);
            result = action.Execute(context);
            if (result != GameActionResult.Success) return result;
            return GameActionResult.Success;
        }
    }
}
