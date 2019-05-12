using System.Collections;
using UnityEngine;

namespace CircleSurvival {
    public class ScoreManager
    {
        private readonly PlayerScore playerScore;
        private readonly ISaveManager saveManager;

        public int HighScore { get; private set; }

        public ScoreManager(
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