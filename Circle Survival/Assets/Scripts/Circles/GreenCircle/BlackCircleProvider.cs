using UnityEngine;
using System;

namespace CircleSurvival
{
    public class BlackCircleProvider : ICircleProvider
    {
        IObjectPool<GameObject> blackCirclePool;
        event Action OnExplode;

        private float tapTime;

        public BlackCircleProvider(
            IObjectPool<GameObject> objectPool, Action deathAction, float tapTime)
        {
            this.blackCirclePool = objectPool;
            this.OnExplode = deathAction;
            this.tapTime = tapTime;
        }

        public ICircleController GetCircle()
        {
            GameObject circle = blackCirclePool.TakeFromPool();
            ICircleController circleController = circle.GetComponent<ICircleController>();
            circleController.Initialize(tapTime, BlackCircleFinishTime);
            circle.GetComponent<CircleCollider>().Initialize(BlackCircleTapAction);
            return circleController;
        }

        public void PoolCircle(ICircleController circleController)
        {
            if (circleController is BlackCircleController)
            {
                GameObject obj = ((BlackCircleController)circleController).gameObject;
                foreach (IClerable clerable in obj.GetComponentsInChildren<IClerable>())
                {
                    clerable.Clear();
                }
                blackCirclePool.AddToPool(obj);
            }
        }

        public void BlackCircleFinishTime(ICircleController circleController)
        {
            PoolCircle(circleController);
        }

        public void BlackCircleTapAction(GameObject obj)
        {
            OnExplode?.Invoke();
        }
    }
}