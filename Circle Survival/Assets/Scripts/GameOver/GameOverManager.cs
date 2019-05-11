using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CircleSurvival
{
    public class GameOverManager
    {
        private readonly GameObject gameOverLayout;
        private readonly ICoroutineRunner gameRunner;
        private readonly float showDelay;
        private readonly PlayerScore playerScore;

        private const string lowScore = "SCORE";
        private const string highScore = "NEW HIGH SCORE";

        private Coroutine gameOverCoroutine;
        private Text scoreText;
        private Text pointsText;

        public GameOverManager(GameObject gameOverLayout, ICoroutineRunner gameRunner, float showDelay, PlayerScore playerScore)
        {
            this.gameOverLayout = gameOverLayout;
            this.gameRunner = gameRunner;
            this.showDelay = showDelay;
            this.playerScore = playerScore;
            scoreText = gameOverLayout.transform.Find("ScoreLabel").GetComponent<Text>();
            pointsText = gameOverLayout.transform.Find("PointsLabel").GetComponent<Text>();
            HideLayout();
        }

        public void ShowLayout()
        {
            gameOverCoroutine = gameRunner.StartCoroutine(ShowInDelay());
        }

        public void HideLayout()
        {
            gameOverLayout.SetActive(false);
        }

        private IEnumerator ShowInDelay()
        {
            yield return new WaitForSeconds(showDelay);
            if(playerScore.Score > playerScore.HighScore)
            {
                scoreText.text = highScore;
            }
            else
            {
                scoreText.text = lowScore;
            }
            pointsText.text = "" + playerScore.Score;
            gameOverLayout.SetActive(true);
        }
    }
}
