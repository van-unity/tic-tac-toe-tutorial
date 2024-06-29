using TicTacToe.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor.CustomEvents {
    public class PlayerModeClickedEvent : EventBase<PlayerModeClickedEvent> {
        public PlayerSymbol PlayerSymbol { get; private set; }

        public static PlayerModeClickedEvent GetEvent(PlayerSymbol symbol) {
            var clickEvent = GetPooled();
            clickEvent.PlayerSymbol = symbol;

            return clickEvent;
        }
    }
}