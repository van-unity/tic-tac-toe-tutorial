using System;

namespace TicTacToe.Domain {
    /// <summary>
    /// Defines the contract for player move strategies within the TicTacToe game.
    /// </summary>
    public interface IPlayerMoveStrategy : IDisposable {
        void Execute(Action<BoardPosition> callback);

        void CancelExecution();
    }
}