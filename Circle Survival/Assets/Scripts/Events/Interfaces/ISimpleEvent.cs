using System;

namespace CircleSurvival
{
    public interface ISimpleEvent
    {
        void Subscribe(Action eventAction);
        void InvokeEvent();
    }
}
