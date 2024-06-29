using TicTacToe.Domain;
using TicTacToe.Presentation.UiToolkit.Editor.CustomEvents;
using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class TopPanelElement : VisualElement {
        private const string TURN_TEXT_FORMAT = "{0} Turn";

        private readonly PlayerModeElement _xModeElement;
        private readonly PlayerModeElement _oModeElement;
        private readonly Label _turnLabel;

        public TopPanelElement(string xPlayerMode, string oPlayerMode) {
            this.AddStyle(Resources.Load<StyleSheet>("Styles/TopPanel"));
            this.AddToClassList("top-panel");

            _xModeElement = new PlayerModeElement();
            _xModeElement.SetSymbol("X");
            _xModeElement.SetMode(xPlayerMode);
            _oModeElement = new PlayerModeElement();
            _oModeElement.SetSymbol("O");
            _oModeElement.SetMode(oPlayerMode);

            var modePanel = new VisualElement();
            modePanel.AddToClassList("mode-panel");
            this.Add(modePanel);

            modePanel.Add(_xModeElement);
            modePanel.Add(_oModeElement);

            _turnLabel = new Label();
            _turnLabel.AddToClassList("turn-label");

            this.Add(_turnLabel);

            _xModeElement.RegisterCallback<ClickEvent>(evt => {
                evt.PreventDefault();

                OnModelElementClicked(PlayerSymbol.X);
            });

            _oModeElement.RegisterCallback<ClickEvent>(evt => {
                evt.PreventDefault();

                OnModelElementClicked(PlayerSymbol.O);
            });
        }

        private void OnModelElementClicked(PlayerSymbol symbol) {
            var modeClickedEvent = PlayerModeClickedEvent.GetEvent(symbol);
            modeClickedEvent.target = this;

            this.SendEvent(modeClickedEvent);
        }

        public void SetXMode(string xMode) {
            _xModeElement.SetMode(xMode);
        }

        public void SetOMode(string oMode) {
            _oModeElement.SetMode(oMode);
        }

        public void SetTurn(string playerSymbol) {
            _turnLabel.text = string.Format(TURN_TEXT_FORMAT, playerSymbol);
        }
    }
}