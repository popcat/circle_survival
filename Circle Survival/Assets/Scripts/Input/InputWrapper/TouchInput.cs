using UnityEngine;

namespace CircleSurvival
{
    /***
    * Structure with touch input data
    * */
    public struct TouchInput
    {
        private bool isTouching;
        public bool IsTouching 
        {
            get => isTouching;
            private set => isTouching = value;
        }

        private float x;
        public float X 
        {
            get => x;
            private set => x = value; 
        }

        private float y;
        public float Y 
        { 
            get => y; 
            private set => y = value; 
        }

        private TouchPhase touchPhase;
        public TouchPhase TouchPhase 
        {
            get => touchPhase; 
            private set => touchPhase = value; 
        }

        public TouchInput(
            bool isTouching = default, Vector2 position = default, TouchPhase touchPhase = default)
        {
            this.isTouching = isTouching;
            x = position.x;
            y = position.y;
            this.touchPhase = touchPhase;
        }


        public Vector2 GetPosition()
        {
            return new Vector2(X, Y);
        }

    }
}
