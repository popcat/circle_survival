using UnityEngine;
using System;

namespace CircleSurvival
{
    public interface ICircleController
    {
        void Initialize(float TimeToAction, Action<GameObject> timeEndAction);
        void Activate();
    }
}
