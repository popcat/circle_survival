namespace CircleSurvival
{
    public class PlayerScore
    {
        public int Score { get; private set; }

        public PlayerScore(int score = 0)
        {
            Score = score;
        }

        public void IncreaseBy(int scorePoints)
        {
            Score += scorePoints;
        }
    }
}
