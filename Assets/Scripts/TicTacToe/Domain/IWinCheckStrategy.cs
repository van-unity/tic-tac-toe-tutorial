namespace TicTacToe.Domain {
    /// <summary>
    /// Defines a strategy for checking for a win condition on a Tic TacToe board.
    /// </summary>
    public interface IWinCheckStrategy {
        Win CheckWin(Board board);
    }
}