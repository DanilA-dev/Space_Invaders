using System.Collections.Generic;
using Systems;
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
        private IUnitEntityRegisterService _unitEntityRegisterService;

        private PlayerEntity _player;
        private List<EnemyEntity> _createdEnemies = new List<EnemyEntity>();

        [Inject]
        private void Construct(DiContainer diContainer, IUnitEntityRegisterService unitEntityRegisterService)
        {
            _unitEntityRegisterService = unitEntityRegisterService;
            _playerFactory = new PlayerFactory(diContainer);
            _enemyFactory = new EnemyFactory(diContainer);
        }

        public PlayerEntity SpawnPlayer(Vector3 pos)
        {
            var playerUnit = new PlayerUnit(_playerEntityData.MaxHealth, _playerEntityData.Damage, pos);
            _player = _playerFactory.Create(_playerEntityData.EntityPrefab.gameObject);
            _player.transform.position = pos;
            _player.gameObject.SetActive(true);
            _player.Init(playerUnit);
            _unitEntityRegisterService.Register(playerUnit);
            return _player;
        }

        public EnemyEntity SpawnEnemy(EnemyType type, Vector3 pos)
        {
            foreach (var entityData in _enemyEntitiesDatas)
            {
                if (entityData.Type == type)
                {
                    var enemyUnit = new EnemyUnit(entityData.MaxHealth, entityData.Damage, pos ,entityData.Type, entityData.Score);
                    var enemyEntity =  _enemyFactory.Create(entityData.EntityPrefab.gameObject);
                    enemyEntity.transform.position = pos;
                    enemyEntity.Init(enemyUnit);
                    _unitEntityRegisterService.Register(enemyUnit);
                    _createdEnemies.Add(enemyEntity);
                    return enemyEntity;
                }
            }

            return null;
        }
      
    }
}
