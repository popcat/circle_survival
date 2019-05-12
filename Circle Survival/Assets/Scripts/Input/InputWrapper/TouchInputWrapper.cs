using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CircleSurvival
{
    /***
    * Input wrapper for mouse input only
    * */
    public class TouchInputWrapper : ITouchInputWrapper
    {
        public TouchInput GetTouch(int index)
        {
            if(Input.touchCount > index)
            {
                Touch t = Input.GetTouch(index);
                return new TouchInput(true, t.position, t.phase);
            }
            return new TouchInput();
        }
    }
}
