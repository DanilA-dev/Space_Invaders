using System;
using Systems;
using Core.Interfaces;
using Core.Model;
using View;
using Zenject;

namespace Entity
{
    public abstract class BaseUnitEntity<T> : BaseEntity, IDamagable where T : BaseUnit
    {
        protected GameState _gameState;
        private IUnitEntityRegisterService _unitRegisterService;
        
        public T Unit { get; private set; }
        public abstract BaseEntityView View { get; }
        public BaseUnit UnitOwner => Unit;

    
        [Inject]
        private void Construct(GameState gameState, IUnitEntityRegisterService unitRegisterService)
        {
            _gameState = gameState;
            _unitRegisterService = unitRegisterService;
        }

        private void Update()
        {
            if(_gameState.State.Value != GameStateType.Gameplay)
                return;
            
            OnUpdate();
        }

        public virtual void Init(T unit)
        {
            Unit = unit;
        }


        public void Damage(int damageValue)
        {
            Unit.CurrentHealth.Value -= damageValue;
            View?.InvokeDamageTween();
            if (Unit.CurrentHealth.Value <= 0)
            {
                OnKillUnit();
                _unitRegisterService.Deregister(Unit);
            }
        }
        protected virtual void OnKillUnit() {}
        protected virtual void OnUpdate() {}
    }
}