using UnityEngine;
using Zenject;

namespace UI
{
    public enum MenuType
    {
        None = 1,
        MainMenu = 2,
        CoreMenu = 3,
        PauseMenu = 4,
        LoseMenu = 5,
    }
    
    public abstract class BaseMenu : MonoBehaviour
    {
        protected GameState _gameState;
        
        public abstract MenuType MenuType { get; }

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }


    }
}