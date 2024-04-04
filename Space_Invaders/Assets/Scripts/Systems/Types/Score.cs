using UniRx;
using UnityEngine;

namespace Systems
{
    public enum ScoreType
    {
        Current = 0,
        Best = 1
    }
    
    [System.Serializable]
    public class Score
    {
        [field :SerializeField] public ScoreType Type { get; private set; }
        [SerializeField] private ReactiveProperty<int> _value = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> Value => _value;

        public void AddScore(int value) => _value.Value += value;
        public void SetScore(int value) => _value.Value = value;

    }
}