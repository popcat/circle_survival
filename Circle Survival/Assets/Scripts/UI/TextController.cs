using UnityEngine.UI;

namespace CircleSurvival
{
    public class TextController
    {
        private Text textComponent;

        public TextController(Text textComponent)
        {
            this.textComponent = textComponent;
        }

        public void SetActive()
        {
            textComponent.gameObject.SetActive(true);
        }

        public void SetInactive()
        {
            textComponent.gameObject.SetActive(false);
        }

        public void UpdateText(string message)
        {
            textComponent.text = message;
        }

        public void UpdateText(int message)
        {
            textComponent.text = message.ToString();
        }
    }
}
