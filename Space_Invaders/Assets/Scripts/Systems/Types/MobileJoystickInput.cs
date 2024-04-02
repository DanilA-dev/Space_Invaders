using System;
using UnityEngine;

namespace Systems
{
    public class MobileJoystickInput : MonoBehaviour, IInput
    {
        [SerializeField] private Joystick _joystick;
        
        public event Action<Vector2> GetInputDirection;

        private void Update()
        {
            UpdateDirection();
        }

        private void UpdateDirection()
        {
            if(_joystick == null)
                return;
            
            var dir = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            GetInputDirection?.Invoke(dir);
        }
    }
}