using Entity;
using Zenject;

namespace Systems.Factories
{
    public class BlasterCollectableFactory : CollectableFactory<BlasterCollectableEntity>
    {
        public BlasterCollectableFactory(DiContainer diContainer) : base(diContainer) {}
    }
}