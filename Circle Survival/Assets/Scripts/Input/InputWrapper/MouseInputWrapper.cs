using UnityEngine;

namespace CircleSurvival
{
    /***
    * Input wrapper for mouse input only
    * */
    public class MouseInputWrapper : ITouchInputWrapper
    {
        public TouchInput GetTouch(int index)
        {
            if (Input.GetMouseButtonDown(index))
            {
                return new TouchInput(true, Input.mousePosition, TouchPhase.Began);
            }
            return new TouchInput();
        }
    }
}
