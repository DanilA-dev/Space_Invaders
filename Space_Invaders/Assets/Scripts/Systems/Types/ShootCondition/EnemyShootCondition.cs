using System;
using Core.Model;

namespace Systems
{
    public class EnemyShootCondition : IShootCondition
    {
        private PlayerUnit _playerUnit;
        private EnemyUnit _ownerUnit;
        
        public void Init(IUnitEntityRegisterService unitEntityRegisterService, BaseUnit ownerUnit)
        {
            _playerUnit = unitEntityRegisterService.GetUnit<PlayerUnit>();
            _ownerUnit = (EnemyUnit)ownerUnit;
        }

        private bool IsPlayerSameXCoord() =>  Math.Abs(_playerUnit.Position.x - _ownerUnit.Position.x) < 0.1f;

        private bool IsBelowEnemyDead() => _ownerUnit.BellowEnemy == null || _ownerUnit.BellowEnemy.IsDead;
        
        public bool CanShoot()
        {
            return IsPlayerSameXCoord() && IsBelowEnemyDead();
        }
    }
}