using UnityEngine;

namespace CircleSurvival
{
    /***
    * Provides position on the screen based on the radius of the collider
    * colliders cant stack on each other
    ***/
    public class PositionProvider : IPositionProvider
    {
        private readonly float circleRadius;
        private readonly float xLeft, xRight, yBottom, yTop;
        private Vector2 position;

        public PositionProvider(float circleRadius, ScreenData screenData)
        {
            this.circleRadius = circleRadius;
            xLeft = screenData.XLeftBoundry + circleRadius;
            xRight = screenData.XRightBoundry - circleRadius;
            yBottom = screenData.YBottomBoundry + circleRadius;
            yTop = screenData.YTopBoundry - circleRadius;
        }

        public Vector2 GetPosition()
        {
            bool valid;
            do
            {
                position.x = Random.Range(xLeft, xRight);
                position.y = Random.Range(yBottom, yTop);

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
