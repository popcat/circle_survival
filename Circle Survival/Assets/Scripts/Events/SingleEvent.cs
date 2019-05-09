using System;
using UnityEngine;

namespace CircleSurvival
{
    public class SingleEvent: ISimpleEvent
    {
        private event Action onEvent;

        public void Subscribe(Action eventAction)
        {
            onEvent += eventAction;
        }

        public void Unsubscribe(Action eventAction)
        {
            onEvent -= eventAction;
        }

        public void InvokeEvent()
        {
            onEvent += TestEvent;
            onEvent?.Invoke();
            onEvent = null;
        }

        private void TestEvent()
        {
            Debug.Log("Invoking single event...");
        }
    }
}
