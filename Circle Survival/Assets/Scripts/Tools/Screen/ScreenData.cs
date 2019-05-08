using System;
using UnityEngine;
namespace CircleSurvival
{
    public class ScreenData
    {
        public float ScreenWidth { get; private set; }
        public float ScreenHeight { get; private set; }
        public float Aspect { get; private set; }

        public int ScreenWidthPixels { get; private set; }
        public int ScreenHeightPixels { get; private set; }

        private Camera mCamera;
        public Vector2 ScreenMiddle { get; private set; }
        public float XLeftBoundry { get; private set; }
        public float XRightBoundry { get; private set; }
        public float YBottomBoundry { get; private set; }
        public float YTopBoundry { get; private set; }

        public ScreenData()
        {

        }
    }
}
