using UnityEngine;

namespace CircleSurvival
{
    /***
    * Reads player input and interacts with objects in the scene with raycasts              
    * */
    public class TouchManager : MonoBehaviour
    {             
        private ITouchInputWrapper touchInputWrapper;
        private Camera mCamera;


        public void Initialize(ITouchInputWrapper touchInputWrapper)
        {
            this.touchInputWrapper = touchInputWrapper;
        }

        public void SetActive()
        {
            gameObject.SetActive(true);
        }

        public void SetInactive()
        {
            gameObject.SetActive(false);
        }

        public void Start()
        {
            mCamera = Camera.main;
            if (touchInputWrapper == null)
            {
                Debug.Log("NO INPUT WRAPPER, assining default");
                touchInputWrapper = new MouseAndTouchInputWrapper();
            }
        }

        private void Update()
        {
            TouchInput touchInput = touchInputWrapper.GetTouch(0);
            if (touchInput.IsTouching && touchInput.TouchPhase.Equals(TouchPhase.Began))
            {
                Debug.Log("Touch initiated: " + touchInput.GetPosition());
                Vector2 worldPosition = mCamera.ScreenToWorldPoint(touchInput.GetPosition());
                RaycastHit2D hit = Physics2D.Raycast(
                    worldPosition, mCamera.transform.forward, 1);

                if(hit.collider != null)
                {
                    Debug.Log("Touch collision: " + hit.collider.gameObject);
                    hit.collider.gameObject.GetComponentInChildren<ICollider>()?.TriggerCollision();
                }
            }
        }
    }
}
