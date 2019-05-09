using System;
using UnityEngine;

namespace CircleSurvival
{
    public interface ICollider
    {
        void TriggerCollision();
        void Initialize(Action<GameObject> onCollision);
    }
}
