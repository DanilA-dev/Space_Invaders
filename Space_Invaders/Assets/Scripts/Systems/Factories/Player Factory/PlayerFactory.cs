using Entity;
using Zenject;

namespace Systems.Factories
{
    public class PlayerFactory : BaseEntityFactory<PlayerEntity>
    {
        public PlayerFactory(DiContainer diContainer) : base(diContainer){}
    }
        
}