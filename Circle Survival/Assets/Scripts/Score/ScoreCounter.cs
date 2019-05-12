using System.Collections;
using UnityEngine;

namespace CircleSurvival
{
    /***
     * Adds score points in time
     * */
    public class ScoreCounter
    {
        private readonly PlayerScore playerScore;
        private readonly ICoroutineRunner gameRunner;

        private readonly float timeInterval;
        private readonly int scorePoints;

        private Coroutine scoreCoroutine;

        public ScoreCounter(
            PlayerScore playerScore, ICoroutineRunner gameRunner
            , float timeInterval = 1f, int scorePoints = 1)
        {
            this.playerScore = playerScore;
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
            while (true)
            {
                yield return new WaitForSeconds(timeInterval);
                playerScore.IncreaseBy(scorePoints);
            }
        }
    }
}