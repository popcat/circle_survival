using UnityEngine;
using System.Collections;
using System;

namespace CircleSurvival {
    public class CircleAnimationController : MonoBehaviour, ICircleAnimationController, IClerable
    {
        private Action onFullGrowth;
        private Action<GameObject> onFullGrowthCallback;

        private Action onFullShrink;
        private Action<GameObject>  onFullShrinkCallback;

        private Coroutine animationCoroutine;

        private float timeOfAnimation;

        private Animator animator;
        private Animator Animator
        {
            get {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }
                return animator;
            }
            set => animator = value;
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
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Clear();
        }

        public void Initialize(Color color, float timeOfAnimation = 1)
        {
            this.timeOfAnimation = timeOfAnimation;
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
            Animator.SetBool("isGrowing", true);
            Animator.SetBool("isShrinking", false);
            animationCoroutine = StartCoroutine(SetTimer(onFullGrowth, onFullGrowthCallback));
        }

        public void SetShrkinking()
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            Animator.SetBool("isShrinking", true);
            Animator.SetBool("isGrowing", false);
            animationCoroutine = StartCoroutine(SetTimer(onFullShrink, onFullShrinkCallback));
        }

        public void Clear()
        {
            StopAllCoroutines();
            Animator.SetBool("isGrowing", false);
            Animator.SetBool("isShrinking", false);
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
