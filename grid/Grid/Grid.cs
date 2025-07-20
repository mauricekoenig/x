using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace tcg
{
    public class Grid
    {
        private int _rows;
        private int _columns;

        public int Rows => _rows;
        public int Columns => _columns;

        private Dictionary<GridPosition, ICard> map;

        private  Grid (int rows, int columns)
        {
            _rows = rows;
            _columns = columns;

            map = new Dictionary<GridPosition, ICard> ();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++) {

                    map[(GridPosition.New(i + 1, j + 1))] = null;
                }
            }
        }

        public static Grid New (int rows, int columns)
        {
            return new Grid (rows, columns);
        }

        public GameActionResult IsValidPointOnGrid (GridPosition point)
        {
            if (point.x > _rows || point.x < 1) return GameActionResult.OutOfGridDimensions;
            if (point.y > _columns || point.y < 1) return GameActionResult.OutOfGridDimensions;

            return GameActionResult.Success;
        }
        public GameActionResult CanBePlacedAt(GridPosition point)
        {
            if (IsValidPointOnGrid(point) != GameActionResult.Success) return GameActionResult.InvalidGridPoint;
            bool isEmpty = map[point] == null;
            if (isEmpty) return GameActionResult.Success;
            return GameActionResult.GridPointTaken;
        }
        public GameActionResult PlaceCard (ICard card, GridPosition gridPosition)
        {

            
            if (card == null) throw new ArgumentNullException(nameof(card));
            GameActionResult result = GameActionResult.Success;

            result = IsValidPointOnGrid(gridPosition);
            if (result != GameActionResult.Success) return result;

            result = CanBePlacedAt(gridPosition);
            if (result != GameActionResult.Success) return result;

            result = Contains(card, out var oldPosition);
            if (result == GameActionResult.Success) map[oldPosition] = null;

            map[gridPosition] = card;

            return GameActionResult.Success;
        }
        public GameActionResult Contains (ICard card, out GridPosition gridPosition)
        {
            gridPosition = GridPosition.Zero;

            foreach (var entry in map)
            {
                if (entry.Value == null) continue;

                if (entry.Value.InstanceId == card.InstanceId)
                {
                    gridPosition = entry.Key;
                    return GameActionResult.Success;
                }
            }

            return GameActionResult.GridDoesNotContainCard;
        }

        public GameActionResult GetReachablePositions (GridPosition fromPosition, Unit unit, out List<GridPosition> positions)
        {

            positions = new List<GridPosition>();

            if (fromPosition == null || unit == null) return GameActionResult.PassedNullArgument;

            foreach (var direction in unit.MoveBehaviour.Directions)
            {
                GridPosition currentPosition = fromPosition;

                for (int i = 0; i < unit.StepsLeft; i++)
                {
                    GridPosition newPosition = currentPosition + direction;

                    if (newPosition.x < 1 || newPosition.x > Columns) break;
                    if (newPosition.y < 1 || newPosition.y > Rows) break;

                    positions.Add(newPosition);
                    currentPosition = newPosition;
                }
            }

            return GameActionResult.Success;
        }

        public int GetChebyshevDistance(GridPosition a, GridPosition b)
        {
            return Math.Max(Math.Abs(a.x - b.x), Math.Abs(a.y - b.y));
        }
    }
}
