namespace TicTacToe.Domain.EventArgs {
    /// <summary>
    /// Provides data for the event that is raised when a cell on the Tic Tac Toe board is updated.
    /// </summary>
    public class CellUpdatedEventArgs : System.EventArgs {
        public PlayerSymbol Symbol { get; }
        public BoardPosition Position { get; }
        
        public CellUpdatedEventArgs(PlayerSymbol symbol, BoardPosition position) {
            Symbol = symbol;
            Position = position;
        }
    }
}