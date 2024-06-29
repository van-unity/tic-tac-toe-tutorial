namespace TicTacToe.Domain.EventArgs {
    /// <summary>
    /// Provides data for events triggered when a player makes a move in the TicTacToe game.
    /// </summary>
    public class PlayerMovedEventArgs : System.EventArgs {
        public PlayerSymbol Symbol { get; }
        public BoardPosition Position { get; }
        
        public PlayerMovedEventArgs(PlayerSymbol symbol, BoardPosition position) {
            Symbol = symbol;
            Position = position;
        }
    }
}