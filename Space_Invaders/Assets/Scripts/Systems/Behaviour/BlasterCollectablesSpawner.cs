using System.Collections.Generic;
using Systems.Factories;
using Core.Model;
using Core.Signals;
using Data;
using Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace Systems.Behaviour
{
    public class BlasterCollectablesSpawner : MonoBehaviour
    {
        [SerializeField] private List<BlasterCollectableEntityData> _blasterDatas;

        private readonly int _collectableCapacitty = 5;
        private readonly int _collectableMaxSize = 15;
        
        private BlasterCollectableFactory _factory;
        private ObjectPool<BlasterCollectableEntity> _blasterCollectablePool;
        private BlasterCollectableEntityData _currentData;
        
        private List<BlasterCollectableEntity> _createdCollectables = new List<BlasterCollectableEntity>();

        private GameState _gameState;
        private DiContainer _diContainer;
        
        [Inject]
        private void Construct(GameState gameState, DiContainer diContainer)
        {
            _gameState = gameState;
            _diContainer = diContainer;
            
            MessageBroker.Default.Receive<EnemyDestroySignal>()
                .Subscribe(_ => TrySpawnCollectable(_.EnemyUnit));

            _gameState.State
                .Where(s => s == GameStateType.Menu || s == GameStateType.LoseGame)
                .Subscribe(_ => DisableAllCollectables()).AddTo(gameObject);
        }

        private void Awake()
        {
            _factory = new BlasterCollectableFactory(_diContainer);
            
            _blasterCollectablePool = new ObjectPool<BlasterCollectableEntity>(SpawnCollectable,
                collectable => collectable.gameObject.SetActive(true),
                collectable => collectable.gameObject.SetActive(false),
                collectable => Destroy(collectable.gameObject),
                true, _collectableCapacitty, _collectableMaxSize);
        }

        private void TrySpawnCollectable(BaseUnit unit)
        {
            int rand = Random.Range(0, 101);
            foreach (var blasterData in _blasterDatas)
            {
                if (rand <= blasterData.DropChance)
                {
                    _currentData = blasterData;
                    var collecable = SpawnCollectable();
                    collecable.transform.position = unit.Position;
                }
            }
        }
        
        private void DisableAllCollectables()
        {
           if(_createdCollectables.Count <= 0)
               return;

           foreach (var collectable in _createdCollectables)
               _blasterCollectablePool.Release(collectable);
        }
        

        private BlasterCollectableEntity SpawnCollectable()
        {
            var collectable = _factory.Create(_currentData.EntityPrefab.gameObject);
            collectable.Init(_currentData.FallSpeed, _currentData.BlasterData, _blasterCollectablePool);
            _createdCollectables.Add(collectable);
            return collectable;
        }
    }
}