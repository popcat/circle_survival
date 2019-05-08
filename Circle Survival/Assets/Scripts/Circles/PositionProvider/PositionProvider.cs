using UnityEngine;

namespace CircleSurvival
{
    public class PositionProvider : IPositionProvider
    {
        private float circleRadius;
        private float xLeft, xRight, yBottom, yTop;
        private Vector2 position;

        public PositionProvider(float circleRadius, ScreenData screenData)
        {
            this.circleRadius = circleRadius;
            xLeft = screenData.XLeftBoundry + circleRadius;
            xRight = screenData.XRightBoundry - circleRadius;
            yBottom = screenData.YBottomBoundry + circleRadius;
            yTop = screenData.YTopBoundry - circleRadius;
        }

        public PositionProvider(float circleRadius, float xLeft, float xRight, float yBottom, float yTop)
        {
            this.circleRadius = circleRadius;
            this.xLeft = xLeft;
            this.xRight = xRight;
            this.yBottom = yBottom;
            this.yTop = yTop;
        }

        public Vector2 GetPosition()
        {
            bool valid;
            do
            {
                position.x = UnityEngine.Random.Range(xLeft, xRight);
                position.y = UnityEngine.Random.Range(yBottom, yTop);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(position, circleRadius);
                valid = true;
                foreach(Collider2D col in colliders)
                {
                    if(col.gameObject.GetComponentsInChildren<ICollider>() != null)
                    {
                        valid = false;
                        break;
                    }
                }
            } while (!valid) ;

            return position;
        }
    }
}
