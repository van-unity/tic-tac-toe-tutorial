using TicTacToe.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor.CustomEvents {
    public class CellClickedEvent : EventBase<CellClickedEvent> {
        public BoardPosition ClickPosition { get; private set; }

        public static CellClickedEvent GetEvent(BoardPosition position) {
            var clickEvent = GetPooled();
            clickEvent.ClickPosition = position;

            return clickEvent;
        }
    }
}