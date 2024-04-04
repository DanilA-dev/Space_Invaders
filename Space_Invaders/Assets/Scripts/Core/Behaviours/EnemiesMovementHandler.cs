using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Behaviours
{
    public class EnemiesMovementHandler : MonoBehaviour
    {
        [SerializeField] private Transform _movingTransform;
        [SerializeField] private float _speedIncreaseByTime;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _moveDelay;
        [SerializeField] private float _yEndValue;

        private Vector3 _startPos;
        private GameState _gameState;
        private float SpeedModfier => _speedIncreaseByTime * Time.deltaTime;
        
        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
            _gameState.State
                .Where(state => state == GameStateType.Gameplay)
                .Subscribe(_ => StartMoving()).AddTo(gameObject);

            _gameState.State
                .Where(state => state == GameStateType.Menu)
                .Subscribe(_ => _speedIncreaseByTime = 0).AddTo(gameObject);
            
            _gameState.OnLevelRestarted += () => _movingTransform.position = _startPos;
        }

        private void Awake()
        {
            _startPos = _movingTransform.position;
        }

        private void StartMoving()
        {
            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            while (true)
            {
                _speedIncreaseByTime += Time.deltaTime;
                CheckEndValue();
                if(_gameState.State.Value != GameStateType.Gameplay)
                    yield break;
                
                yield return new WaitForSeconds(_moveDelay);
                MoveBlock();
            }
        }
        
        private void CheckEndValue()
        {
            if (_movingTransform.position.y <= _yEndValue)
                _gameState.State.Value = GameStateType.LoseGame;
        }

        private void MoveBlock()
        {
            _movingTransform.Translate(Vector3.down * (Time.deltaTime * _moveSpeed + SpeedModfier));
        }
    }
}