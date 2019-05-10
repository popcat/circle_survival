using System;
using UnityEngine;

namespace CircleSurvival
{
    public class SingleEvent: ISimpleEvent
    {
        private Action onEvent;

        public SingleEvent()
        {
            onEvent += TestEvent;
        }

        public void Subscribe(Action eventAction)
        {
            onEvent += eventAction;
        }

        public void InvokeEvent()
        {
            onEvent?.Invoke();
            onEvent = null;
        }

        private void TestEvent()
        {
            Debug.Log("Invoking single event...");
        }
    }
}
