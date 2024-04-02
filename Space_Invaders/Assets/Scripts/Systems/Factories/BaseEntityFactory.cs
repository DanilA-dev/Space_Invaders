using UnityEngine;
using Zenject;

namespace Systems.Factories
{
    public class BaseEntityFactory<T> where T : Entity.BaseEntity
    {
        protected DiContainer _diContainer;

        public BaseEntityFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public T Create(GameObject prefab)
        {
            return _diContainer.InstantiatePrefab(prefab).GetComponent<T>();
        }
    }
}