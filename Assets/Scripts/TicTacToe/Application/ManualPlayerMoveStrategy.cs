using System;
using TicTacToe.Domain;
using TicTacToe.Domain.EventArgs;

namespace TicTacToe.Application {
    /// <summary>
    /// Implements a player move strategy that responds to manual cell click events.
    /// </summary>
    public class ManualPlayerMoveStrategy : IPlayerMoveStrategy {
        private readonly ICellClickSubscriber _cellClickSubscriber;
        private Action<BoardPosition> _clickHandler;

        private BoardPosition _clickedPos;
        
        public ManualPlayerMoveStrategy(ICellClickSubscriber cellClickSubscriber) {
            _cellClickSubscriber = cellClickSubscriber;
            _cellClickSubscriber.CellClicked += OnCellClicked;
        }
        
        private void OnCellClicked(CellClickedEventArgs clickArgs) {
            _clickHandler?.Invoke(clickArgs.ClickPosition);
            _clickHandler = null;
        }
        
        public void Execute(Action<BoardPosition> callback) {
            _clickHandler = callback;
        }
        
        public void CancelExecution() {
            _clickHandler = null;
        }
        
        public void Dispose() {
            _cellClickSubscriber.CellClicked -= OnCellClicked;
        }
    }
}