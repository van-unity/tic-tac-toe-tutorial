using System;
using TicTacToe.Domain.EventArgs;

namespace TicTacToe.Domain {
    /// <summary>
    /// Represents a player in the TicTacToe game.
    /// </summary>
    public class Player : IDisposable {
        private readonly PlayerSymbol _symbol;
        private readonly IPlayerMoveStrategy _moveStrategy;
        
        public static event Action<PlayerMovedEventArgs> Moved;
        
        public Player(PlayerSymbol symbol, IPlayerMoveStrategy moveStrategy) {
            _symbol = symbol;
            _moveStrategy = moveStrategy;
        }
        
        public void MakeMove() {
            _moveStrategy.Execute(position => { Moved?.Invoke(new PlayerMovedEventArgs(_symbol, position)); });
        }
        
        public void CancelMove() {
            _moveStrategy.CancelExecution();
        }

        public void Dispose() {
            _moveStrategy?.Dispose();
        }
    }
}