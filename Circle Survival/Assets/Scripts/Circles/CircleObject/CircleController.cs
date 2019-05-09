using UnityEngine;
using System;
using System.Collections;

namespace CircleSurvival
{
    public class CircleController : MonoBehaviour, ICircleController, IClerable
    {
        private float timeToAction;
        private Action<GameObject> OnTimeEnd;
        private Coroutine circleCoroutine;

        public void Initialize(float timeToAction, Action<GameObject> timeEndAction)
        {
            gameObject.SetActive(true);
            this.timeToAction = timeToAction;
            OnTimeEnd = timeEndAction;
        }

        public void Activate()
        {
            circleCoroutine = StartCoroutine(StartTimer());
        }

        public IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(timeToAction);
            OnTimeEnd?.Invoke(this.gameObject);
        }

        public void Clear()
        {
            StopAllCoroutines();
            OnTimeEnd = null;
        }
    }
}
