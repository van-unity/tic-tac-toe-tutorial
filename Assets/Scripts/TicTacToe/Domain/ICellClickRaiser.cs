namespace TicTacToe.Domain {
    /// <summary>
    /// Provides an interface for components that can raise cell click events in the game board.
    /// </summary>
    public interface ICellClickRaiser {
        void RaiseCellClicked(BoardPosition clickedPos);
    }
}