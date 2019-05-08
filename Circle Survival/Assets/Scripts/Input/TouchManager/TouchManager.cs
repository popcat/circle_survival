using UnityEngine;

namespace CircleSurvival
{
    public class TouchManager : MonoBehaviour
    {
        /***
         * TouchManager reads player input and interacts with object in the scene        
         * Use touch input wrapper for mobile play
         * Use mouse input wrapper for editor testing              
         * */  
               
        private ITouchInputWrapper touchInputWrapper;
        private Camera mCamera;


        public void Initialize(ITouchInputWrapper touchInputWrapper)
        {
            this.touchInputWrapper = touchInputWrapper;
        }

        public void Start()
        {
            mCamera = Camera.main;
            if (touchInputWrapper == null)
            {
                Debug.Log("NO INPUT WRAPPER, assining default");
                touchInputWrapper = new MouseInputWrapper();
            }
        }

        // Update is called once per frame
        void Update()
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
