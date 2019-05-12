using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CircleSurvival
{
    /***
     * Layout controller activating and managing gameover UI components
     * */
    public class GameOverLayout
    {
        private const string lowScoreMessage = "SCORE";
        private const string highScoreMessage = "NEW HIGH SCORE";

        private readonly GameObject layoutRoot;
        private readonly ICoroutineRunner gameRunner;
        private readonly float showDelay;
        private readonly HighScoreManager scoreManager;

        private readonly Text scoreText;
        private Coroutine gameOverCoroutine;

        public GameOverLayout(GameObject layoutRoot, ICoroutineRunner gameRunner, float showDelay, HighScoreManager scoreManager)
        {
            this.layoutRoot = layoutRoot;
            this.gameRunner = gameRunner;
            this.showDelay = showDelay;
            this.scoreManager = scoreManager;
            scoreText = layoutRoot.transform.Find("ScoreLabel").GetComponent<Text>();
            HideLayout();
        }

        public void ShowLayout()
        {
            gameOverCoroutine = gameRunner.StartCoroutine(ShowInDelay());
        }

        public void HideLayout()
        {
            layoutRoot.SetActive(false);
        }

        private IEnumerator ShowInDelay()
        {
            yield return new WaitForSeconds(showDelay);
            scoreText.text = scoreManager.IsHighScore() ? highScoreMessage : lowScoreMessage;
            layoutRoot.SetActive(true);
            if(scoreManager.IsHighScore())
            {
                scoreText.text = highScoreMessage;
            }
            else
            {
                layoutRoot.transform.Find("StarParticles").gameObject.SetActive(false);
                scoreText.text = lowScoreMessage;
            }
        }
    }
}
