
using UniRx;

namespace Core.Model
{
    public abstract class BaseUnit
    {
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }

        public IntReactiveProperty CurrentHealth = new IntReactiveProperty();
        
        public BaseUnit(int maxHealth, int damage)
        {
            MaxHealth = maxHealth;
            Damage = damage;

            CurrentHealth.Value = MaxHealth;
        }

    }
}