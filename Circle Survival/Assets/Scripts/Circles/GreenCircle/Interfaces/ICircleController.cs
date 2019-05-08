using UnityEngine;
using System;

namespace CircleSurvival
{
    public interface ICircleController
    {
        //mozna mniejszyc do nawet jednej metody(jesli bedzie dzialac)
        void Initialize(float TimeToAction, Action<ICircleController> timeEndAction);
        void Activate();
        void SetPosition(Vector2 position);
    }
}
