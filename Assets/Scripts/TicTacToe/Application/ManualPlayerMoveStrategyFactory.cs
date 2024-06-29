using TicTacToe.Domain;
using TicTacToe.Domain.Factories;

namespace TicTacToe.Application {
    /// <summary>
    /// A factory for creating instances of ManualPlayerMoveStrategy.
    /// </summary>
    public class ManualPlayerMoveStrategyFactory : IPlayerMoveStrategyFactory {
        private readonly ICellClickSubscriber _cellClickSubscriber; // The subscriber to cell click events.

        public ManualPlayerMoveStrategyFactory(ICellClickSubscriber cellClickSubscriber) {
            _cellClickSubscriber = cellClickSubscriber;
        }

        public IPlayerMoveStrategy Create() => new ManualPlayerMoveStrategy(_cellClickSubscriber);
    }
}