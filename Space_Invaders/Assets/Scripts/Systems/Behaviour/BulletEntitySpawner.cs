using Core.Model;
using Data;
using Entity;
using UnityEngine;
using UnityEngine.Pool;

namespace Systems
{
    [System.Serializable]
    public class BulletEntitySpawner
    {
        [SerializeField] private BulletEntityData _bulletEntityData;
        [SerializeField] private int _bulletsCapacity;
        [SerializeField] private int _bulletsMaxSize;
        
        private ObjectPool<BulletEntity> _bulletsPool;
        private BaseUnit _owner;
        private Vector2 _moveDirection;
        
        
        public void Init(BaseUnit owner, Vector2 moveDirection)
        {
            _owner = owner;
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
            var bullet = (BulletEntity)GameObject.Instantiate(_bulletEntityData.EntityPrefab);
            bullet.Init(_owner,_moveDirection, _bulletEntityData.Speed, _bulletsPool);
            return bullet;
        }
        
    }
}