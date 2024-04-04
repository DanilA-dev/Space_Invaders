using Core.Interfaces;
using Core.Model;
using UnityEngine;
using UnityEngine.Pool;

namespace Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletEntity : BaseEntity
    {
        private Rigidbody2D _body2d;
        private BaseUnit _owner;
        private ObjectPool<BulletEntity> _pool;
        
        private float _speed;
        private Vector2 _direction;

        public void Init(BaseUnit owner, Vector2 direction, float speed, ObjectPool<BulletEntity> pool)
        {
            _owner = owner;
            _speed = speed;
            _pool = pool;
            _direction = direction;
        }
        
        private void Awake() =>  _body2d = GetComponent<Rigidbody2D>();
        private void FixedUpdate() =>  _body2d.velocity = -_direction * (Time.fixedDeltaTime * _speed);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out BulletBlocker blocker))
                _pool?.Release(this);
            
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                if(_owner == damagable.UnitOwner)
                    return;
                
                damagable?.Damage(_owner.Damage);
                _pool?.Release(this);
            }
        }
    }
}