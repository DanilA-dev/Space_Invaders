using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Entity/Bullet")]
    public class BulletEntityData : EntityData
    {
        [field:SerializeField] public float Speed { get; private set; }
    }
        
}