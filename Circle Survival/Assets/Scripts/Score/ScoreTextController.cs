using UnityEngine.UI;

namespace CircleSurvival
{
    public class ScoreTextController
    {
        private Text scoreText;
        private readonly string textBegining;

        public ScoreTextController(Text scoreText, string textBegining)
        {
            this.scoreText = scoreText;
            this.textBegining = textBegining;
        }

        public void UpdateText(int score)
        {
            scoreText.text = textBegining + score;
        }
    }
}
