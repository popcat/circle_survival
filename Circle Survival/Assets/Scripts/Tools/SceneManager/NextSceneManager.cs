using UnityEngine.SceneManagement;

namespace CircleSurvival
{
    class NextSceneManager
    {
        private string nextScene;

        public NextSceneManager(string nextScene)
        {
            this.nextScene = nextScene;
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
