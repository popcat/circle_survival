using System;

namespace CircleSurvival
{
    /***
     * Simple event invoking once and then clearing itself
     ***/
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
            onEvent?.Invoke();
            onEvent = null;
        }
    }
}
