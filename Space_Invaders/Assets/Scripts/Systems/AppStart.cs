using UnityEngine;
using Zenject;

namespace Systems
{
    public class AppStart : MonoBehaviour
    {
        private GameState _gameState;

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            _gameState.State.Value = GameStateType.Menu;
        }
    }
}