using TicTacToe.Domain;
using TicTacToe.Domain.Factories;

namespace TicTacToe.Application {
    /// <summary>
    /// Factory for creating instances of automatic player move strategies.
    /// </summary>
    public class DelayedPlayerMoveStrategyFactory : IPlayerMoveStrategyFactory {
        private readonly Board _board;
        
        public DelayedPlayerMoveStrategyFactory(Board board) {
            _board = board;
        }
        
        public IPlayerMoveStrategy Create() => new DelayedPlayerMoveStrategy(_board);
    }
}