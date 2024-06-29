namespace TicTacToe.Domain {
    /// <summary>
    /// Represents the result of resolving a player's move on the Tic-Tac-Toe board.
    /// </summary>
    public class ResolveResult {
        public bool IsBoardFull { get; }
        public Win Win { get; }

        public ResolveResult(bool isBoardFull, Win win) {
            IsBoardFull = isBoardFull;
            Win = win;
        }
    }
}