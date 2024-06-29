using System.Collections.Generic;

namespace TicTacToe.Domain {
    /// <summary>
    /// Manages the game settings, specifically the modes of the players and the turn switch delay.
    /// </summary>
    public class GameSettings {
        private readonly Dictionary<PlayerSymbol, PlayerMode> _playerModeBySymbol;
        
        public int TurnSwitchDelay { get; }
        
        public GameSettings(PlayerMode xPlayerMode = PlayerMode.Manual, PlayerMode oPlayerMode = PlayerMode.Auto,
            int turnSwitchDelay = 500) {
            _playerModeBySymbol = new() {
                { PlayerSymbol.X, xPlayerMode },
                { PlayerSymbol.O, oPlayerMode }
            };

            TurnSwitchDelay = turnSwitchDelay;
        }
        
        public PlayerMode GetPlayerMode(PlayerSymbol playerSymbol) => _playerModeBySymbol[playerSymbol];
        
        public void SetPlayerMode(PlayerSymbol symbol, PlayerMode mode) {
            _playerModeBySymbol[symbol] = mode;
        }
    }
}