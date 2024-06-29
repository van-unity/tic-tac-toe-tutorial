namespace TicTacToe.Domain {
    /// <summary>
    /// Represents a position on the board by row and column indexes.
    /// </summary>
    public readonly struct BoardPosition {
        public int RowIndex { get; }

        public int ColumnIndex { get; }

        public BoardPosition(int rowIndex, int columnIndex) {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }
    }
}