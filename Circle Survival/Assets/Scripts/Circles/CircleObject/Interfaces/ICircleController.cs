using UnityEngine;
using System;

namespace CircleSurvival
{
    public interface ICircleController
    {
        void Initialize();
        void Initialize(Color color, float timeOfGrowth, float timeOfShrink);
        void SubscribeFullGrowth(Action action);
        void SubscribeFullGrowth(Action<GameObject> action);
        void SubscribeFullShrink(Action action);
        void SubscribeFullShrink(Action<GameObject> action);
        void SetGrowing();
        void SetShrkinking();
    }
}
