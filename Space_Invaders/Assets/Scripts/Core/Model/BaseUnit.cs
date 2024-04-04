
using UniRx;
using UnityEngine;

namespace Core.Model
{
    public abstract class BaseUnit
    {
        public Vector3 Position { get; set; }
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }

        public IntReactiveProperty CurrentHealth = new IntReactiveProperty();
        
        public BaseUnit(int maxHealth, int damage, Vector3 position)
        {
            Position = position;
            MaxHealth = maxHealth;
            Damage = damage;

            CurrentHealth.Value = MaxHealth;
        }

    }
}