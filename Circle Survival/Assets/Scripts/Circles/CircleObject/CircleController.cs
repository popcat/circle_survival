﻿using UnityEngine;
using System.Collections;
using System;

namespace CircleSurvival
{
    /***
    * Controlls growing and shrinking animation
    * Handles delegates when processes are done
    ***/
    public class CircleController : MonoBehaviour, ICircleController, IClerable, ICoroutineRunner
    {

        private float timeOfGrowth;
        private float timeOfShrink;

        private Action onFullGrowth;
        private Action<GameObject> onFullGrowthCallback;

        private Action onFullShrink;
        private Action<GameObject> onFullShrinkCallback;

        private Coroutine animationCoroutine;

        private SpriteRenderer spriteRenderer;
        private SpriteRenderer SpriteRenderer
        {
            get
            {
                if (spriteRenderer == null)
                {
                    spriteRenderer = GetComponent<SpriteRenderer>();
                }
                return spriteRenderer;
            }
            set => spriteRenderer = value;
        }

        private void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Color color, float timeOfGrowth, float timeOfShrink)
        {
            this.timeOfGrowth = timeOfGrowth;
            this.timeOfShrink = timeOfShrink;
            transform.localScale = Vector2.zero;
            SpriteRenderer.color = color;
        }

        public void SubscribeFullGrowth(Action action)
        {
            onFullGrowth += action;
        }

        public void SubscribeFullGrowth(Action<GameObject> action)
        {
            onFullGrowthCallback += action;
        }

        public void SubscribeFullShrink(Action action)
        {
            onFullShrink += action;
        }

        public void SubscribeFullShrink(Action<GameObject> action)
        {
            onFullShrinkCallback += action;
        }

        public void SetGrowing()
        {
            gameObject.SetActive(true);
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            animationCoroutine = StartCoroutine(SetTimer());
        }

        public void SetShrkinking()
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            animationCoroutine = StartCoroutine(SetTimerBack());
        }

        public void Clear()
        {
            StopAllCoroutines();
            onFullGrowth = null;
            onFullGrowthCallback = null;
            onFullShrink = null;
            onFullShrinkCallback = null;
            gameObject.SetActive(false);
        }

        private IEnumerator SetTimer()
        {
            while (transform.localScale.x < 1)
            {
                yield return new WaitForEndOfFrame();
                float interval = Time.deltaTime / timeOfGrowth;
                transform.localScale += new Vector3(interval, interval, 0);
            }
            transform.localScale = Vector2.one;
            onFullGrowth?.Invoke();
            onFullGrowthCallback?.Invoke(this.gameObject);
        }

        private IEnumerator SetTimerBack()
        {
            while (transform.localScale.x > 0)
            {
                yield return new WaitForEndOfFrame();
                float interval = Time.deltaTime / timeOfShrink;
                transform.localScale -= new Vector3(interval, interval, 0);
            }
            transform.localScale = Vector2.zero;
            onFullShrink?.Invoke();
            onFullShrinkCallback?.Invoke(this.gameObject);
        }
    }
}
