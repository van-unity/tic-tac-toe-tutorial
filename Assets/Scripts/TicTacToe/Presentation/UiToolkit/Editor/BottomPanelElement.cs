using TicTacToe.Presentation.UiToolkit.Editor.CustomEvents;
using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class BottomPanelElement : VisualElement {
        private VisualElement _startButton;
        private VisualElement _restartButton;
        
        public BottomPanelElement() {
            this.AddStyle(Resources.Load<StyleSheet>("Styles/BottomPanel"));
            this.AddToClassList("bottom-panel");

            _startButton = new Button() {
                text = "Start"
            };

            _startButton.AddToClassList("button");

            _restartButton = new Button() {
                text = "Restart"
            };

            _restartButton.AddToClassList("button");

            _startButton.RegisterCallback<ClickEvent>(evt => {
                evt.PreventDefault();

                var startClickedEvent = StartClickedEvent.GetEvent();

                startClickedEvent.target = this;

                this.SendEvent(startClickedEvent);
            });

            _restartButton.RegisterCallback<ClickEvent>(evt => {
                evt.PreventDefault();

                var restartClickedEvent = RestartClickedEvent.GetEvent();

                restartClickedEvent.target = this;

                this.SendEvent(restartClickedEvent);
            });
        }

        public void HideStartButton() {
            this.Remove(_startButton);
        }

        public void HideRestartButton() {
            this.Remove(_restartButton);
        }

        public void ShowStartButton() {
            this.Add(_startButton);
        }
        
        public void ShowRestartButton() {
            this.Add(_restartButton);
        }
    }
}