using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class CellElement : VisualElement {
        private readonly Label _symbolLabel;

        public CellElement() {
            this.SetStyle(Resources.Load<StyleSheet>("Styles/Cell"));
            this.AddToClassList("cell");

            _symbolLabel = new Label();
            _symbolLabel.AddToClassList("symbol-label");
            this.Add(_symbolLabel);
        }

        public void SetSymbol(string symbol) {
            _symbolLabel.text = symbol;
        }

        public void SetSymbolSize(int fontSize) {
            _symbolLabel.style.fontSize = fontSize;
        }

        public void Show() {
            this.RemoveFromClassList("cell-hidden");
            this.AddToClassList("cell-shown");
        }

        public void Hide() {
            this.AddToClassList("cell-hidden");
            this.RemoveFromClassList("cell-shown");
        }
    }
}