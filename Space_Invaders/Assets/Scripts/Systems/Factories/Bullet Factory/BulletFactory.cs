using Entity;
using Zenject;

namespace Systems.Factories
{
    public class BulletFactory : BaseEntityFactory<BulletEntity>
    {
        public BulletFactory(DiContainer diContainer) : base(diContainer) {}
    }
}