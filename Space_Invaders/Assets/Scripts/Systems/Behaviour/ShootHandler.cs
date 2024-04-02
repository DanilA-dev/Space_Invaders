using System;
using System.Collections;
using Core.Model;
using Data;
using Entity;
using UnityEngine;
using UnityEngine.Pool;

namespace Systems.Behaviour
{
    public class ShootHandler : MonoBehaviour
    {
        [Header("Shoot settings")] 
        [SerializeField] private bool _shootOnInit;
        [SerializeField] private Transform _shootPos;
        [SerializeField] private BlasterData _blasterData;
        [SerializeField] private Vector2 _shootDirection;
        [SerializeField] private BulletEntityData _bulletEntityData;
        [SerializeField] private int _bulletsCapacity;
        [SerializeField] private int _bulletsMaxSize;

        private BaseUnit _owner;
        
        //[TODO] Move out to seperate class or unit spawner???
        private ObjectPool<BulletEntity> _bulletsPool;

        public void Init(BaseUnit unit)
        {
            _owner = unit;
            _bulletsPool =new ObjectPool<BulletEntity>(CreateBullet, 
                bullet => bullet.gameObject.SetActive(true),
                bullet => bullet.gameObject.SetActive(false),
                bullet => Destroy(bullet.gameObject),
                false, _bulletsCapacity, _bulletsMaxSize);

            if (_shootOnInit)
                StartCoroutine(StartShooting());
        }

        private IEnumerator StartShooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_blasterData.RateOfFire);
                for (int i = 0; i < _blasterData.BulletsPerFire; i++)
                {
                    var bullet = _bulletsPool.Get();
                    bullet.transform.position = _shootPos.position;
                }
            }
        }

        private BulletEntity CreateBullet()
        {
            var bullet = (BulletEntity)Instantiate(_bulletEntityData.EntityPrefab);
            bullet.Init(_owner,_shootDirection, _bulletEntityData.Speed, _bulletsPool);
            return bullet;
        }

        private void Update()
        {
            
        }
    }
}