using System;
using UnityEngine;

namespace Systems
{
    public interface IInput
    {
        public event Action<Vector2> GetInputDirection;
    }
}