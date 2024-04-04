using System;
using Core.Signals;
using Data;
using UniRx;

namespace Systems
{
    public class ScoreHandler : IScoreHandler, IDisposable
    {
        private Score _currentScore;
        private Score _bestScore;
        private GameState _gameState;
        
        private IDisposable _disposable;
        
        #region Properties

        public Score CurrentScore => _currentScore;
        public Score BestScore => _bestScore;

        #endregion
        
        public ScoreHandler(UserData userData, GameState gameState)
        {
            _gameState = gameState;
            _currentScore = userData.CurrentScore;
            _bestScore = userData.BestScore;

            _gameState.OnLevelRestarted += () => _currentScore.SetScore(0);
            
            _disposable = MessageBroker.Default.Receive<AddScoreSignal>()
                .Subscribe(_ => AddScore(_.Score));
        }
        
        
        public void AddScore(int value)
        {
            _currentScore.AddScore(value);
            
            if(_currentScore.Value.Value > _bestScore.Value.Value)
                _bestScore.SetScore(_currentScore.Value.Value);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}