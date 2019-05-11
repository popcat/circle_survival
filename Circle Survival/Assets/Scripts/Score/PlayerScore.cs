namespace CircleSurvival
{
    public class PlayerScore
    {
        public int Score { get; private set; }
        public int HighScore { get; private set; }

        public PlayerScore(int score = 0, int highScore = 0)
        {
            Score = score;
            HighScore = highScore;
        }

        public void IncreaseBy(int scorePoints)
        {
            Score += scorePoints;
        }
    }
}
