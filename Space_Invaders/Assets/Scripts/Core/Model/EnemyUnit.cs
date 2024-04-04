using Data;
using UnityEngine;

namespace Core.Model
{
    public class EnemyUnit : BaseUnit
    {
        public EnemyType EnemyType { get; private set; }
        public int ScoreForDeath { get; private set; }
        
        public EnemyUnit BellowEnemy { get; set; }
        public EnemyUnit(int maxHealth, int damage,Vector3 position,
            EnemyType enemyType, int scoreForDeath) : base(maxHealth, damage, position)
        {
            EnemyType = enemyType;
            ScoreForDeath = scoreForDeath;
        }
    }
}