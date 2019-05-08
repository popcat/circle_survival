using UnityEngine;
using System;

namespace CircleSurvival
{
    public class GreenCircleProvider: ICircleProvider
    {
        IObjectPool<GameObject> greeCirclePool;
        event Action OnExplode;

        private float minTapTime, maxTapTime;
        private float deltaTapTime;

        public GreenCircleProvider(
            IObjectPool<GameObject> objectPool, Action deathAction,
            float minTapTime, float maxTapTime, float deltaTapTime)
        {
            this.greeCirclePool = objectPool;
            this.OnExplode = deathAction;
            this.minTapTime = minTapTime;
            this.maxTapTime = maxTapTime;
            this.deltaTapTime = deltaTapTime;
        }

        public ICircleController GetCircle()
        {
            GameObject circle = greeCirclePool.TakeFromPool();
            ICircleController circleController = circle.GetComponent<ICircleController>();
            circleController.Initialize(
                UnityEngine.Random.Range(minTapTime, maxTapTime), GreenCircleFinishTime);
            UpdateTapTime();
            circle.GetComponent<CircleCollider>().Initialize(GreenCircleTapAction);
            return circleController;
        }

        public void PoolCircle(ICircleController circleController)
        {
            if (circleController is GreenCircleController)
            {
                GameObject obj = ((GreenCircleController)circleController).gameObject;
                foreach (IClerable clerable in obj.GetComponentsInChildren<IClerable>())
                {
                    clerable.Clear();
                }
                greeCirclePool.AddToPool(obj);
            }
        }

        public void GreenCircleFinishTime(ICircleController circleController)
        {
            OnExplode?.Invoke();
        }

        public void GreenCircleTapAction(GameObject obj)
        {
            PoolCircle(obj.GetComponent<ICircleController>());
        }

        private void UpdateTapTime()
        {
            minTapTime = Mathf.Max(0, minTapTime - deltaTapTime);
            maxTapTime = Mathf.Max(0, maxTapTime - deltaTapTime);
        }
    }
}


