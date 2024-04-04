using System.Collections;
using Core.Model;
using Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Systems.Behaviour
{
    public class ShootHandler : SerializedMonoBehaviour
    {
        [Header("Shoot settings")] 
        [SerializeField] private IShootCondition _shootCondition;
        [SerializeField] private Transform _shootPos;
        [SerializeField] private BlasterData _blasterData;
        [SerializeField] private Vector2 _shootDirection;
        [SerializeField] private BulletEntitySpawner _bulletEntitySpawner;

        private bool _isShooting;
        private IUnitEntityRegisterService _unitEntityRegisterService;
        
        [Inject]
        private void Construct(IUnitEntityRegisterService unitEntityRegisterService)
        {
            _unitEntityRegisterService = unitEntityRegisterService;
        }
        
        public void Init(BaseUnit owner)
        {
            _shootCondition.Init(_unitEntityRegisterService,owner);
           _bulletEntitySpawner.Init(owner, _shootDirection);
        }

        public void OnUpdate()
        {
            TryStartShooting(_shootCondition.CanShoot());
        }

        private void TryStartShooting(bool condition)
        {
            if (condition && !_isShooting)
                StartCoroutine(StartShooting());
        }
        
        private IEnumerator StartShooting()
        {
            _isShooting = true;
            yield return new WaitForSeconds(_blasterData.RateOfFire);
            for (int i = 0; i < _blasterData.BulletsPerFire; i++)
            { 
                var bullet = _bulletEntitySpawner.Get();
                bullet.transform.position = _shootPos.position;
            }
            _isShooting = false;
        }
    }
}