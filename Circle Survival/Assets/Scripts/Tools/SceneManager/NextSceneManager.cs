using UnityEngine.SceneManagement;

namespace CircleSurvival
{
    /***
     * Loads next scene
     * */
    class NextSceneManager
    {
        private readonly string nextScene;

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
