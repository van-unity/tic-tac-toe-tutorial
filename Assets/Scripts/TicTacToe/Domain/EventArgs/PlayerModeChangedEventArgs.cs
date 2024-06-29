namespace TicTacToe.Domain.EventArgs {
    /// <summary>
    /// Provides data for the event that is triggered when a player's mode changes.
    /// </summary>
    public class PlayerModeChangedEventArgs : System.EventArgs {
        public PlayerSymbol PlayerSymbol { get; }
        public PlayerMode NewMode { get; }

        public PlayerModeChangedEventArgs(PlayerSymbol playerSymbol, PlayerMode newMode) {
            PlayerSymbol = playerSymbol;
            NewMode = newMode;
        }
    }
}