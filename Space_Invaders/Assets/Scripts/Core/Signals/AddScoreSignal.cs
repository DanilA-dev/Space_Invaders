namespace Core.Signals
{
    public class AddScoreSignal
    {
        public int Score { get; private set; }
        
        public AddScoreSignal(int score)
        {
            Score = score;
        }
    }
}