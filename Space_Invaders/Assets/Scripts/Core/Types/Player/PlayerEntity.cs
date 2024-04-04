using Systems.Behaviour;
using Core.Model;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(ShootHandler),
        typeof(InputMovementHandler))]
    public class PlayerEntity : BaseUnitEntity<PlayerUnit>
    {
        private ShootHandler _shootHandler;
        private InputMovementHandler _inputMovementHandler;

        private void Awake()
        {
            _shootHandler = GetComponent<ShootHandler>();
            _inputMovementHandler = GetComponent<InputMovementHandler>();
        }

        public override void Init(PlayerUnit unit)
        {
            base.Init(unit);
            _shootHandler.Init(unit);
        }

        protected override void OnUpdate()
        {
            _shootHandler?.OnUpdate();
        }

        private void LateUpdate()
        {
            if(_gameState.State.Value != GameStateType.Gameplay)
                return;

            var playerUnit = Unit;
            _inputMovementHandler.Move(ref playerUnit);
        }

        protected override void OnDestroyUnit()
        {
            _gameState.State.Value = GameStateType.LoseGame;
        }
    }
}