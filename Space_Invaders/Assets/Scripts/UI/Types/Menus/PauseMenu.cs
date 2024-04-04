using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : BaseMenu
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ReturnToMenu);
            _closeButton.onClick.AddListener(ClosePauseMenu);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ReturnToMenu);
            _closeButton.onClick.RemoveListener(ClosePauseMenu);
        }

        private void ClosePauseMenu()
        {
            _gameState.State.Value = GameStateType.Gameplay;
        }

        private void ReturnToMenu()
        {
            _gameState.State.Value = GameStateType.Menu;
        }

        private void RestartGame()
        {
            _gameState.RestartLevel();
            _gameState.State.Value = GameStateType.Gameplay;
        }

        public override MenuType MenuType => MenuType.PauseMenu;
    }
}