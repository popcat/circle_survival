using UnityEngine;
using System;
using System.Collections;

namespace CircleSurvival
{
    public class GreenCircleController : MonoBehaviour, ICircleController, IClerable
    {
        private float timeToAction;
        private event Action<ICircleController> OnTimeEnd;
        private Coroutine circleCoroutine;
        //private SpriteRenderer spriteRenderer;

        public void Start()
        {
            //spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(float timeToAction, Action<ICircleController> timeEndAction)
        {
            this.timeToAction = timeToAction;
            //spriteRenderer.sprite = sprite;
            OnTimeEnd = timeEndAction;
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
            OnTimeEnd?.Invoke(this);
        }

        public void Clear()
        {
            StopAllCoroutines();
            OnTimeEnd = null;
        }
    }
}
