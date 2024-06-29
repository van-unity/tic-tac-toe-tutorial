using System;
using TicTacToe.Domain.EventArgs;

namespace TicTacToe.Domain {
    /// <summary>
    /// Provides an interface for components that can subscribe to cell click events.
    /// </summary>
    public interface ICellClickSubscriber {
        event Action<CellClickedEventArgs> CellClicked;
    }
}