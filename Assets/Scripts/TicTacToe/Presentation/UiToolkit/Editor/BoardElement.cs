using TicTacToe.Domain;
using TicTacToe.Presentation.UiToolkit.Editor.CustomEvents;
using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class BoardElement : VisualElement {
        private const float GRID_LINE_OFFSET = .1f;

        private readonly VisualElement _gridLinesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly VisualElement _winningLineContainer;
        private readonly int _size;

        private float _cellWidth;
        private float _cellHeight;
        private int _symbolSize;

        public BoardElement(int size) {
            _size = size;
            this.SetStyle(Resources.Load<StyleSheet>("Styles/Board"));
            this.AddToClassList("board");
            _gridLinesContainer = new VisualElement();
            _gridLinesContainer.AddToClassList("grid-lines-container");
            _cellsContainer = new VisualElement();
            _cellsContainer.AddToClassList("cells-container");
            _winningLineContainer = new VisualElement();
            _winningLineContainer.AddToClassList("winning-lines-container");
            this.Add(_cellsContainer);
            this.Add(_gridLinesContainer);
            this.Add(_winningLineContainer);
            this.RegisterCallback<ClickEvent>(OnClick);
            this.RegisterCallback<AttachToPanelEvent>(evt => Initialize());
        }
        
        private void OnClick(ClickEvent clickEvent) {
            clickEvent.PreventDefault();
            var boardPos = PixelToLogicPos(clickEvent.localPosition);
            var cellClickedEvent = CellClickedEvent.GetEvent(boardPos);
            cellClickedEvent.target = this;
            this.SendEvent(cellClickedEvent);
        }

        public void UpdateCell(BoardPosition position, PlayerSymbol playerSymbol) {
            var pixelPos = LogicToPixelPos(position);

            var cell = new CellElement {
                style = {
                    width = _cellWidth,
                    height = _cellHeight,
                    left = pixelPos.x,
                    top = pixelPos.y
                }
            };
            cell.SetSymbol(playerSymbol.ToString());
            cell.SetSymbolSize(_symbolSize);
            cell.Hide();
            _cellsContainer.Add(cell);
            this.schedule
                .Execute(cell.Show)
                .ExecuteLater(16);
        }

        public void Initialize() {
            this.schedule.Execute(() => {
                _cellWidth = layout.width / _size;
                _cellHeight = layout.height / _size;
                _symbolSize = Mathf.CeilToInt(Mathf.Min(_cellWidth, _cellHeight));
                DrawGrid();
            }).ExecuteLater(1000);
        }

        private void DrawGrid() {
            _gridLinesContainer.Clear();
            for (int columnIndex = 1; columnIndex < _size; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(_size, columnIndex));
                from.y += GRID_LINE_OFFSET * _cellHeight;
                to.y -= GRID_LINE_OFFSET * _cellHeight;
                var line = new LineElement(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateLineLength(line, line.Length, (columnIndex - 1) * 250);
            }

            for (int rowIndex = 1; rowIndex < _size; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, _size));
                from.x += GRID_LINE_OFFSET * _cellWidth;
                to.x -= GRID_LINE_OFFSET * _cellWidth;
                var line = new LineElement(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateLineLength(line, line.Length, (rowIndex - 1) * 250);
            }
        }

        private void AnimateLineLength(LineElement line, float width, int delayMS = 0) {
            line.Length = 0;
            line.schedule
                .Execute(() => { line.Length = width; })
                .ExecuteLater(16 + delayMS);
        }

        public void DrawWinningLine(BoardPosition from, BoardPosition to) {
            var fromPixel = LogicToPixelPos(from);
            var toPixel = LogicToPixelPos(to);
            fromPixel.x += _cellWidth * .5f;
            fromPixel.y += _cellHeight * .5f;
            toPixel.x += _cellWidth * .5f;
            toPixel.y += _cellHeight * .5f;
            var line = new LineElement(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _winningLineContainer.Add(line);
            AnimateLineLength(line, line.Length);
        }

        private Vector2 LogicToPixelPos(BoardPosition logicPos) =>
            new(logicPos.ColumnIndex * _cellWidth,
                logicPos.RowIndex * _cellHeight);

        private BoardPosition PixelToLogicPos(Vector2 pixelPos) {
            var rowIndex = Mathf.CeilToInt(pixelPos.y / _cellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / _cellWidth) - 1;

            return new BoardPosition(rowIndex, columnIndex);
        }

        public void Reset() {
            _winningLineContainer.Clear();
            _cellsContainer.Clear();
        }
    }
}