using Systems;
using Core.Interfaces;
using Core.Model;
using Zenject;

namespace Entity
{
    public abstract class BaseUnitEntity<T> : BaseEntity, IDamagable where T : BaseUnit
    {
        protected GameState _gameState;
        private IUnitEntityRegisterService _unitRegisterService;
        
        public T Unit { get; private set; }
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
            if (Unit.CurrentHealth.Value <= 0)
            {
                _unitRegisterService.Deregister(Unit);
                OnDestroyUnit();
            }
        }
        protected virtual void OnDestroyUnit() {}
        protected virtual void OnUpdate() {}
    }
}