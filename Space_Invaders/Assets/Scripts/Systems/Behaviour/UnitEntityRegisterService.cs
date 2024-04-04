using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using UnityEngine;
using UniRx;

namespace Systems
{
    public class UnitEntityRegisterService : IUnitEntityRegisterService, IDisposable
    {
        private List<BaseUnit> _units = new List<BaseUnit>();

        private IDisposable _disposable;
        private GameState _gameState;

        public event Action<BaseUnit> OnUnitRegister;
        public event Action<BaseUnit> OnUnitDeregister; 
        
        public UnitEntityRegisterService(GameState gameState)
        {
            _gameState = gameState;
            _disposable = _gameState.State
                .Where(state => state == GameStateType.Menu)
                .Subscribe(_ => OnMenuState());
        }
       
        public void Register(BaseUnit unit)
        {
            if (!_units.Contains(unit))
            {
                _units.Add(unit);
                OnUnitRegister?.Invoke(unit);
            }
        }

        public void Deregister(BaseUnit unit)
        {
            if (_units.Contains(unit))
            {
                _units.Remove(unit);
                OnUnitDeregister?.Invoke(unit);
            }
        }

        public T GetUnit<T>() where T : BaseUnit
        {
            T unit = (T)_units.FirstOrDefault(u => u is T);
            if (unit == null)
            {
                Debug.LogError($"Unit type {unit.GetType()} don't registered");
                return null;
            }

            return unit;
        }
        
        public List<T> GetUnits<T>() where T : BaseUnit
        {
            List<T> units = new List<T>();

            foreach (var unit in _units)
            {
                if(unit is T)
                    units.Add((T)unit);
            }

            return units;
        }
        
        private void OnMenuState()
        {
            _units.Clear();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}