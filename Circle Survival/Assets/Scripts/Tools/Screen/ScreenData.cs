using System;
using UnityEngine;
namespace CircleSurvival
{
    public class ScreenData
    {
        public Camera MainCamera { get; private set; }

        public float ScreenWidth { get; private set; }
        public float ScreenWidthHalf { get; private set; }
        public float ScreenHeight { get; private set; }
        public float ScreenHeightHalf { get; private set; }
        public float Aspect { get; private set; }

        public Vector2 ScreenMiddle { get; private set; }
        public float XLeftBoundry { get; private set; }
        public float XRightBoundry { get; private set; }
        public float YBottomBoundry { get; private set; }
        public float YTopBoundry { get; private set; }

        public ScreenData()
        {
            SetData();
        }

        public void SetFixedWidth(float width)
        {
            if (width > 0) {
                float fitHeight = (width / 2) * Aspect;
                MainCamera.orthographicSize = fitHeight;
            }
            SetData();
        }

        public void SetFixedHeight(float height)
        {
            if (height > 0)
            {
                MainCamera.orthographicSize = height/2;
            }
            SetData();
        }


        private void SetData()
        {
            MainCamera = Camera.main;
            Aspect = MainCamera.aspect;

            ScreenHeightHalf = MainCamera.orthographicSize;
            ScreenHeight = ScreenHeightHalf * 2;

            ScreenWidthHalf = ScreenHeightHalf * Aspect;
            ScreenWidth = ScreenWidthHalf * 2;

            ScreenMiddle = MainCamera.transform.position;
            XLeftBoundry = ScreenMiddle.x - ScreenWidthHalf;
            XRightBoundry = ScreenMiddle.x + ScreenWidthHalf;
            YBottomBoundry = ScreenMiddle.y - ScreenHeightHalf;
            YTopBoundry = ScreenMiddle.y + ScreenHeightHalf;
        }

    }
}
