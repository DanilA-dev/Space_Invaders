using System;
using System.Threading.Tasks;
using Systems;
using Entity;
using UnityEngine;
using Zenject;

namespace Core.Behaviours
{
    [System.Serializable]
    public class PlayerEntityGetService
    {
        [SerializeField] private Transform _spawnPos;

        private PlayerEntity _player;
        
        private IUnitEntitySpawner _unitEntitySpawner;
        private IUnitEntityRegisterService _unitEntityRegisterService;

        public void Init(IUnitEntitySpawner unitEntitySpawner, IUnitEntityRegisterService unitEntityRegisterService)
        {
            _unitEntitySpawner = unitEntitySpawner;
            _unitEntityRegisterService = unitEntityRegisterService;
        }
        
        public async Task SpawnPlayer()
        {
            _player = _unitEntitySpawner.SpawnPlayer(_spawnPos.position);
            await Task.Yield();
        }

        public void DespawnPlayer()
        {
            _player?.gameObject.SetActive(false);
        }

        public void RestorePlayer()
        {
            var unit = _player.Unit;
            _player.Unit.CurrentHealth.Value = unit.MaxHealth;
            _player.gameObject.SetActive(true);
            _unitEntityRegisterService.Register(unit);
        }

    }
}