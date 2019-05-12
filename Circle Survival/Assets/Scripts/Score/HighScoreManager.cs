namespace CircleSurvival
{
    /***
     * Prvides access to saved highscore data
     * */
    public class HighScoreManager
    {
        private readonly PlayerScore playerScore;
        private readonly ISaveManager saveManager;

        public int HighScore { get; private set; }

        public HighScoreManager(
            PlayerScore playerScore, ISaveManager saveManager)
        {
            this.playerScore = playerScore;
            this.saveManager = saveManager;
            LoadHighScore();
        }

        public void SaveHighScore()
        {
            if (playerScore.Score > HighScore)
            {
                SaveData newSave = new SaveData();
                newSave.HighScore = playerScore.Score;
                saveManager.Save(newSave);
            }
        }

        public void LoadHighScore()
        {
            HighScore = saveManager.Load().HighScore;
        }

        public bool IsHighScore()
        {
            return playerScore.Score > HighScore;
        }
    }
}