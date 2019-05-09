using UnityEngine;

namespace CircleSurvival
{
    public interface ICircleProvider
    {
        GameObject GetCircle();
        void PoolCircle(GameObject obj);
    }
}
