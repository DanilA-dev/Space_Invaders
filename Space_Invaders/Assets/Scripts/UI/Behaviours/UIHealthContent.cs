using System;
using System.Collections.Generic;
using System.Linq;
using Systems;
using Core.Model;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI
{
    public class UIHealthContent : MonoBehaviour
    {
        [SerializeField] private Transform _healthContentParent;
        [SerializeField] private UIHealthItem _healthItemPrefab;

        private List<UIHealthItem> _healthItems = new List<UIHealthItem>();
        
        private GameState _gameState;
        private IUnitEntityRegisterService _unitRegisterService;

        [Inject]
        private void Construct(GameState gameState, IUnitEntityRegisterService unitEntityRegisterService)
        {
            _gameState = gameState;
            _unitRegisterService = unitEntityRegisterService;
            _gameState.OnLevelInitialized += OnLevelInit;
            _gameState.State
                .Where(state => state == GameStateType.Menu)
                .Subscribe(_ => ClearHealthItems()).AddTo(gameObject);
        }

        private void OnLevelInit()
        {
            var playerUnit = GetPlayerUnit();
            CreateHealthItems(playerUnit);
        }

        private void CreateHealthItems(PlayerUnit playerUnit)
        {
            for (int i = 0; i < playerUnit.MaxHealth; i++)
            {
                var newHealthItem = Instantiate(_healthItemPrefab, _healthContentParent);
                _healthItems.Add(newHealthItem);
            }
        }

        private PlayerUnit GetPlayerUnit()
        {
            var playerUnit = _unitRegisterService.GetUnit<PlayerUnit>();
            playerUnit.CurrentHealth.Subscribe(_ => DamageHealthItems()).AddTo(gameObject);
            return playerUnit;
        }

        private void DamageHealthItems()
        {
            if(_healthItems.Count <= 0)
                return;

            var lastHealth = _healthItems.FirstOrDefault(h => !h.IsHidden);
            lastHealth?.Hide();
        }
        
        private void ClearHealthItems()
        {
            if(_healthItems.Count <= 0)
                return;

            foreach (var healthItem in _healthItems)
                Destroy(healthItem);
            
            _healthItems.Clear();
        }
    }
}