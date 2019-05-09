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
            Debug.Log("Collision detected");
            onCollision?.Invoke(gameObject);
        }

        public void Clear()
        {
            StopAllCoroutines();
            onCollision = null;
        }
    }
}
