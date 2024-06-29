namespace TicTacToe.Domain.EventArgs {
    /// <summary>
    /// Represents data for the event that is triggered when the current player's turn changes.
    /// </summary>
    public class TurnChangedEventArgs : System.EventArgs {
        public PlayerSymbol CurrentPlayerSymbol { get; }
        
        public TurnChangedEventArgs(PlayerSymbol currentPlayerSymbol) {
            CurrentPlayerSymbol = currentPlayerSymbol;
        }
    }
}