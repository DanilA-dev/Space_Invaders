using Data;

namespace Core.Model
{
    public class EnemyUnit : BaseUnit
    {
        public EnemyType EnemyType { get; private set; }
        
        public EnemyUnit(int maxHealth, int damage, EnemyType enemyType) : base(maxHealth, damage)
        {
            EnemyType = enemyType;
        }
    }
}