namespace TicTacToe.Domain.EventArgs {
    /// <summary>
    /// Provides data for the event that is triggered when the game ends.
    /// </summary>
    public class GameEndedEventArgs : System.EventArgs {
        public Win Win { get; }
        
        public GameEndedEventArgs(Win win) {
            Win = win;
        }
    }
}