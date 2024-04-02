using UnityEngine;
using Random = UnityEngine.Random;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private void Start()
        {
            _animator.speed = Random.Range(_minSpeed, _maxSpeed);
        }
    }

}
