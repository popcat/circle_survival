using System;
namespace CircleSurvival
{
    public class SingleEvent: ISingleEvent
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
