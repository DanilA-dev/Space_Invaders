using Systems;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/User Data")]
    public class UserData : ScriptableObject
    {
        [SerializeField] private Score _currentScore;
        [SerializeField] private Score _bestScore;

        public Score CurrentScore => _currentScore;
        public Score BestScore => _bestScore;
    }
}