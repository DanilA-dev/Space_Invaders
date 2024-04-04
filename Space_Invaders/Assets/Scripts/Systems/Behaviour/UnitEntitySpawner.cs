using System.Collections.Generic;
using Systems;
using Systems.Factories;
using Core.Behaviours;
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
        private IEnemiesMovementHandler _enemiesMovementHandler;

        private PlayerEntity _player;
        private List<EnemyEntity> _createdEnemies = new List<EnemyEntity>();

        [Inject]
        private void Construct(DiContainer diContainer, IUnitEntityRegisterService unitEntityRegisterService,
            IEnemiesMovementHandler enemiesMovementHandler)
        {
            _unitEntityRegisterService = unitEntityRegisterService;
            _enemiesMovementHandler = enemiesMovementHandler;
            _playerFactory = new PlayerFactory(diContainer);
            _enemyFactory = new EnemyFactory(diContainer);
        }

        private void Awake()
        {
            _unitEntityRegisterService.OnUnitDeregister += OnRemoveUnit;
        }

        private void OnDestroy()
        {
            _unitEntityRegisterService.OnUnitDeregister -= OnRemoveUnit;
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
                    _enemiesMovementHandler.AddEnemyEntity(enemyEntity);
                    return enemyEntity;
                }
            }

            return null;
        }

        public void DespawnPlayer()
        {
            Destroy(_player?.gameObject);
        }

        public void DespawnEnemies()
        {
            if(_createdEnemies.Count <= 0)
                return;
            
            foreach (var enemy in _createdEnemies)
                Destroy(enemy.gameObject);
            
            _createdEnemies.Clear();
        }
        
        private void OnRemoveUnit(BaseUnit unit)
        {
            TryRemoveDeadEnemy(unit);
        }

        private void TryRemoveDeadEnemy(BaseUnit unit)
        {
            if (_createdEnemies.Count <= 0)
                return;

            foreach (var enemyEntity in _createdEnemies)
            {
                if (enemyEntity.UnitOwner == unit)
                     enemyEntity.gameObject.SetActive(false);
            }
        }
    }
}
