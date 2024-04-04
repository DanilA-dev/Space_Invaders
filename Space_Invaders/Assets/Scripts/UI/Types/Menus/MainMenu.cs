using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : BaseMenu
    {
        [SerializeField] private Button _playButton;
        
        private void Start()
        {
            _playButton.onClick.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            _gameState.State.Value = GameStateType.Gameplay;
            
            if(_gameState.IsLevelCreated)
                _gameState.RestartLevel();
            else
                _gameState.LevelStart();
        }

        public override MenuType MenuType => MenuType.MainMenu;
    }
}