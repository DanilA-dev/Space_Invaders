using Systems.Behaviour;
using Core.Interfaces;
using Data;
using UnityEngine;
using UnityEngine.Pool;

namespace Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BlasterCollectableEntity : BaseCollectableEntity, IBlasterCollectableVisitor
    {
        private Rigidbody2D _body;
        private BlasterData _blasterData;
        private float _speed;
        private Vector2 _dir = Vector2.down;
        private ObjectPool<BlasterCollectableEntity> _pool;

        public void Init(float speed, BlasterData blasterData, ObjectPool<BlasterCollectableEntity> pool)
        {
            _speed = speed;
            _blasterData = blasterData;
            _pool = pool;
        }

        private void Awake() => _body = GetComponent<Rigidbody2D>();
        
        private void FixedUpdate() =>  _body.velocity = _dir * (Time.fixedDeltaTime * _speed);
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out BulletBlocker blocker))
                _pool.Release(this);
            
            if (other.gameObject.TryGetComponent(out IBlasterCollectbleVisitable visitable))
            {
                visitable.Accept(this);
                _pool.Release(this);
            }
        }

        public void Visit(ShootHandler visitable)
        {
            visitable.ChangeBlaster(_blasterData);
        }
    }
}