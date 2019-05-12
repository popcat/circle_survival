using UnityEngine;

namespace CircleSurvival
{
    /**
     * Corountine runner for simple game coroutines
     * */
    public class GameCoroutineRunner : MonoBehaviour, ICoroutineRunner, IClerable
    {
        public void Clear()
        {
            StopAllCoroutines();
        }
    }
}
