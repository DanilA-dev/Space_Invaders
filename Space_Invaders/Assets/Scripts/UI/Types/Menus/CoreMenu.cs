using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CoreMenu : BaseMenu
    {
        [SerializeField] private Button _pauseButton;

        private void Start()
        {
            _pauseButton.onClick.AddListener(OpenPauseMenu);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OpenPauseMenu);
        }

        private void OpenPauseMenu()
        {
            _gameState.State.Value = GameStateType.Pause;
        }

        public override MenuType MenuType => MenuType.CoreMenu;
    }
}