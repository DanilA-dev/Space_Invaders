using Systems.Interfaces;
using Data;
using UnityEngine;

namespace Systems
{
    public class PrefsPersistentDataService : ISavePersistentDataService
    {
        private readonly string _bestScoreKey = "_bestScore";
        
        private UserData _userData;

        public PrefsPersistentDataService(UserData userData)
        {
            _userData = userData;
        }
        
        public void Save()
        {
            var bestScore = _userData.BestScore;
            PlayerPrefs.SetInt(_bestScoreKey, bestScore.Value.Value);
        }

        public void Load()
        {
            var bestScore = _userData.BestScore;
            bestScore.SetScore(PlayerPrefs.GetInt(_bestScoreKey));
        }
    }
}