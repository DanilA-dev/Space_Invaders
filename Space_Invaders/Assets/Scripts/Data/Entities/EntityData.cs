using Entity;
using UnityEngine;

namespace Data
{
    public abstract class EntityData : ScriptableObject
    {
        [SerializeField] protected BaseEntity _entityPrefab;

        public BaseEntity EntityPrefab => _entityPrefab;
    }
}