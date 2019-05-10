using UnityEngine;
using System.Collections;

namespace CircleSurvival {

    public class ScreenShake
    {
        private Transform cameraTransform;
        private readonly float shakeDuration;
        private readonly float shakeMagnitude;
        private readonly float shakeInterval;
        private readonly ICoroutineRunner shakeRunner;

        private Vector3 initialPosition;
        private Coroutine shakeCoroutine;

        public ScreenShake(Transform cameraTransform, float shakeDuration
        , float shakeMagnitude, float shakeInterval, ICoroutineRunner shakeRunner)
        {
            this.cameraTransform = cameraTransform;
            this.shakeDuration = shakeDuration;
            this.shakeMagnitude = shakeMagnitude;
            this.shakeInterval = shakeInterval;
            this.shakeRunner = shakeRunner;

            initialPosition = cameraTransform.position;
        }

        public void StartShake()
        {
            shakeCoroutine = shakeRunner.StartCoroutine(Shake());
        }

        public void StopShake()
        {
            shakeRunner.StopCoroutine(shakeCoroutine);
        }

        private IEnumerator Shake()
        {
            float duration = shakeDuration;
            while(duration > 0)
            {
                cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                duration -= shakeInterval;
                yield return new WaitForSeconds(shakeInterval);
            }
            cameraTransform.localPosition = initialPosition;
        }
    }
}
