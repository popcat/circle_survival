using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CircleSurvival {
    public class StartSceneInitializer : MonoBehaviour
    {
        #region VARIABLES

        [Header("Camera variables")]
        [SerializeField] private float cameraWidth;

        [Header("UI variables")]
        [SerializeField] private GameObject menuLayout;

        private ScreenData screenData;
        private PlayerScore playerScore;

        private GameObject playButton;
        #endregion

        private void Awake()
        {
            ISaveManager saveManager = new BinarySaveManager(Application.persistentDataPath + "/saveData.dat");
            NextSceneManager sceneManager = new NextSceneManager("GameScene");
            ButtonController buttonController = new ButtonController(menuLayout.GetComponentInChildren<Button>(), sceneManager.LoadNextScene);
            menuLayout.transform.Find("PointsLabel").GetComponent<Text>().text = "" + saveManager.Load().HighScore;
        }
    }
}
