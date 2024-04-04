using System;
using Core.Model;

namespace Systems
{
    public interface IUnitEntityRegisterService
    {
        public event Action<BaseUnit> OnUnitRegister;
        public event Action<BaseUnit> OnUnitDeregister; 
        
        public void Register(BaseUnit unit);
        public void Deregister(BaseUnit unit);

        public T GetUnit<T>() where T : BaseUnit;
    }
}

