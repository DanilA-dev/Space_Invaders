using Core.Model;

namespace Systems
{
    public interface IShootCondition
    {
        public void Init(IUnitEntityRegisterService unitEntityRegisterService, BaseUnit ownerUnit);
        public bool CanShoot();
    }
}