using Systems;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] private string _label;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private ScoreType _scoreType;
        
        private Score _score;
        
        [Inject]
        private void Construct(IScoreHandler scoreHandler)
        {
            _score = _scoreType == ScoreType.Current ? scoreHandler.CurrentScore : scoreHandler.BestScore;
            _score.Value.Subscribe(UpdateScore).AddTo(gameObject);
        }

        private void UpdateScore(int score) => _scoreText?.SetText(_label + " " + score);

    }
}