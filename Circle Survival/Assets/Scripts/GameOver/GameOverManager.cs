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
        private readonly ScoreManager scoreManager;

        private const string lowScoreMessage = "SCORE";
        private const string highScoreMessage = "NEW HIGH SCORE";

        private Coroutine gameOverCoroutine;
        private Text scoreText;

        public GameOverManager(GameObject gameOverLayout, ICoroutineRunner gameRunner, float showDelay, ScoreManager scoreManager)
        {
            this.gameOverLayout = gameOverLayout;
            this.gameRunner = gameRunner;
            this.showDelay = showDelay;
            this.scoreManager = scoreManager;
            scoreText = gameOverLayout.transform.Find("ScoreLabel").GetComponent<Text>();
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
            scoreText.text = scoreManager.IsHighScore() ? highScoreMessage :lowScoreMessage;
            yield return new WaitForSeconds(showDelay);
            gameOverLayout.SetActive(true);
        }
    }
}
