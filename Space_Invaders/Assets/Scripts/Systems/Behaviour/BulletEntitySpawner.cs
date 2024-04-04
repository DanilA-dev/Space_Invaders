using Systems.Factories;
using Core.Model;
using Data;
using Entity;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Systems
{
    [System.Serializable]
    public class BulletEntitySpawner
    {
        [SerializeField] private BulletEntityData _bulletEntityData;
        [SerializeField] private int _bulletsCapacity;
        [SerializeField] private int _bulletsMaxSize;
        
        private ObjectPool<BulletEntity> _bulletsPool;
        private BulletFactory _bulletFactory;
        private BaseUnit _owner;
        private Vector2 _moveDirection;
        
        
        public void Init(BaseUnit owner, Vector2 moveDirection, DiContainer diContainer)
        {
            _owner = owner;
            _bulletFactory = new BulletFactory(diContainer);
            _moveDirection = moveDirection;
            
            _bulletsPool = new ObjectPool<BulletEntity>(CreateBullet, 
                bullet => bullet.gameObject.SetActive(true),
                bullet => bullet.gameObject.SetActive(false),
                bullet => GameObject.Destroy(bullet.gameObject),
                true, _bulletsCapacity, _bulletsMaxSize);
        }

        public BulletEntity Get() => _bulletsPool.Get();
        private BulletEntity CreateBullet()
        {
            var bullet = _bulletFactory.Create(_bulletEntityData.EntityPrefab.gameObject);
            bullet.Init(_owner,_moveDirection, _bulletEntityData.Speed, _bulletsPool);
            return bullet;
        }
        
    }
}