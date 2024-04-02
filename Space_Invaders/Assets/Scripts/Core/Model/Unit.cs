
namespace Core.Model
{
    public class Unit
    {
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        
        public Unit(int maxHealth, int damage)
        {
            MaxHealth = maxHealth;
            Damage = damage;
        }

    }
}