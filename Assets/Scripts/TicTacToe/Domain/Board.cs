using System;
using System.Collections.Generic;

namespace TicTacToe.Domain {
    public class Board {
        private readonly PlayerSymbol[,] _state;
        private readonly List<BoardPosition> _emptyCells;
        private readonly IWinCheckStrategy _winCheckStrategy;

        public int Size { get; }

        public IReadOnlyList<BoardPosition> EmptyCells => _emptyCells;

        public Board(int size, IWinCheckStrategy winCheckStrategy) {
            Size = size;
            _state = new PlayerSymbol[Size, Size];
            _emptyCells = new List<BoardPosition>(Size * Size);
            _winCheckStrategy = winCheckStrategy;

            InitializeState();
            InitializeEmptyCells();
        }

        private void InitializeState() {
            for (int rowIndex = 0; rowIndex < Size; rowIndex++) {
                for (int columnIndex = 0; columnIndex < Size; columnIndex++) {
                    _state[rowIndex, columnIndex] = PlayerSymbol.Empty;
                }
            }
        }

        private void InitializeEmptyCells() {
            _emptyCells.Clear();
            for (int rowIndex = 0; rowIndex < Size; rowIndex++) {
                for (int columnIndex = 0; columnIndex < Size; columnIndex++) {
                    _emptyCells.Add(new BoardPosition(rowIndex, columnIndex));
                }
            }
        }

        public PlayerSymbol GetAt(BoardPosition position) => GetAt(position.RowIndex, position.ColumnIndex);

        public PlayerSymbol GetAt(int rowIndex, int columnIndex) {
            if (!IsPositionValid(rowIndex, columnIndex)) {
                throw new IndexOutOfRangeException($"Trying to get an invalid position: ({rowIndex}, {columnIndex})");
            }

            return _state[rowIndex, columnIndex];
        }

        public ResolveResult ResolvePlayerMove(BoardPosition position, PlayerSymbol playerSymbol) {
            if (IsPositionValid(position)) {
                _state[position.RowIndex, position.ColumnIndex] = playerSymbol;
                _emptyCells.Remove(position);

                var win = _winCheckStrategy.CheckWin(this);
                return new ResolveResult(_emptyCells.Count == 0, win);
            }

            throw new IndexOutOfRangeException($"Invalid move position: ({position.RowIndex}, {position.ColumnIndex})");
        }

        public bool IsPositionValid(BoardPosition position) => IsPositionValid(position.RowIndex, position.ColumnIndex);

        public bool IsPositionValid(int rowIndex, int columnIndex) =>
            rowIndex >= 0 && columnIndex >= 0 &&
            rowIndex < Size && columnIndex < Size;

        public void Reset() {
            InitializeState();
            InitializeEmptyCells();
        }
    }
}