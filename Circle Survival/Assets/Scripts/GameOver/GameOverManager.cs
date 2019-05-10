using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CircleSurvival
{
    public class GameOverManager
    {
        private Text gameOverText;
        private Text scoreInfoText;
        private Text scoreValueText;
        private Button endButton;
        private Image panelImage;

        private readonly float showDelay;

        public GameOverManager()
        {
            showDelay = 0;
        }

        public void ShowLayout()
        {
            //todo controllery
            gameOverText.gameObject.SetActive(true);
            scoreInfoText.gameObject.SetActive(true);
            scoreValueText.gameObject.SetActive(true);
            endButton.gameObject.SetActive(true);
            panelImage.gameObject.SetActive(true);
        }

        private IEnumerator ShowInDelay()
        {
            yield return new WaitForSeconds(showDelay);
            ShowLayout();
        }
    }
}
