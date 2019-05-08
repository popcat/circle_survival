using UnityEngine;
using System;
using System.Collections;

namespace CircleSurvival
{

    public class BlackCircleController : MonoBehaviour,ICircleController
    {
        private float timeToAction;
        private Coroutine circleCoroutine;

        public void Initialize(float timeToAction, Action<ICircleController> timeEndAction)
        {
            this.timeToAction = timeToAction;
            //spriteRenderer.sprite = sprite;
            //OnTimeEnd = timeEndAction;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            circleCoroutine = StartCoroutine(StartTimer());
        }

        public void SetPosition(Vector2 position)
        {
            gameObject.transform.position = position;
        }

        public IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(timeToAction);
            //OnTimeEnd?.Invoke(this);
        }

        public void Clear()
        {
            StopAllCoroutines();
            //OnTimeEnd = null;
        }

    }
}
