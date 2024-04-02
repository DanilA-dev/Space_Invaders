using UnityEngine;

[System.Serializable]
public class MovementRestrictions
{
    [SerializeField] private Vector2 _yConstraint;
    [SerializeField] private Vector2 _xConstraint;

    private Camera _camera;
    private Vector3 _bounds;

    public void Init()
    {
        _camera = Camera.main;
            
        if (_camera is not null)
            _bounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                _camera.transform.position.z));
    }
        
        
    public Vector3 GetRestrictedDirection(Vector2 direction)
    {
        var x = Mathf.Clamp(direction.x, (_bounds.x * -1 - _xConstraint.x), (_bounds.x + _xConstraint.y));
        var y =  Mathf.Clamp(direction.y, (_bounds.y * -1 - _yConstraint.x), (_bounds.y + _yConstraint.y));
        return new Vector3(x, y, 10);
    }
        
      
}
