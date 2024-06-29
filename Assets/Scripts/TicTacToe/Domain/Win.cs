using System.Collections.Generic;

namespace TicTacToe.Domain {
    /// <summary>
    /// Represents the result of a win in the Tic Tac Toe game.
    /// Contains the winning player symbol and the positions on the board that formed the win.
    /// </summary>
    public class Win {
        private readonly List<BoardPosition> _winPositions;
        public PlayerSymbol Symbol { get; }
        
        public IReadOnlyList<BoardPosition> WinPositions => _winPositions;
        
        public Win(PlayerSymbol symbol, IEnumerable<BoardPosition> winPositions) {
            Symbol = symbol;
            _winPositions = new List<BoardPosition>(winPositions);
        }
    }
}