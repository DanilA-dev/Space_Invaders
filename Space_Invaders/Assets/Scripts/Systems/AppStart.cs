using System;
using Systems.Interfaces;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class AppStart : MonoBehaviour
    {
        private GameState _gameState;
        private ISavePersistentDataService _persistentData;
        
        [Inject]
        private void Construct(GameState gameState, ISavePersistentDataService persistentData)
        {
            _gameState = gameState;
            _persistentData = persistentData;
        }
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            _gameState.State.Value = GameStateType.Menu;
            _persistentData.Load();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            _persistentData.Save();
        }

        private void OnApplicationQuit()
        {
            _persistentData.Save();
        }
    }
}