using UnityEngine;
using UnityEngine.UI;

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

        [Header("UI variables")]
        [SerializeField] private GameObject gameOverButtonPrefab;

        private ScreenData screenData;
        private GameCoroutineRunner mainCoroutineRunner;
        private SingleEvent startManager;
        private SingleEvent deathManager;
        private CircleSpawner circleSpawner;
        private ScreenShake screenShake;
        private TouchManager touchManager;
        private ScoreManager scoreManager;

        private GameOverManager gameOverManager;
        private GameObject gameOverButton;

        #endregion

        #region UNITY METHODS
        private void Awake()
        {
            CreateScreenData();
            CreateCoroutineRunners();
            CreateEventManagers();
            CreateTouchManager();
            CreateCircles();
            CreateScreenShake();
            CreateGameOverUI();

            DeathManagerSubcriptions();
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

        private void CreateScoreManager()
        {
            //scoreManager = new ScoreManager();
        }

        private void CreateEventManagers()
        {
            startManager = new SingleEvent();
            deathManager = new SingleEvent();
        }

        private void CreateTouchManager()
        {
            GameObject touchManagerObj = Instantiate(touchManagerPrefab);
            touchManager = touchManagerObj.GetComponent<TouchManager>();
            ITouchInputWrapper touchInputWrapper = new MouseInputWrapper();

            touchManager.Initialize(touchInputWrapper);
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

        private void CreateGameOverUI()
        {

            gameOverButton = Instantiate(gameOverButtonPrefab);
            //todo gameOverButton.GetComponent<ButtonController>().Initialize();
            gameOverButton.SetActive(false);

            gameOverManager = new GameOverManager();
        }

        #endregion

        #region SUBSCRIPTIONS
        private void StartManagerSubscriptions()
        {
            startManager.Subscribe(circleSpawner.StartSpawning);
            //todo startManager.Subscribe()
        }
        private void DeathManagerSubcriptions()
        {
            deathManager.Subscribe(screenShake.StartShake);
            deathManager.Subscribe(circleSpawner.StopSpawning);
            deathManager.Subscribe(touchManager.SetInactive);
            //deathManager.Subscribe(gameOverManager.ShowLayout);
        }
        #endregion
    }
}
