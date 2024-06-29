using TicTacToe.Presentation.UiToolkit.Editor.CustomEvents;
using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class ResultPopup : VisualElement {
        private readonly Label _resultLabel;

        public ResultPopup() {
            this.AddStyle(Resources.Load<StyleSheet>("Styles/ResultPopup"));
            this.AddToClassList("result-popup");

            _resultLabel = new Label();
            _resultLabel.AddToClassList("result-label");

            this.Add(_resultLabel);
        }

        public void Show() {
            RemoveFromClassList("result-popup-hide");
            AddToClassList("result-popup-show");
        }

        public void Hide() {
            RemoveFromClassList("result-popup-show");
            AddToClassList("result-popup-hide");
        }

        public void SetResultText(string resultText) {
            _resultLabel.text = resultText;
        }
    }
}