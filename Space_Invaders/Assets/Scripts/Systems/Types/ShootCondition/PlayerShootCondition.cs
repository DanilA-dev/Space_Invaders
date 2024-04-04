using Core.Model;

namespace Systems
{
    public class PlayerShootCondition : IShootCondition
    {
        public void Init(IUnitEntityRegisterService unitEntityRegisterService, BaseUnit ownerUnit) {}
            
        public bool CanShoot() => true;
          
    }
}