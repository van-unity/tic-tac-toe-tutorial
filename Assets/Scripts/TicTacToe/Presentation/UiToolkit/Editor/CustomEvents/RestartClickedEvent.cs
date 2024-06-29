using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor.CustomEvents {
    public class RestartClickedEvent : EventBase<RestartClickedEvent> {
        public static RestartClickedEvent GetEvent() {
            var clickEvent = GetPooled();

            return clickEvent;
        }
    }
}