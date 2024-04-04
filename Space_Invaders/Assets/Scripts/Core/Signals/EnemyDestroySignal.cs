using Core.Model;

namespace Core.Signals
{
    public class EnemyDestroySignal
    {
        public EnemyUnit EnemyUnit { get; private set; }
        public EnemyDestroySignal(EnemyUnit enemyUnit)
        {
            EnemyUnit = enemyUnit;
        }

    }
}