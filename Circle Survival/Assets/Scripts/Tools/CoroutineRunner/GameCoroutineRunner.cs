using UnityEngine;

namespace CircleSurvival
{
    public class GameCoroutineRunner : MonoBehaviour, ICoroutineRunner, IClerable
    {

        public void Clear()
        {
            StopAllCoroutines();
        }
    }
}
