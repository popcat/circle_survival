using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CircleSurvival
{
    public class TouchInputWrapper : ITouchInputWrapper
    {
        public TouchInput GetTouch(int index)
        {
            if(Input.touchCount > index)
            {
                Debug.Log("Touch count: " + Input.touchCount);
                Touch t = Input.GetTouch(index);
                return new TouchInput(true, t.position, t.phase);
            }
            //by default isTouching will be set to false
            return new TouchInput();
        }
    }
}
