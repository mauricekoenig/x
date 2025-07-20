

using System;
using System.Collections.Generic;

namespace tcg
{
    public class GameEvents
    {
        public event EventHandler<GridEventArgs> GridCreated;

        public event EventHandler<MoveEventArgs> Moved;
        public event EventHandler<SelectionEventArgs> Selected;
        public event EventHandler<PlayCardEventArgs> Played;
        public event EventHandler<DrawCardEventArgs> Draw;
        public event EventHandler<EndTurnEventArgs> EndTurn;

        internal void OnDraw (DrawCardEventArgs args) => Draw?.Invoke (this, args);
        internal void OnPlayed (PlayCardEventArgs args) => Played?.Invoke(this, args);
        internal void OnSelected (SelectionEventArgs args) => Selected?.Invoke(this, args);
        internal void OnMoved (MoveEventArgs args) => Moved?.Invoke (this, args);
        internal void OnGridCreated (GridEventArgs args) => GridCreated?.Invoke(this, args);
        internal void OnEndTurn (EndTurnEventArgs args) => EndTurn?.Invoke(this, args);
    }
}