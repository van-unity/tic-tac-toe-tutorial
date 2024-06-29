using TicTacToe.Domain;
using TicTacToe.Domain.Factories;

namespace TicTacToe.Application {
    /// <summary>
    /// Factory for creating Player instances based on the given PlayerMode.
    /// </summary>
    public class PlayerFactory : IPlayerFactory {
        private readonly IPlayerMoveStrategyFactory _manualMoveStrategyFactory;
        private readonly IPlayerMoveStrategyFactory _autoMoveStrategyFactory;

        public PlayerFactory(IPlayerMoveStrategyFactory manualMoveStrategyFactory,
            IPlayerMoveStrategyFactory autoMoveStrategyFactory) {
            _manualMoveStrategyFactory = manualMoveStrategyFactory;
            _autoMoveStrategyFactory = autoMoveStrategyFactory;
        }

        public Player Create(PlayerSymbol playerSymbol, PlayerMode playerMode) {
            var moveStrategy = playerMode switch {
                PlayerMode.Auto => _autoMoveStrategyFactory.Create(),
                _ => _manualMoveStrategyFactory.Create()
            };

            return new Player(playerSymbol, moveStrategy);
        }
    }
}