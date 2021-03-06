﻿using UnityEngine;
using System;

namespace CircleSurvival
{
    public class GreenCircleProvider: ICircleProvider
    {
        private readonly IObjectPool<GameObject> circlePool;
        private readonly Action OnExplode;
        private float minTapTime, maxTapTime;
        private float deltaTapTime;
        private readonly float baseAnimationTime;
        private readonly Color greenColor;
        private readonly Color redColor;

        public GreenCircleProvider(
            IObjectPool<GameObject> objectPool, Action explodeAction,
            float minTapTime, float maxTapTime, float deltaTapTime, float baseAnimationTime, Color greenColor, Color redColor)
        {
            this.circlePool = objectPool;
            this.OnExplode = explodeAction;
            this.minTapTime = minTapTime;
            this.maxTapTime = maxTapTime;
            this.deltaTapTime = deltaTapTime;
            this.baseAnimationTime = baseAnimationTime;
            this.greenColor = greenColor;
            this.redColor = redColor;
        }

        public GameObject GetCircle()
        {
            float tapTime = UnityEngine.Random.Range(minTapTime, maxTapTime);
            UpdateTapTime();

            GameObject circle = circlePool.TakeFromPool();
            GameObject mainCircle = circle.transform.GetChild(0).gameObject;
            GameObject fillCircle = circle.transform.GetChild(1).gameObject;

            //All components
            ICircleController greenController= mainCircle.GetComponent<ICircleController>();
            ICircleController redController = fillCircle.GetComponent<ICircleController>();
            ICollider circleCollider = circle.GetComponent<ICollider>();

            circle.SetActive(true);

            greenController.Initialize(greenColor, baseAnimationTime, baseAnimationTime);
            greenController.SubscribeFullGrowth(redController.SetGrowing);
            greenController.SubscribeFullShrink(PoolCircle);
            greenController.SetGrowing();

            redController.Initialize(redColor, tapTime, baseAnimationTime);
            redController.SubscribeFullGrowth(GreenCircleExplode);

            circleCollider.Initialize(GreenCircleDisarm);

            return circle;
           }

        public void PoolCircle(GameObject obj)
        {
            GameObject circle = obj.transform.parent.gameObject;
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
            foreach(ICircleController iac in obj.GetComponentsInChildren<ICircleController>())
            {
                iac.SetShrkinking();
            }
        }

        private void UpdateTapTime()
        {
            minTapTime = Mathf.Max(0.5f, minTapTime - deltaTapTime);
            maxTapTime = Mathf.Max(0.5f, maxTapTime - deltaTapTime);
        }
    }
}


