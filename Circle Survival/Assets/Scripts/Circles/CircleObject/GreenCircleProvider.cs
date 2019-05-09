using UnityEngine;
using System;

namespace CircleSurvival
{
    public class GreenCircleProvider: ICircleProvider
    {
        private readonly IObjectPool<GameObject> circlePool;
        private Action OnExplode;

        private float minTapTime, maxTapTime;
        private readonly float deltaTapTime;
        private readonly Color color;

        public GreenCircleProvider(
            IObjectPool<GameObject> objectPool, Action explodeAction,
            float minTapTime, float maxTapTime, float deltaTapTime, Color color)
        {
            this.circlePool = objectPool;
            this.OnExplode = explodeAction;
            this.minTapTime = minTapTime;
            this.maxTapTime = maxTapTime;
            this.deltaTapTime = deltaTapTime;
            this.color = color;
        }

        public GameObject GetCircle()
        {
            float tapTime = UnityEngine.Random.Range(minTapTime, maxTapTime);
            UpdateTapTime();

            GameObject circle = circlePool.TakeFromPool();
            GameObject mainCircle = circle.transform.GetChild(0).gameObject;
            GameObject fillCircle = circle.transform.GetChild(1).gameObject;

            //All components
            CircleAnimationController greenAnimation = mainCircle.GetComponent<CircleAnimationController>();
            CircleAnimationController redAnimation = fillCircle.GetComponent<CircleAnimationController>();
            CircleController circleController = circle.GetComponent<CircleController>();
            CircleCollider circleCollider = circle.GetComponent<CircleCollider>();

            //Initializations
            circleController.Initialize(tapTime, GreenCircleExplode);

            greenAnimation.Initialize(color);
            greenAnimation.SubscribeFullGrowth(redAnimation.SetGrowing);
            greenAnimation.SubscribeFullGrowth(circleController.Activate);
            greenAnimation.SubscribeFullShrink(PoolCircle);
            greenAnimation.SetGrowing();

            //todo color shit
            redAnimation.Initialize(Color.red, tapTime);

            circleCollider.Initialize(GreenCircleDisarm);

            return circle;
           }

        public void PoolCircle(GameObject obj)
        {
            GameObject circle = obj.GetComponentInParent<CircleController>()?.gameObject;
            if (circle != null)
            {
                foreach (IClerable clerable in obj.GetComponentsInChildren<IClerable>())
                {
                    clerable.Clear();
                }
                circlePool.AddToPool(circle);
            }
        }

        private void GreenCircleExplode(GameObject obj)
        {
            OnExplode?.Invoke();
        }

        private void GreenCircleDisarm(GameObject obj)
        {
            foreach(ICircleAnimationController iac in obj.GetComponentsInChildren<ICircleAnimationController>())
            {
                iac.SetShrkinking();
            }
        }

        private void UpdateTapTime()
        {
            minTapTime = Mathf.Max(0, minTapTime - deltaTapTime);
            maxTapTime = Mathf.Max(0, maxTapTime - deltaTapTime);
        }
    }
}


