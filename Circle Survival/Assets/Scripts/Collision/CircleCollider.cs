using UnityEngine;
using System;

namespace CircleSurvival
{
    public class CircleCollider : MonoBehaviour, ICollider, IClerable
    {
        private Action<GameObject> onCollision;

        public void Initialize(Action<GameObject> onCollision)
        {
            this.onCollision = onCollision;
        }

        public void TriggerCollision()
        {
            onCollision?.Invoke(gameObject);
            Debug.Log("Collision detected");
        }

        public void Clear()
        {
            onCollision = null;
        }
    }
}
