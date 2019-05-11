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

        [Header("Camera variables")]
        [SerializeField] private float cameraWidth;

        [Header("Screen Shake variables")]
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeMagnitude;
        [SerializeField] private float shakeInterval;

        [Header("UI variables")]
        [SerializeField] private GameObject scoreLabel;
        [SerializeField] private GameObject gameOverLayout;
        [SerializeField] private float gameOverDelay;

        private ScreenData screenData;
        private GameCoroutineRunner mainCoroutineRunner;
        private SingleEvent startManager;
        private SingleEvent deathManager;
        private CircleSpawner circleSpawner;
        private ScreenShake screenShake;
        private TouchManager touchManager;
        private ScoreManager scoreManager;
        private ScoreTextController scoreTextController;
        private PlayerScore playerScore;

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
            CreateScoreManager();
            CreateGameOverUI();

            StartManagerSubscriptions();
            DeathManagerSubscriptions(); 
        }

        private void Start()
        {
            startManager.InvokeEvent();
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
            playerScore = new PlayerScore();
            scoreTextController = new ScoreTextController(scoreLabel.GetComponent<Text>(), "SCORE: ");
            scoreManager = new ScoreManager(playerScore, scoreTextController, mainCoroutineRunner, scorePoints: 5);
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
            NextSceneManager sceneManager = new NextSceneManager("StartScene");
            ButtonController buttonController = new ButtonController(gameOverLayout.GetComponentInChildren<Button>(), sceneManager.LoadNextScene);
            gameOverManager = new GameOverManager(gameOverLayout, mainCoroutineRunner, gameOverDelay, playerScore);
        }
        #endregion

        #region SUBSCRIPTIONS
        private void StartManagerSubscriptions()
        {
            startManager.Subscribe(circleSpawner.StartSpawning);
            startManager.Subscribe(scoreManager.StartScoreCounter);
            startManager.Subscribe(scoreTextController.SetActive);
        }
        private void DeathManagerSubscriptions()
        {
            deathManager.Subscribe(scoreManager.StopScoreCounter);
            deathManager.Subscribe(circleSpawner.StopSpawning);
            deathManager.Subscribe(touchManager.SetInactive);
            deathManager.Subscribe(screenShake.StartShake);
            deathManager.Subscribe(gameOverManager.ShowLayout);
        }
        #endregion
    }
}
