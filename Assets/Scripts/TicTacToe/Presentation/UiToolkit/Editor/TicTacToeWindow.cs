using TicTacToe.Domain;
using TicTacToe.Domain.EventArgs;
using TicTacToe.Presentation.UiToolkit.Editor.CustomEvents;
using TicTacToe.Presentation.UiToolkit.Editor.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class TicTacToeWindow : EditorWindow {
        private BoardElement _boardElement;
        private TopPanelElement _topPanelElement;
        private BottomPanelElement _bottomPanelElement;
        private PopupManager _popupManager;
        private TicTacToeViewModel _viewModel;

        [MenuItem("TicTacToe/Play")]
        private static void ShowWindow() {
            var window = GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent("TicTacToe");
            window.Show();
        }

        private void OnEnable() {
            TicTacToeContext.Initialize();
            SetViewModel(TicTacToeContext.ViewModel);
        }

        private void CreateGUI() {
            this.minSize = new Vector2(377, 613);
            this.maxSize = this.minSize;
            rootVisualElement.Clear();
            rootVisualElement.AddStyle(Resources.Load<StyleSheet>("Styles/Root"));
            rootVisualElement.AddToClassList("root");
            _topPanelElement =
                new TopPanelElement(_viewModel.XPlayerMode.ToString(), _viewModel.OPlayerMode.ToString());
            _topPanelElement.RegisterCallback<PlayerModeClickedEvent>(OnPlayerModeClicked);

            var boardContainer = new VisualElement() {
                name = "BoardContainer"
            };
            boardContainer.AddToClassList("board-container");

            _boardElement = new BoardElement(3);
            _boardElement.RegisterCallback<CellClickedEvent>(OnCellClicked);

            boardContainer.Add(_boardElement);

            _bottomPanelElement = new BottomPanelElement();
            _bottomPanelElement.RegisterCallback<StartClickedEvent>(OnStartClicked);
            _bottomPanelElement.RegisterCallback<RestartClickedEvent>(OnRestartClicked);

            if (_viewModel.IsGameStarted) {
                _bottomPanelElement.ShowRestartButton();
            } else {
                _bottomPanelElement.ShowStartButton();
            }

            _popupManager = new PopupManager();

            rootVisualElement.Add(_topPanelElement);
            rootVisualElement.Add(boardContainer);
            rootVisualElement.Add(_bottomPanelElement);
            rootVisualElement.Add(_popupManager);
        }

        private void OnRestartClicked(RestartClickedEvent evt) {
            _viewModel.HandleRestart();

            CreateGUI();
        }

        private void OnStartClicked(StartClickedEvent evt) {
            _viewModel.HandleStart();

            _bottomPanelElement.HideStartButton();
            _bottomPanelElement.ShowRestartButton();
        }

        private void OnPlayerModeClicked(PlayerModeClickedEvent evt) {
            _viewModel.TogglePlayerMode(evt.PlayerSymbol);
        }

        private void SetViewModel(TicTacToeViewModel viewModel) {
            _viewModel = viewModel;

            _viewModel.CellUpdated += OnCellUpdated;
            _viewModel.GameEnded += OnGameEnded;
            _viewModel.PlayerModeChanged += OnPlayerModeChanged;
            _viewModel.TurnChanged += OnTurnChanged;
        }

        private void OnTurnChanged(TurnChangedEventArgs turnChangedEventArgs) {
            _topPanelElement.SetTurn(turnChangedEventArgs.CurrentPlayerSymbol.ToString());
        }

        private void OnPlayerModeChanged(PlayerModeChangedEventArgs playerModeChangedEventArgs) {
            switch (playerModeChangedEventArgs.PlayerSymbol) {
                case PlayerSymbol.O:
                    _topPanelElement.SetOMode(playerModeChangedEventArgs.NewMode.ToString());
                    break;
                case PlayerSymbol.X:
                    _topPanelElement.SetXMode(playerModeChangedEventArgs.NewMode.ToString());
                    break;
            }
        }

        private void OnGameEnded(GameEndedEventArgs gameEndedEventArgs) {
            if (gameEndedEventArgs.Win == null) {
                _popupManager.ShowDraw();
                return;
            }

            var win = gameEndedEventArgs.Win;

            _boardElement.DrawWinningLine(win.WinPositions[0], win.WinPositions[^1]);

            rootVisualElement.schedule
                .Execute(() => { _popupManager.ShowWin(win.Symbol.ToString()); })
                .ExecuteLater(1000);
        }

        private void OnCellUpdated(CellUpdatedEventArgs obj) {
            _boardElement.UpdateCell(obj.Position, obj.Symbol);
        }

        private void OnCellClicked(CellClickedEvent evt) {
            _viewModel.HandleCellClick(evt.ClickPosition);
        }

        private void OnDisable() {
            if (_viewModel != null) {
                _viewModel.CellUpdated -= OnCellUpdated;
                _viewModel.GameEnded -= OnGameEnded;
                _viewModel.PlayerModeChanged -= OnPlayerModeChanged;
                _viewModel.TurnChanged -= OnTurnChanged;
            }

            TicTacToeContext.Dispose();
        }
    }
}