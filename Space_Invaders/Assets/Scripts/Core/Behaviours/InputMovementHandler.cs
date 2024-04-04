using Systems;
using Core.Model;
using UnityEngine;
using Zenject;

public class InputMovementHandler : MonoBehaviour
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
   
    private void UpdateMoveDirection(Vector2 dir)
    {
        _moveDirection = dir;
    }
    
    public void Move(ref PlayerUnit unit)
    {
        var dir = _transform.position;
        var clampedDir = _movementRestrictions.GetRestrictedDirection(dir);
        Vector3 movementDir = _moveDirection * (_speed * Time.deltaTime);
        _transform.position = movementDir + clampedDir;
        unit.Position = _transform.position;
    }
        
}
