using UnityEngine;
using System;

namespace CircleSurvival
{
    public class BlackCircleProvider : ICircleProvider
    {
        private readonly IObjectPool<GameObject> circlePool;
        private Action OnExplode;

        private readonly float tapTime;
        private readonly Color color;

        public BlackCircleProvider(
            IObjectPool<GameObject> objectPool, Action deathAction,
            float tapTime, Color color)
        {
            this.circlePool = objectPool;
            this.OnExplode = deathAction;
            this.tapTime = tapTime;
            this.color = color;
        }

        public GameObject GetCircle()
        {
            GameObject circle = circlePool.TakeFromPool();
            GameObject mainCircle = circle.transform.GetChild(0).gameObject;
            GameObject fillCircle = circle.transform.GetChild(1).gameObject;

            //All components
            CircleAnimationController blackAnimation = mainCircle.GetComponent<CircleAnimationController>();
            CircleAnimationController fillAnimation = fillCircle.GetComponent<CircleAnimationController>();
            CircleController circleController = circle.GetComponent<CircleController>();
            CircleCollider circleCollider = circle.GetComponent<CircleCollider>();

            //All Subscriptions
            circleController.Initialize(tapTime, BlackCircleDisarm);

            blackAnimation.Initialize(color);
            blackAnimation.SubscribeFullGrowth(circleController.Activate);
            blackAnimation.SubscribeFullShrink(PoolCircle);
            blackAnimation.SetGrowing();

            fillAnimation.Initialize();

            circleCollider.Initialize(BlackCircleExplode);

            return circle;
        }

        public void PoolCircle(GameObject obj)
        {
            GameObject circle = obj.GetComponentInParent<CircleController>()?.gameObject;
            if (circle != null)
            {
                foreach (IClerable clerable in circle.GetComponentsInChildren<IClerable>())
                {
                    clerable.Clear();
                }
                circlePool.AddToPool(circle);
            }
        }

        private void BlackCircleExplode(GameObject obj)
        {
            OnExplode?.Invoke();
        }

        private void BlackCircleDisarm(GameObject obj)
        {
            obj.transform.GetChild(0).GetComponent<CircleAnimationController>().SetShrkinking();
        }
    }
}