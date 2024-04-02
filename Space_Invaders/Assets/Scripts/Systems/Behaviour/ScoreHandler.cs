using Data;

namespace Systems
{
    public class ScoreHandler : IScoreHandler
    {
        private UserData _userData;
        private Score _currentScore;
        private Score _bestScore;

        #region Properties

        public Score CurrentScore => _currentScore;
        public Score BestScore => _bestScore;

        #endregion
      
        
        public ScoreHandler(UserData userData)
        {
            _userData = userData;
            _currentScore = userData.CurrentScore;
            _bestScore = userData.BestScore;
        }

        
        public void AddScore(int value)
        {
            _currentScore.ChangeScore(value);
        }
    }
}