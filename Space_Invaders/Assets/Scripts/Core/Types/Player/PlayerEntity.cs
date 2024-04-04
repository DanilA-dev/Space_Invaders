using Systems.Behaviour;
using Core.Model;
using UnityEngine;
using View;

namespace Entity
{
    [RequireComponent(typeof(ShootHandler),
        typeof(InputMovementHandler))]
    public class PlayerEntity : BaseUnitEntity<PlayerUnit>
    {
        [SerializeField] private PlayerView _view;
        
        private ShootHandler _shootHandler;
        private InputMovementHandler _inputMovementHandler;
        private Transform _transform;
        public override BaseEntityView View => _view;
        
        private void Awake()
        {
            _transform = transform;
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

            Move();
        }

        private void Move()
        {
            BaseUnit playerUnit = Unit;
            _inputMovementHandler.Move(ref playerUnit);
            _transform.position = playerUnit.Position;
        }

        protected override void OnKillUnit()
        {
            _gameState.State.Value = GameStateType.LoseGame;
            gameObject.SetActive(false);
        }
    }
}