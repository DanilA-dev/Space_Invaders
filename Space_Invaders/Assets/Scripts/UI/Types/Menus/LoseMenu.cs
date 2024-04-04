using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseMenu : BaseMenu
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        public override MenuType MenuType => MenuType.LoseMenu;
        
        private void Start()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _menuButton.onClick.AddListener(ReturnToMenu);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _menuButton.onClick.RemoveListener(ReturnToMenu);
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
    }
}