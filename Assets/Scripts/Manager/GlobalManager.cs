namespace Manager {

    using UnityEngine;
    using UnityEngine.SceneManagement;

    using Extension;

    public class GlobalManager : SingletonMono<GlobalManager> {

        public string childName;
        [SerializeField] private string currentScene;

        public void ChangeScene(string scene) {
            Debug.Log(scene);
            this.currentScene = scene;

            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(scene);
        }

        public void LoadUser(string childName)
        {

        }

        private void LoadUser()
        {

        }
    }
}