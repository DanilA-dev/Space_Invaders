using Core.Model;

namespace Core.Interfaces
{
    public interface IDamagable
    {
        public BaseUnit UnitOwner { get; }
        public void Damage(int damageValue);
    }
}