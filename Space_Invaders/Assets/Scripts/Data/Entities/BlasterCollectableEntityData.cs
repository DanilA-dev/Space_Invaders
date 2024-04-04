using UnityEngine;

namespace Data
{
    
    [CreateAssetMenu(menuName = "Data/Entity/Collectable/Blaster")]
    public class BlasterCollectableEntityData : CollectableEntityData
    {
        [field: SerializeField] public BlasterData BlasterData { get; private set; }
        [field: SerializeField] public float FallSpeed { get; private set; }
    }
}