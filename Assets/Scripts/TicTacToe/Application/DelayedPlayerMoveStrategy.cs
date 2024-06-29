using System;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.Domain;

namespace TicTacToe.Application {
    /// <summary>
    /// Implements a delayed move strategy for an automated player.
    /// This strategy waits for a specified delay before making a move, simulating a thinking process.
    /// </summary>
    public class DelayedPlayerMoveStrategy : IPlayerMoveStrategy {
        private const int DELAY_MS = 1000; 

        private readonly Board _board; 
        private readonly Random _random;

        private CancellationTokenSource _moveTaskCancellation;
        
        public DelayedPlayerMoveStrategy(Board board) {
            _board = board;
            _random = new Random();
        }
        
        public async void Execute(Action<BoardPosition> callback) {
            await MakeMoveAsync(callback);
        }
        
        public void CancelExecution() {
            _moveTaskCancellation?.Cancel();
        }
        
        private async Task MakeMoveAsync(Action<BoardPosition> callback) {
            var moveTask = MoveTask();
            var pos = await moveTask;
            callback?.Invoke(pos);
        }
        
        private async Task<BoardPosition> MoveTask() {
            _moveTaskCancellation?.Cancel();
            _moveTaskCancellation = new CancellationTokenSource();
            try {
                await Task.Delay(DELAY_MS, _moveTaskCancellation.Token);
                var randomIndex = _random.Next(0, _board.EmptyCells.Count);
                return _board.EmptyCells[randomIndex];
            }
            catch (TaskCanceledException) {
                return new BoardPosition(-1, -1); // Return an invalid position if the task is cancelled
            }
            finally {
                _moveTaskCancellation.Dispose();
                _moveTaskCancellation = null;
            }
        }
        
        public void Dispose() {
            if (_moveTaskCancellation != null) {
                _moveTaskCancellation.Cancel();
                _moveTaskCancellation.Dispose();
            }
        }
    }
}