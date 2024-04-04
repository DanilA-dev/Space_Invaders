using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Systems;
using Core.Model;
using Data;
using Entity;
using UnityEngine;

namespace Core.Behaviours
{
    [System.Serializable]
    public class EnemyEntityGetService
    {
        [SerializeField] private Transform _movingParent;
        [SerializeField] private Grid _grid;
        [SerializeField] private int _x;
        [SerializeField] private int _y;

        private IUnitEntitySpawner _unitEntitySpawner;
        private IUnitEntityRegisterService _unitEntityRegisterService;
        private List<EnemyEntity> _enemyEntities = new List<EnemyEntity>();

        public void Init(IUnitEntitySpawner unitEntitySpawner, IUnitEntityRegisterService unitEntityRegisterService)
        {
            _unitEntitySpawner = unitEntitySpawner;
            _unitEntityRegisterService = unitEntityRegisterService;
        }

        public async Task SpawnEnemies()
        {
            for (int x = 0; x < _x; x++)
            {
                for (int y = 0; y < _y; y++)
                {
                    var pos = _grid.GetCellCenterWorld(new Vector3Int(x, y));
                    var enemy = _unitEntitySpawner.SpawnEnemy((EnemyType)y, pos);
                    enemy.transform.SetParent(_movingParent);
                    _enemyEntities.Add(enemy);
                    await Task.Yield();
                }
            }
        }

        public void DespawnEnemies()
        {
            if(_enemyEntities.Count <= 0)
                return;
            
            foreach (var enemyEntity in _enemyEntities)
                enemyEntity.gameObject.SetActive(false);
        }

        public async Task RestoreEnemies()
        {
            foreach (var enemyEntity in _enemyEntities)
            {
                enemyEntity.Unit.CurrentHealth.Value = enemyEntity.Unit.MaxHealth;
                enemyEntity.gameObject.SetActive(true);
                _unitEntityRegisterService.Register(enemyEntity.Unit);
                await Task.Yield();
            }
        }
    }
}