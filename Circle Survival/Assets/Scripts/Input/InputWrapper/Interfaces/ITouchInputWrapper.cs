using System;
using UnityEngine;

namespace CircleSurvival
{
    public interface ITouchInputWrapper
    {
        TouchInput GetTouch(int index);
    }
}
