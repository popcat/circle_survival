using UnityEngine;

namespace CircleSurvival
{
    public class GameSceneInitializer : MonoBehaviour
    {
        #region VARIABLES
        [Header("Touch variables")]
        [SerializeField] private GameObject touchManagerPrefab;

        [Header("Circle variables")]
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private int circleOptimalCount;
        [SerializeField] private float circleSpawnTimeInterval;
        [SerializeField] private float circleDeltaSpawnTime;

        [Header("Green Circle variables")]
        [SerializeField] private Color greenColor;
        [SerializeField] private float greenMinTapTime;
        [SerializeField] private float greenMaxTapTime;
        [SerializeField] private float greenDeltaTapTime;

        [Header("Black Circle variables")]
        [SerializeField] private Color blackColor;
        [SerializeField] private float blackTapTime;

        [Header("Screen Shake variables")]
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeMagnitude;
        [SerializeField] private float shakeInterval;

        [Header("Camera variables")]
        [SerializeField] private float cameraWidth;

        private ScreenData screenData;
        private ICoroutineRunner mainCoroutineRunner;
        private ISimpleEvent deathManager;
        private ISpawner circleSpawner;
        private ScreenShake screenShake;
        #endregion

        #region UNITY METHODS
        private void Awake()
        {
            CreateScreenData();
            CreateCoroutineRunners();
            CreateDeathManager();
            CreateTouchManager();
            CreateCircles();
            CreateScreenShake();
        }

        private void Start()
        {
            //todo change it to starting event
            circleSpawner.StartSpawning();
        }
        #endregion

        #region CREATE METHODS
        private void CreateScreenData()
        {
            screenData = new ScreenData();
            screenData.SetFixedWidth(cameraWidth);
        }

        private void CreateScreenShake()
        {
            screenShake = new ScreenShake(screenData.MainCamera.transform, shakeDuration
                , shakeMagnitude, shakeInterval, mainCoroutineRunner);
        }


        private void CreateCoroutineRunners()
        {
            GameObject mainRunnerObject = new GameObject();
            mainCoroutineRunner = mainRunnerObject.AddComponent<GameCoroutineRunner>();
            mainRunnerObject.name = "Main Coroutine Runner";
        }

        private void CreateDeathManager()
        {
            deathManager = new SingleEvent();
        }

        private void CreateTouchManager()
        {
            GameObject touchManager = Instantiate(touchManagerPrefab);
            TouchManager touchManagerComponent = touchManager.GetComponent<TouchManager>();
            ITouchInputWrapper touchInputWrapper = new MouseInputWrapper();

            touchManagerComponent.Initialize(touchInputWrapper);
        }

        private void CreateCircles()
        {
            GameObject poolRoot = new GameObject();
            poolRoot.name = "Circle Pool";

            IObjectPool<GameObject> circlePool
                = new ExpandableGameObjectPool(circlePrefab, poolRoot.transform, circleOptimalCount
            );

            ICircleProvider greenCircleProvider = new GreenCircleProvider(
                circlePool, deathManager.InvokeEvent, greenMinTapTime
                , greenMaxTapTime, greenDeltaTapTime, greenColor
            );

            ICircleProvider blackCircleProvider = new BlackCircleProvider(
                circlePool, deathManager.InvokeEvent, blackTapTime, blackColor
            );

            float radius = circlePrefab.GetComponent<CircleCollider2D>().radius;
            IPositionProvider positionProvider = new PositionProvider(
                radius, screenData
            );

            circleSpawner = new CircleSpawner(
                greenCircleProvider, blackCircleProvider, positionProvider,
                 mainCoroutineRunner, circleSpawnTimeInterval, circleDeltaSpawnTime
            );
        }
        #endregion

        #region SUBSCRIPTIONS
        private void DeathManagerSubcriptions()
        {
            deathManager.Subscribe(screenShake.StartShake);
        }

        #endregion
    }
}
