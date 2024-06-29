using TicTacToe.Presentation.UiToolkit.Editor.CustomEvents;
using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class PopupManager : VisualElement {
        private const string WIN_TEXT_FORMAT = "{0} WON!";
        private const string DRAW_TEXT_FORMAT = "X/O\nDRAW!";

        private readonly VisualElement _backdrop;
        private readonly ResultPopup _resultPopup;

        public PopupManager() {
            this.AddStyle(Resources.Load<StyleSheet>("Styles/PopupManager"));
            this.AddToClassList("popup-manager");
            this.visible = false;
            _backdrop = new VisualElement();
            _backdrop.AddToClassList("backdrop");

            _backdrop.RegisterCallback<ClickEvent>(evt => { HideResultPopup(); });

            _resultPopup = new ResultPopup();
            _resultPopup.Hide();

            this.Add(_backdrop);
            this.Add(_resultPopup);
        }

        public void ShowWin(string winner) {
            visible = true;
            SetBackdropEnabled(true);

            _resultPopup.SetResultText(string.Format(WIN_TEXT_FORMAT, winner));
            _resultPopup.Show();
        }

        public void ShowDraw() {
            visible = true;
            SetBackdropEnabled(true);

            _resultPopup.SetResultText(DRAW_TEXT_FORMAT);
            _resultPopup.Show();
        }

        private void HideResultPopup() {
            SetBackdropEnabled(false);
            _resultPopup.Hide();
            this.schedule
                .Execute(() => { visible = false; })
                .ExecuteLater(500);
        }

        private void SetBackdropEnabled(bool isBackdropEnabled) {
            if (isBackdropEnabled) {
                _backdrop.AddToClassList("backdrop-show");
            } else {
                _backdrop.RemoveFromClassList("backdrop-show");
            }
        }
    }
}