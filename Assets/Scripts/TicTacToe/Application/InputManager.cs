using System;
using TicTacToe.Domain;
using TicTacToe.Domain.EventArgs;

namespace TicTacToe.Application {
    /// <summary>
    /// Manages input-related events within the Tic Tac Toe application.
    /// This class serves as both a raiser and subscriber for cell click events, 
    /// facilitating communication between the user interface and the game logic.
    /// </summary>
    public class InputManager : ICellClickRaiser, ICellClickSubscriber {
        public event Action<CellClickedEventArgs> CellClicked;

        public void RaiseCellClicked(BoardPosition clickedPos) {
            CellClicked?.Invoke(new CellClickedEventArgs(clickedPos));
        }
    }
}