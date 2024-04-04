using Systems.Behaviour;
using Core.Model;
using Core.Signals;
using UniRx;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(ShootHandler))]
    public class EnemyEntity : BaseUnitEntity<EnemyUnit>
    {
        private ShootHandler _shootHandler;
        private void Awake()
        {
            _shootHandler = GetComponent<ShootHandler>();
        }

        public override void Init(EnemyUnit unit)
        {
            base.Init(unit);
            _shootHandler.Init(unit);
        }

        protected override void OnUpdate()
        {
            _shootHandler?.OnUpdate();
        }

        protected override void OnDestroyUnit()
        {
            MessageBroker.Default.Publish(new AddScoreSignal(Unit.ScoreForDeath));
        }
    }
}