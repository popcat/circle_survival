using System;
using UnityEngine;

namespace CircleSurvival
{
    public interface ICircleAnimationController
    {
        void Initialize(Color color, float timeOfAnimation = 1);
        void SubscribeFullGrowth(Action action);
        void SubscribeFullGrowth(Action<GameObject> action);
        void SubscribeFullShrink(Action action);
        void SubscribeFullShrink(Action<GameObject> action);
        void SetGrowing();
        void SetShrkinking();
    }
}
