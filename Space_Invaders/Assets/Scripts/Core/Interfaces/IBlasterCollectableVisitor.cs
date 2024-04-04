using Systems.Behaviour;

namespace Core.Interfaces
{
    public interface IBlasterCollectableVisitor
    {
        public void Visit(ShootHandler visitable);
    }

    public interface IBlasterCollectbleVisitable
    {
        public void Accept(IBlasterCollectableVisitor visitor);
    }
}