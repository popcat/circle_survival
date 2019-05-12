using UnityEngine;
using System;
using System.Collections;

namespace CircleSurvival
{
    /***
     * Provides circle from object pool to caller,
     * Handles initialization and clearing of circles
     ***/
    public class BlackCircleProvider : ICircleProvider
    {
        private readonly IObjectPool<GameObject> circlePool;
        private readonly Action OnExplode;
        private readonly float tapTime;
        private readonly float baseAnimationTime;
        private readonly Color color;

       

        public BlackCircleProvider(
            IObjectPool<GameObject> objectPool, Action deathAction,
            float tapTime, float baseAnimationTime, Color color)
        {
            this.circlePool = objectPool;
            this.OnExplode = deathAction;
            this.tapTime = tapTime;
            this.baseAnimationTime = baseAnimationTime;
            this.color = color;
        }

        public GameObject GetCircle()
        {
            GameObject circle = circlePool.TakeFromPool();
            GameObject mainCircle = circle.transform.GetChild(0).gameObject;
            GameObject fillCircle = circle.transform.GetChild(1).gameObject;

            //All components
            ICircleController blackController = mainCircle.GetComponent<ICircleController>();
            ICircleController fillController = fillCircle.GetComponent<ICircleController>();
            ICollider circleCollider = circle.GetComponent<ICollider>();

            //All Subscriptions
            circle.SetActive(true);
            fillCircle.SetActive(false);

            blackController.Initialize(color, baseAnimationTime, baseAnimationTime);
            blackController.SubscribeFullGrowth(SetTimer);
            blackController.SubscribeFullShrink(PoolCircle);
            blackController.SetGrowing();

            circleCollider.Initialize(BlackCircleExplode);

            return circle;
        }

        public void PoolCircle(GameObject obj)
        {
            GameObject circle = obj.transform.parent.gameObject;
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
            obj.GetComponent<ICircleController>().SetShrkinking();
        }

        private void SetTimer(GameObject obj)
        {
            obj.GetComponent<ICoroutineRunner>().StartCoroutine(timerRunner(obj));
        }

        private IEnumerator timerRunner(GameObject obj)
        {
            yield return new WaitForSeconds(tapTime);
            BlackCircleDisarm(obj);
        }
    }
}