using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor.CustomEvents {
    public class StartClickedEvent : EventBase<StartClickedEvent> {
        public static StartClickedEvent GetEvent() {
            var clickEvent = GetPooled();

            return clickEvent;
        }
    }
}