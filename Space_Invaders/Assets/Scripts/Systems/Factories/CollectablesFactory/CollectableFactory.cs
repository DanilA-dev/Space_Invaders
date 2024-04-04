using Entity;
using Zenject;

namespace Systems.Factories
{
    public abstract class CollectableFactory<T> : BaseEntityFactory<T> where T : BaseCollectableEntity
    {
        public CollectableFactory(DiContainer diContainer) : base(diContainer) {}
            
    }
}