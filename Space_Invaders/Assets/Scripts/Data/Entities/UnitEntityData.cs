using UnityEngine;

namespace Data
{
    public abstract class UnitEntityData : EntityData
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}