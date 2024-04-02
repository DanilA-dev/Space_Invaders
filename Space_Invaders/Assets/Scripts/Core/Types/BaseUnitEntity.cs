using System;
using Systems.Behaviour;
using Core.Interfaces;
using Core.Model;
using UnityEngine;

namespace Entity
{
    public abstract class BaseUnitEntity : BaseEntity, IDamagable
    {
        [SerializeField] private ShootHandler _shootHandler;
        
        public event Action<BaseUnitEntity> OnDestroy;
        public BaseUnit Unit { get; private set; }


        public void Init(BaseUnit unit)
        {
            Unit = unit;
            _shootHandler.Init(Unit);
        }
        
        public void Damage(int damageValue)
        {
            Unit.CurrentHealth.Value -= damageValue;
            if (Unit.CurrentHealth.Value <= 0)
            {
                OnDestroyUnit();
                OnDestroy?.Invoke(this);
            }
        }
        protected virtual void OnDestroyUnit() =>  Destroy(gameObject);
    }
}