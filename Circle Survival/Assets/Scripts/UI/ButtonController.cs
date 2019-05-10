using System;
using UnityEngine;
using UnityEngine.UI;

namespace CircleSurvival
{
    public class ButtonController: MonoBehaviour
    {
        private Button button;

        public void Initialize(Action onButtonClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onButtonClick.Invoke);
        }

        private void Start()
        {
            button = GetComponent<Button>();
        }

        public void Click()
        {
            button.onClick.Invoke();
        }
    }
}
