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
            SetInactive();
        }

        public void SetActive()
        {
            scoreText.gameObject.SetActive(true);
        }

        public void SetInactive()
        {
            scoreText.gameObject.SetActive(false);
        }

        public void UpdateText(int score)
        {
            scoreText.text = "" + score;
        }
    }
}
