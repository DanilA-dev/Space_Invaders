using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

namespace UI
{
    public class MenuSwitcher : MonoBehaviour
    {
        [SerializeField] private List<BaseMenu> _menus = new List<BaseMenu>();
        
        private GameState _gameState;

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
            _gameState.State.Subscribe(UpdateMenu).AddTo(gameObject);

        }

        private void UpdateMenu(GameStateType stateType)
        {
            switch (stateType)
            {
                case GameStateType.None:
                    break;
                case GameStateType.Menu:
                    ToggleMenu(MenuType.MainMenu);
                    break;
                case GameStateType.Gameplay:
                    ToggleMenu(MenuType.CoreMenu);
                    break;
                case GameStateType.Pause:
                    ShowMenuOnTop(MenuType.PauseMenu);
                    break;
                case GameStateType.LoseGame:
                    ToggleMenu(MenuType.LoseMenu);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stateType), stateType, null);
            }
        }

        private void ToggleMenu(MenuType type)
        {
            if(_menus.Count <= 0)
                return;

            foreach (var menu in _menus)
                menu.gameObject.SetActive(menu.MenuType == type);
        }

        private void ShowMenuOnTop(MenuType type)
        {
            if(_menus.Count <= 0)
                return;

            var menu = _menus.FirstOrDefault(m => m.MenuType == type);
            menu?.gameObject.SetActive(true);
        }
        
    }

}
