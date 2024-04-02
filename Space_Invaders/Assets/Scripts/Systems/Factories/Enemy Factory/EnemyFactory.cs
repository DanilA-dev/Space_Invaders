using Entity;
using Zenject;

namespace Systems.Factories
{
    public class EnemyFactory : BaseEntityFactory<EnemyEntity>
    {
        public EnemyFactory(DiContainer diContainer) : base(diContainer){}
    }
}