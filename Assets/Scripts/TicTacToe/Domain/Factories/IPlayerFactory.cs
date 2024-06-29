namespace TicTacToe.Domain.Factories {
    /// <summary>
    /// Defines a factory interface for creating instances of <see cref="Player"/>..
    /// </summary>
    public interface IPlayerFactory {
        Player Create(PlayerSymbol playerSymbol, PlayerMode mode);
    }
}