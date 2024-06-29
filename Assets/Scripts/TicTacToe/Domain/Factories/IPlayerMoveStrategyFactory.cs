namespace TicTacToe.Domain.Factories {
    /// <summary>
    /// Defines a factory interface for creating instances of <see cref="IPlayerMoveStrategy"/>.
    /// </summary>
    public interface IPlayerMoveStrategyFactory {
        IPlayerMoveStrategy Create();
    }
}