

using System;
using System.Linq;
using System.Net.Mime;

namespace tcg
{
    public class MoveCommand : CommandBase
    {
        public ICard Card { get; set; }
        public GridPosition Destination { get; set; }

        public MoveCommand (ICard card, GridPosition destination)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            requiresTurnPlayer = true;

            this.Card = card;
            Destination = destination;

        }

        public override GameActionResult Execute (ICommandContext context)
        {
            if (!(Card is Unit unit)) return GameActionResult.CardIsNotAUnit;

            Grid grid = context.State.Grid;
            GameActionResult result = GameActionResult.Success;


            result = grid.IsValidPointOnGrid(Destination);
            if (result != GameActionResult.Success) return result;

            if (unit.StepsLeft == 0) return GameActionResult.UnitHasNoStepsLeft;


            result = grid.Contains(Card as ICard, out var currentPosition);
            if (result != GameActionResult.Success) return result;

            int distance = grid.GetChebyshevDistance(currentPosition, Destination);
            if (unit.StepsLeft < distance) return GameActionResult.OutOfReachablePositions;


            result = grid.GetReachablePositions(currentPosition, unit, out var positions);
            if (result != GameActionResult.Success) return result;

            bool withinReach = positions.Any(x => x.Equals(Destination));
            if (!withinReach) return GameActionResult.OutOfReachablePositions;

            result = grid.CanBePlacedAt(Destination);
            if (result != GameActionResult.Success) return result;

            result = grid.PlaceCard(Card as ICard, Destination);
            if (result != GameActionResult.Success) return result;

            unit.StepsLeft -= distance;

            MoveEventArgs args = MoveEventArgs.New(unit, Destination);
            context.Events.OnMoved(args);

            return result;
        }
    }
}
