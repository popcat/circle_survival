using UnityEngine;
using System.Collections;

namespace CircleSurvival
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator function);
        void StopCoroutine(Coroutine coroutine);
        void StopAllCoroutines();
    }
}
