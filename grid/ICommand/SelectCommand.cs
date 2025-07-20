
using System;
using System.Collections.Generic;

namespace tcg
{
    public sealed class SelectCommand : CommandBase
    {
        private ICard sourceCard;

        public SelectCommand (ICard source)
        {
            if (source == null) throw new ArgumentNullException();
            this.sourceCard = source;
            requiresTurnPlayer = true;
        }

        public override GameActionResult Execute (ICommandContext context)
        {
            GameActionResult result = context.State.Grid.Contains(sourceCard, out var position);
            if (result != GameActionResult.Success) return result;

            GridPosition sourcePosition = position;
            if (!(sourceCard is Unit unit)) return GameActionResult.SelectionSoureIsNoUnit;

            result = context.State.Grid.GetReachablePositions(sourcePosition, unit, out var positions);
            if (result != GameActionResult.Success) return GameActionResult.ErrorCalculatingReachablePositions;

            List<GridPosition> reachablePositions = positions;

            SelectionEventArgs args = SelectionEventArgs.New(sourceCard, sourcePosition, reachablePositions);
            context.Events.OnSelected(args);

            return GameActionResult.Success;
        }
    }
}