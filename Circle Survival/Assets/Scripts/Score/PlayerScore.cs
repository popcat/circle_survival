using System;

namespace CircleSurvival
{
    public class PlayerScore
    {
        private event Action<int> onScoreChange;

        private int score;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                onScoreChange?.Invoke(score);
            } 
        }

        public PlayerScore(int score = 0)
        {
            Score = score;
        }

        public void IncreaseBy(int scorePoints)
        {
            Score += scorePoints;
        }

        public void Subscribe(Action<int> action)
        {
            action.Invoke(score);
            onScoreChange += action;
        }


        public void Unsubscribe(Action<int> action)
        {
            onScoreChange -= action;
        }
    }
}
