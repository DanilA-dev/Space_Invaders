using UnityEngine;

namespace Data
{
    public abstract class CollectableEntityData : EntityData
    {
        [field: SerializeField] public int DropChance { get; private set; }
    }
}
