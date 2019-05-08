using UnityEngine;

namespace CircleSurvival
{
    public interface ICircleProvider
    {
        ICircleController GetCircle();
        void PoolCircle(ICircleController obj);
    }
}
