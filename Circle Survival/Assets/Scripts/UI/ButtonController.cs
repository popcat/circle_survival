using System;
using UnityEngine.UI;

namespace CircleSurvival
{
    public class ButtonController
    {
        private Button button;

        public ButtonController(Button button, Action onButtonClick)
        {
            this.button = button;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onButtonClick.Invoke);
        }

        public void Click()
        {
            button.onClick.Invoke();
        }
    }
}
