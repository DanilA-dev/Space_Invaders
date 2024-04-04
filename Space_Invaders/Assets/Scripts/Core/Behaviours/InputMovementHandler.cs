using Systems;
using Core.Model;
using UnityEngine;
using Zenject;

public class InputMovementHandler : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MovementRestrictions _movementRestrictions;

    private Vector2 _moveDirection;
    private IInput _input;
    
    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Awake()
    {
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
    
    public void Move(ref BaseUnit unit)
    {
        var pos = unit.Position;
        var clampedDir = _movementRestrictions.GetRestrictedDirection(pos);
        Vector3 movementDir = _moveDirection * (_speed * Time.deltaTime);
        unit.Position = movementDir + clampedDir;
    }
        
}
