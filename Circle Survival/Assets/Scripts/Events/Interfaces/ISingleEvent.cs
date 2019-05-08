using System;

namespace CircleSurvival
{
    public interface ISingleEvent
    {
        void Subscribe(Action eventAction);
        void Unsubscribe(Action eventAction);
        void InvokeEvent();
    }
}
