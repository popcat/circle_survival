using UnityEngine;
using System.Collections;
using System;

namespace CircleSurvival {
    public class CircleAnimationController : MonoBehaviour
    {
        private event Action onFullGrowth;
        private event Action onFullShrink;

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

        private void Start()
        {
            Animator = GetComponent<Animator>();
        }

        public void Initialize(float timeOfAnimation)
        {
            this.timeOfAnimation = timeOfAnimation;
            Animator.speed = timeOfAnimation;
        }

        public void SubscribeFullGrowth(Action action)
        {
            onFullGrowth += action;
        }

        public void SubscribeFullShrink(Action action)
        {
            onFullShrink += action;
        }

        public void SetGrowing()
        {
            animator.SetBool("isGrowing", true);
            animator.SetBool("isShrinking", false);
            StartCoroutine(SetTimer(onFullGrowth));
        }

        public void SetShrkinking()
        {
            animator.SetBool("isShrinking", true);
            animator.SetBool("isGrowing", false);
            StartCoroutine(SetTimer(onFullShrink));
        }

        private IEnumerator SetTimer(Action action)
        {
            yield return new WaitForSeconds(timeOfAnimation);
            action?.Invoke();
        } 
    }
}
