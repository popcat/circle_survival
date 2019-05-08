using UnityEngine;

namespace CircleSurvival
{
    public class MouseInputWrapper : ITouchInputWrapper
    {
        public TouchInput GetTouch(int index)
        {
            if (Input.GetMouseButtonDown(index))
            {
                return new TouchInput(true, Input.mousePosition, TouchPhase.Began);
            }
            //by default isTouching will be set to false
            return new TouchInput();
        }
    }
}
