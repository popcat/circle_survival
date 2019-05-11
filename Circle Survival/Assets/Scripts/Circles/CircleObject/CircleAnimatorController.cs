﻿using UnityEngine;
using System.Collections;
using System;

namespace CircleSurvival {
    public class CircleAnimatorController : MonoBehaviour, ICircleController, IClerable, ICoroutineRunner
    {
        private const string isGrowing = "isGrowing";
        private const string isShrinking = "isShrinking";
        private const string growProgress = "growProgress";
        private const string shrinkProgress = "shrinkProgress";

        private Action onFullGrowth;
        private Action<GameObject> onFullGrowthCallback;

        private Action onFullShrink;
        private Action<GameObject>  onFullShrinkCallback;

        private Coroutine animationCoroutine;

        private float timeOfAnimation;
        private const float baseTime = 5;

        private Animator circleAnimator;
        private Animator CircleAnimator
        {
            get {
                if (circleAnimator == null)
                {
                    circleAnimator = GetComponent<Animator>();
                }
                return circleAnimator;
            }
            set => circleAnimator = value;
        }

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
            CircleAnimator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(Color color, float timeOfAnimation = baseTime)
        {
            gameObject.SetActive(false);
            this.timeOfAnimation = timeOfAnimation;
            CircleAnimator.speed = 1/timeOfAnimation;
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
            CircleAnimator.SetBool(isGrowing, true);
            CircleAnimator.SetBool(isShrinking, false);
            animationCoroutine = StartCoroutine(SetTimer(onFullGrowth, onFullGrowthCallback));
        }

        public void SetShrkinking()
        {
            AnimatorStateInfo stateInfo = CircleAnimator.GetCurrentAnimatorStateInfo(0);
            float progress = stateInfo.normalizedTime;//CircleAnimator.GetFloat(growProgress);
            progress = progress % (int)progress;

            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            CircleAnimator.SetBool(isShrinking, true);
            CircleAnimator.SetBool(isGrowing, false);
            CircleAnimator.SetFloat(shrinkProgress, 1 - progress);
            CircleAnimator.speed = baseTime;

            animationCoroutine = StartCoroutine(SetTimer(onFullShrink, onFullShrinkCallback));
        }

        public void Clear()
        {
            StopAllCoroutines();
            onFullGrowth = null;
            onFullGrowthCallback = null;
            onFullShrink = null;
            onFullShrinkCallback = null;
            CircleAnimator.SetBool("isGrowing", false);
            CircleAnimator.SetBool("isShrinking", false);
            gameObject.SetActive(false);
        }

        private IEnumerator SetTimer(Action action1, Action<GameObject> action2)
        {
            yield return new WaitForSeconds(timeOfAnimation);
            action1?.Invoke();
            action2?.Invoke(this.gameObject);
        }
    }
}