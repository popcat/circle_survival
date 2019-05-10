using System.Collections;
using UnityEngine;

namespace CircleSurvival {
    public class ScoreManager
    {
        private readonly PlayerScore playerScore;
        private readonly ScoreTextController textController;
        private readonly ICoroutineRunner gameRunner;

        private readonly float timeInterval;
        private readonly int scorePoints;

        private Coroutine scoreCoroutine;

        public ScoreManager(
            PlayerScore playerScore, ScoreTextController textController
            , ICoroutineRunner gameRunner, float timeInterval = 1f
            , int scorePoints = 1)
        {
            this.playerScore = playerScore;
            this.textController = textController;
            this.gameRunner = gameRunner;

            this.timeInterval = timeInterval;
            this.scorePoints = scorePoints;
        }


        public void StartScoreCounter()
        {
            scoreCoroutine = gameRunner.StartCoroutine(RunScoreCoroutine());
        }

        public void StopScoreCounter()
        {
            if (scoreCoroutine != null)
                gameRunner.StopCoroutine(scoreCoroutine);
        }

        private IEnumerator RunScoreCoroutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(timeInterval);
                playerScore.IncreaseBy(scorePoints);
                textController.UpdateText(playerScore.Score);
            }
        }
    }
}