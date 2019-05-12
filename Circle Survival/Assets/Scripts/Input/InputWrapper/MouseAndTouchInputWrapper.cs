using UnityEngine;

namespace CircleSurvival
{
    /***
     * Input wrapper for testing, works both with mouse or touch input
     * */
    public class MouseAndTouchInputWrapper : ITouchInputWrapper
    {

        public TouchInput GetTouch(int index)
        {
            if (Input.GetMouseButtonDown(index))
            {
                return new TouchInput(true, Input.mousePosition, TouchPhase.Began);
            }

            if (Input.touchCount > index)
            {
                Touch t = Input.GetTouch(index);
                return new TouchInput(true, t.position, t.phase);
            }
            //by default isTouching will be set to false
            return new TouchInput();
        }
    }
}
