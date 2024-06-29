using TicTacToe.Application;
using TicTacToe.Domain;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public static class TicTacToeContext {
        public static TicTacToeViewModel ViewModel { get; private set; }

        private static GameplayModel _gameplayModel;

        public static void Initialize() {
            var gameSettings = new GameSettings();
            var boardModel = new Board(3, new BasicWinCheckStrategy());
            var inputManager = new InputManager();

            var manualMoveStrategyFactory = new ManualPlayerMoveStrategyFactory(inputManager);
            var autoMoveStrategyFactory = new DelayedPlayerMoveStrategyFactory(boardModel);

            var playerFactory = new PlayerFactory(manualMoveStrategyFactory, autoMoveStrategyFactory);

            _gameplayModel = new GameplayModel(playerFactory, boardModel, gameSettings);

            ViewModel?.Dispose();
            ViewModel = new TicTacToeViewModel(_gameplayModel, gameSettings, inputManager);
        }

        public static void Dispose() {
            ViewModel?.Dispose();
            _gameplayModel?.Dispose();
        }
    }
}