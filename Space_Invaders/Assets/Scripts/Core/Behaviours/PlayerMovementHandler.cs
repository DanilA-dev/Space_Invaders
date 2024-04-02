using Systems;
using Entity;
using UnityEngine;
using Zenject;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MovementRestrictions _movementRestrictions;

    private Transform _transform;
    private Vector2 _moveDirection;
    private IInput _input;
    
    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        _transform = transform;
        _movementRestrictions.Init();
        _input.GetInputDirection += UpdateMoveDirection;
    }

    private void OnDestroy()
    {
        _input.GetInputDirection -= UpdateMoveDirection;
    }

    private void LateUpdate()
    {
        Move();
    }

    private void UpdateMoveDirection(Vector2 dir)
    {
        _moveDirection = dir;
    }

    
    private void Move()
    {
        var dir = _transform.position;
        var clampedDir = _movementRestrictions.GetRestrictedDirection(dir);
        Vector3 movementDir = _moveDirection * (_speed * Time.deltaTime);
        _transform.position = movementDir + clampedDir;
    }
        
}
