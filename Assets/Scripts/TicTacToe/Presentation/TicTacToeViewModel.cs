using System;
using TicTacToe.Application;
using TicTacToe.Domain;
using TicTacToe.Domain.EventArgs;

namespace TicTacToe.Presentation {
    public class TicTacToeViewModel : IDisposable {
        private readonly GameplayModel _gameplayModel;
        private readonly GameSettings _gameSettings;
        private readonly ICellClickRaiser _cellClickRaiser;

        public bool IsGameStarted => _gameplayModel.IsGameStarted;
        public PlayerMode XPlayerMode => _gameSettings.GetPlayerMode(PlayerSymbol.X);
        public PlayerMode OPlayerMode => _gameSettings.GetPlayerMode(PlayerSymbol.O);

        public event Action<CellUpdatedEventArgs> CellUpdated;
        public event Action<TurnChangedEventArgs> TurnChanged;
        public event Action<PlayerModeChangedEventArgs> PlayerModeChanged;
        public event Action<GameEndedEventArgs> GameEnded;

        public TicTacToeViewModel(GameplayModel gameplayModel, GameSettings gameSettings, ICellClickRaiser cellClickRaiser) {
            _gameplayModel = gameplayModel;
            _gameSettings = gameSettings;
            _cellClickRaiser = cellClickRaiser;

            _gameplayModel.CellUpdated += OnCellUpdated;
            _gameplayModel.TurnChanged += OnTurnChanged;
            _gameplayModel.PlayerModeChanged += OnPlayerModeChanged;
            _gameplayModel.GameEnded += OnGameEnded;
        }
        
        private void OnCellUpdated(CellUpdatedEventArgs cellUpdatedEventArgs) {
            CellUpdated?.Invoke(cellUpdatedEventArgs);
        }

        private void OnGameEnded(GameEndedEventArgs gameEndedEventArgs) {
            GameEnded?.Invoke(gameEndedEventArgs);
        }

        private void OnPlayerModeChanged(PlayerModeChangedEventArgs modeChangedArgs) {
            PlayerModeChanged?.Invoke(modeChangedArgs);
        }

        private void OnTurnChanged(TurnChangedEventArgs turnChangedArgs) {
            TurnChanged?.Invoke(turnChangedArgs);
        }

        public void TogglePlayerMode(PlayerSymbol symbol) {
            _gameplayModel.TogglePlayerMode(symbol);
        }

        public void HandleCellClick(BoardPosition boardPosition) {
            _cellClickRaiser.RaiseCellClicked(boardPosition);
        }

        public void HandleStart() {
            _gameplayModel.Start();
        }

        public void HandleRestart() {
            _gameplayModel.Restart();
        }

        public void Dispose() {
            _gameplayModel.CellUpdated -= OnCellUpdated;
            _gameplayModel.TurnChanged -= OnTurnChanged;
            _gameplayModel.PlayerModeChanged -= OnPlayerModeChanged;
            _gameplayModel.GameEnded -= OnGameEnded;
        }
    }
}