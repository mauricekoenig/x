
using System.Collections.Generic;
using System.Linq;

namespace tcg
{
    public interface IGameState
    {
        ICard CurrentSelection { get; set; }
        IReadOnlyList<IPlayer> Players { get; }
        Grid Grid { get; }
        GameActionResult GetPlayerById (int id, out IPlayer player);
    }

    public class GameState : IGameState
    {
        public Grid Grid { get; }
        public ICard CurrentSelection { get; set; }
        public IReadOnlyList<IPlayer> Players { get; }

        public GameState (IPlayer player1, IPlayer player2, Grid grid)
        {
            Players = new List<IPlayer> { player1, player2 }.AsReadOnly();
            Grid = grid;
        }

        public GameActionResult GetPlayerById(int id, out IPlayer player)
        {
            foreach (var p in Players)
            {
                if (p.Id == id)
                {
                    player = p;
                    return GameActionResult.Success;
                }
            }

            player = null;
            return GameActionResult.PlayerNotFound;
        }
    }
}