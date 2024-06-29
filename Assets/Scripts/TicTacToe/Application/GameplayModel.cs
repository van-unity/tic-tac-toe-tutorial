using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.Domain;
using TicTacToe.Domain.EventArgs;
using TicTacToe.Domain.Factories;

namespace TicTacToe.Application {
    /// <summary>
    /// Manages the state and logic of the Tic Tac Toe game, coordinating interactions
    /// between the game board and the players.
    /// </summary>
    public class GameplayModel : IDisposable {
        private readonly IPlayerFactory _playerFactory;
        private readonly Board _board;
        private readonly GameSettings _gameSettings;
        private readonly Dictionary<PlayerSymbol, Player> _players;

        private PlayerSymbol _currentPlayer;
        private CancellationTokenSource _turnSwitchCancellation;
        
        public bool IsGameStarted { get; private set; }

        public event Action<TurnChangedEventArgs> TurnChanged;
        public event Action<CellUpdatedEventArgs> CellUpdated;
        public event Action<PlayerModeChangedEventArgs> PlayerModeChanged;
        public event Action<GameEndedEventArgs> GameEnded;
        
        public GameplayModel(IPlayerFactory playerFactory, Board board, GameSettings gameSettings) {
            _playerFactory = playerFactory;

            _board = board;
            _gameSettings = gameSettings;

            _players = new Dictionary<PlayerSymbol, Player> {
                { PlayerSymbol.X, playerFactory.Create(PlayerSymbol.X, _gameSettings.GetPlayerMode(PlayerSymbol.X)) },
                { PlayerSymbol.O, playerFactory.Create(PlayerSymbol.O, _gameSettings.GetPlayerMode(PlayerSymbol.O)) }
            };

            Player.Moved += OnPlayerMoved;
        }
        
        private void OnPlayerMoved(PlayerMovedEventArgs moveEventArgs) {
            if (moveEventArgs.Symbol != _currentPlayer || !_board.IsPositionValid(moveEventArgs.Position)) {
                return;
            }

            CellUpdated?.Invoke(new CellUpdatedEventArgs(moveEventArgs.Symbol, moveEventArgs.Position));

            var result = _board.ResolvePlayerMove(moveEventArgs.Position, moveEventArgs.Symbol);

            HandleResolveResult(result);
        }

        private void HandleResolveResult(ResolveResult resolveResult) {
            if (resolveResult.IsBoardFull ||  resolveResult.Win != null) {
                IsGameStarted = false;

                GameEnded?.Invoke(new GameEndedEventArgs(resolveResult.Win));
                return;
            }

            WaitAndSwitchTurn();
        }

        private async void WaitAndSwitchTurn() {
            _turnSwitchCancellation?.Cancel();
            _turnSwitchCancellation = new CancellationTokenSource();

            try {
                await Task.Delay(_gameSettings.TurnSwitchDelay, _turnSwitchCancellation.Token);
                _currentPlayer = _currentPlayer == PlayerSymbol.O ? PlayerSymbol.X : PlayerSymbol.O;
                _players[_currentPlayer].MakeMove();
                TurnChanged?.Invoke(new TurnChangedEventArgs(_currentPlayer));
            }
            catch (TaskCanceledException) {
                // Ignore cancellation
            }
            finally {
                _turnSwitchCancellation?.Dispose();
                _turnSwitchCancellation = null;
            }
        }
        
        public void Start() {
            _currentPlayer = PlayerSymbol.X;
            IsGameStarted = true;
            _players[_currentPlayer].MakeMove();
            TurnChanged?.Invoke(new TurnChangedEventArgs(_currentPlayer));
        }

        public void Restart() {
            _turnSwitchCancellation?.Cancel();
            _players[_currentPlayer].CancelMove();
            _board.Reset();
            Start();
        }

        public void TogglePlayerMode(PlayerSymbol playerSymbol) {
            if (IsGameStarted) {
                return;
            }

            var newMode = _gameSettings.GetPlayerMode(playerSymbol) == PlayerMode.Manual
                ? PlayerMode.Auto
                : PlayerMode.Manual;

            var newPlayer = _playerFactory.Create(playerSymbol, newMode);
            _players[playerSymbol].Dispose();
            _players[playerSymbol] = newPlayer;

            _gameSettings.SetPlayerMode(playerSymbol, newMode);

            PlayerModeChanged?.Invoke(new PlayerModeChangedEventArgs(playerSymbol, newMode));
        }
        
        public void Dispose() {
            Player.Moved -= OnPlayerMoved;
            foreach (var player in _players.Values) {
                player.Dispose();
            }

            if (_turnSwitchCancellation != null) {
                _turnSwitchCancellation.Cancel();
                _turnSwitchCancellation.Dispose();
            }
        }
    }
}