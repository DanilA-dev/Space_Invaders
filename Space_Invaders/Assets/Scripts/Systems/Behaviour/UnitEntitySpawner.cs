using System.Collections.Generic;
using Systems.Factories;
using Core.Model;
using Data;
using Entity;
using UnityEngine;
using Zenject;

namespace System
{
    public class UnitEntitySpawner : MonoBehaviour, IUnitEntitySpawner
    {
        [SerializeField] private PlayerEntityData _playerEntityData;
        [SerializeField] private List<EnemyEntityData> _enemyEntitiesDatas;

        private PlayerFactory _playerFactory;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _playerFactory = new PlayerFactory(diContainer);
            _enemyFactory = new EnemyFactory(diContainer);
        }
        
        public PlayerEntity SpawnPlayer(Vector3 pos)
        {
            var playerUnit = new PlayerUnit(_playerEntityData.MaxHealth, _playerEntityData.Damage);
            var playerEntity = _playerFactory.Create(_playerEntityData.EntityPrefab.gameObject);
            playerEntity.transform.position = pos;
            playerEntity.Init(playerUnit);
            return playerEntity;
        }

        public EnemyEntity SpawnEnemy(EnemyType type, Vector3 pos)
        {
            foreach (var entityData in _enemyEntitiesDatas)
            {
                if (entityData.Type == type)
                {
                    var enemyUnit = new EnemyUnit(entityData.MaxHealth, entityData.Damage, entityData.Type);
                    var enemyEntity =  _enemyFactory.Create(entityData.EntityPrefab.gameObject);
                    enemyEntity.transform.position = pos;
                    enemyEntity.Init(enemyUnit);
                    return enemyEntity;
                }
            }

            return null;
        }
        
        
    }
}
