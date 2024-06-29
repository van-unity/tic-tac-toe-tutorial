namespace TicTacToe.Domain {
    /// <summary>
    /// Implements a basic strategy for checking wins in a TicTacToe game on any size board.
    /// </summary>
    public class BasicWinCheckStrategy : IWinCheckStrategy {
        /// <summary>
        /// Checks the entire board for any winning patterns.
        /// </summary>
        /// <param name="board">A two-dimensional array representing the game board state with PlayerSymbol values.</param>
        /// <returns>A Win object if a win is found, otherwise null.</returns>
        public Win CheckWin(Board board) {
            var size = board.Size;
            
            // Check rows for a win
            for (int i = 0; i < size; i++) {
                if (board.GetAt(i, 0) != PlayerSymbol.Empty) {
                    // Ensure the row is not empty
                    var rowWin = true;
                    var winningPosition = new BoardPosition[size];
                    winningPosition[0] = new BoardPosition(i, 0); // Start checking from the first cell in the row

                    for (int j = 1; j < size; j++) {
                        if (board.GetAt(i, j) != board.GetAt(i, 0)) {
                            rowWin = false; // If any cell does not match, it's not a winning row
                            break;
                        }

                        winningPosition[j] = new BoardPosition(i, j); // Store each position in the winning row
                    }

                    if (rowWin) {
                        return new Win(board.GetAt(i, 0), winningPosition);
                    }
                }
            }

            // Check columns for a win
            for (int i = 0; i < size; i++) {
                if (board.GetAt(0, i) != PlayerSymbol.Empty) {
                    // Ensure the column is not empty
                    var colWin = true;
                    var winningPosition = new BoardPosition[board.Size];
                    winningPosition[0] = new BoardPosition(0, i); // Start checking from the first cell in the column

                    for (int j = 1; j < size; j++) {
                        if (board.GetAt(j, i) != board.GetAt(0, i)) {
                            colWin = false; // If any cell does not match, it's not a winning column
                            break;
                        }

                        winningPosition[j] = new BoardPosition(j, i); // Store each position in the winning column
                    }

                    if (colWin) {
                        return new Win(board.GetAt(0, i), winningPosition);
                    }
                }
            }

            // Check top-left to bottom-right diagonal
            if (board.GetAt(0, 0) != PlayerSymbol.Empty) {
                // Check if the starting position of the diagonal is not empty
                var diag1Win = true;
                var winningPosition = new BoardPosition[size];
                winningPosition[0] = new BoardPosition(0, 0); // Start from the top-left corner

                for (int i = 1; i < size; i++) {
                    if (board.GetAt(i, i) != board.GetAt(0, 0)) {
                        diag1Win = false; // If any diagonal cell does not match, it's not a winning diagonal
                        break;
                    }

                    winningPosition[i] = new BoardPosition(i, i); // Store each position in the diagonal
                }

                if (diag1Win) {
                    return new Win(board.GetAt(0, 0), winningPosition);
                }
            }

            // Check top-right to bottom-left diagonal
            if (board.GetAt(0, size - 1) != PlayerSymbol.Empty) {
                // Check if the starting position of the opposite diagonal is not empty
                var diag2Win = true;
                var winningPosition = new BoardPosition[size];
                winningPosition[0] = new BoardPosition(0, size - 1); // Start from the top-right corner

                for (int i = 1; i < size; i++) {
                    if (board.GetAt(i, size - 1 - i) != board.GetAt(0, size - 1)) {
                        diag2Win =
                            false; // If any cell on the opposite diagonal does not match, it's not a winning diagonal
                        break;
                    }

                    winningPosition[i] = new BoardPosition(i, size - 1 - i); // Store each position in the diagonal
                }

                if (diag2Win) {
                    return new Win(board.GetAt(0, size - 1), winningPosition);
                }
            }

            // No win found
            return null;
        }
    }
}