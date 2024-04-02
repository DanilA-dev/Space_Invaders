
namespace Systems
{
    public interface IScoreHandler
    {
        public Score CurrentScore { get; }
        public Score BestScore { get; }

        public void AddScore(int value);
    }
}