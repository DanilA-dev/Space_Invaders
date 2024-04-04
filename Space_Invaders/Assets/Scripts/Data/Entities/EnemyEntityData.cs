using UnityEngine;

namespace Data
{
    public enum EnemyType
    {
        Type1 = 0,
        Type2 = 1,
        Type3 = 2
    }
    
    [CreateAssetMenu(menuName = "Data/Entity/Enemy Entity")]
    public class EnemyEntityData : UnitEntityData
    {
        [field:SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField] public int Score { get; private set; }
    }
}