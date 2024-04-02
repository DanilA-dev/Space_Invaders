using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Entity/Bullet", order = 0)]
    public class BulletEntityData : EntityData
    {
        [field:SerializeField] public float Speed { get; private set; }
    }
        
}