namespace TicTacToe.Domain.EventArgs {
    /// <summary>
    /// Provides data for events triggered when a cell on the Tic Tac Toe board is clicked.
    /// </summary>
    public class CellClickedEventArgs : System.EventArgs {
        public BoardPosition ClickPosition { get; }
        
        public CellClickedEventArgs(BoardPosition clickPosition) {
            ClickPosition = clickPosition; 
        }
    }
}