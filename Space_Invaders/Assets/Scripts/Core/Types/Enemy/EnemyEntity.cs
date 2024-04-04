using System;
using Systems.Behaviour;
using Core.Behaviours;
using Core.Model;
using Core.Signals;
using UniRx;
using UnityEngine;
using View;
using Zenject;

namespace Entity
{
    [RequireComponent(typeof(ShootHandler))]
    public class EnemyEntity : BaseUnitEntity<EnemyUnit>
    {
        [SerializeField] private EnemyNeighbourDetection _enemyNeighbourDetection;
        [SerializeField] private EnemyView _view;

        private ShootHandler _shootHandler;
        public override BaseEntityView View => _view;

        [Inject]
        private void Construct()
        {
            _gameState.OnLevelInitialized += TryDetectBelowUnit;
        }
        
        private void Awake()
        {
            _shootHandler = GetComponent<ShootHandler>();
        }

        protected override void OnUpdate()
        {
            _shootHandler?.OnUpdate();
            TryDetectBelowUnit();
        }


        public override void Init(EnemyUnit unit)
        {
            base.Init(unit);
            _shootHandler.Init(unit);
        }


        private void TryDetectBelowUnit()
        {
            if (_enemyNeighbourDetection.TryDetectBelowNeighbour(out var enemyUnitBellow))
                Unit.BellowEnemy = enemyUnitBellow;
        }
        
        

        protected override void OnKillUnit()
        {
            MessageBroker.Default.Publish(new AddScoreSignal(Unit.ScoreForDeath));
            MessageBroker.Default.Publish(new EnemyDestroySignal(Unit));
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            _enemyNeighbourDetection?.OnDrawGizmos();
        }
    }
}