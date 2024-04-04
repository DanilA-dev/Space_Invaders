using System;
using Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [Space] 
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        [SerializeField] private Grid _grid;
        
        private GameState _gameState;
        private IUnitEntitySpawner _unitEntitySpawner;
        
        [Inject]
        private void Construct(GameState gameState, IUnitEntitySpawner unitEntitySpawner)
        {
            _unitEntitySpawner = unitEntitySpawner;
            _gameState = gameState;
        }

        private void Awake()
        {
            _gameState.State
                .Where(s => s == GameStateType.Menu)
                .Subscribe(_ => ClearLevel()).AddTo(gameObject);
            
            _gameState.OnLevelRestarted += CreateLevel;
        }

        private void CreateLevel()
        {
            ClearLevel();
            CreatePlayer();
            CreateEnemies();
            _gameState.InitializeLevel();
        }

        private void CreateEnemies()
        {
            for (int x = 0; x < _x; x++)
            {
                for (int y = 0; y < _y; y++)
                {
                    var pos = _grid.GetCellCenterWorld(new Vector3Int(x, y));
                    var enemy = _unitEntitySpawner.SpawnEnemy((EnemyType)y, pos);
                }
            }
        }

        private void CreatePlayer()
        {
            _unitEntitySpawner.SpawnPlayer(_playerSpawnPoint.position);
        }

        private void ClearLevel()
        {
            _unitEntitySpawner.DespawnPlayer();
            _unitEntitySpawner.DespawnEnemies();
        }
    }
}