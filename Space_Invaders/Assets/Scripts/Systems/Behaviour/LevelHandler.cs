using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Behaviours;
using Core.Model;
using Core.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private PlayerEntityGetService _playerEntityGetService;
        [SerializeField] private EnemyEntityGetService _enemyEntityGetService;
        
        private GameState _gameState;
        private IUnitEntitySpawner _unitEntitySpawner;
        private IUnitEntityRegisterService _unitEntityRegisterService;
        private List<EnemyUnit> _registeredEnemies;
        
        [Inject]
        private void Construct(GameState gameState, IUnitEntitySpawner unitEntitySpawner, IUnitEntityRegisterService registerService)
        {
            _unitEntitySpawner = unitEntitySpawner;
            _unitEntityRegisterService = registerService;
            _gameState = gameState;
        }

        private void Awake()
        {
            _playerEntityGetService.Init(_unitEntitySpawner, _unitEntityRegisterService);
            _enemyEntityGetService.Init(_unitEntitySpawner, _unitEntityRegisterService);
            
            _gameState.State
                .Where(s => s == GameStateType.Menu)
                .Subscribe(_ => ClearLevel()).AddTo(gameObject);
            
            _gameState.OnLevelStart += CreateLevel;
            _gameState.OnLevelRestarted += RestartLevel;

            MessageBroker.Default.Receive<EnemyDestroySignal>()
                .Subscribe(_ => OnEnemyKilled(_.EnemyUnit)).AddTo(gameObject);
        }

        private async void CreateLevel()
        {
            await CreatePlayer();
            await CreateEnemies();
            _gameState.InitializeLevel();
        }

        private async Task CreateEnemies()
        {
            await _enemyEntityGetService.SpawnEnemies();
        }

        private async Task CreatePlayer()
        {
            await _playerEntityGetService.SpawnPlayer();
        }
        
        private void RestartLevel()
        {
            _playerEntityGetService.RestorePlayer();
            _enemyEntityGetService.RestoreEnemies();
        }
        
        private void ClearLevel()
        {
            _enemyEntityGetService.DespawnEnemies();
            _playerEntityGetService.DespawnPlayer();
        }
        
        private void OnEnemyKilled(BaseUnit unit)
        {
            _registeredEnemies = _unitEntityRegisterService.GetUnits<EnemyUnit>();
            if(_registeredEnemies.All(u => u.IsDead))
                RestartLevel();
        }
    }
}