using Core.Interfaces;
using Core.Model;
using UnityEngine;
using UnityEngine.Pool;

namespace Entity
{
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
        private void FixedUpdate() =>  _body2d.MovePosition(_direction * (_speed * Time.fixedDeltaTime));

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable?.Damage(_owner.Damage);
                _pool?.Release(this);
            }
        }
    }
}