namespace Manager {

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.SceneManagement;

    using Extension;

    public class GlobalManager : SingletonMono<GlobalManager> {

        [SerializeField] private string currentType;
        [SerializeField] private string mainMenu;
        [SerializeField] private List<string> letters;
        [SerializeField] private List<string> numbers;

        public override void Init() {
            throw new System.NotImplementedException();
        }

        public void ChangeScene(string type) {
            Debug.Log(type);
            this.currentType = type;

            if (type == "letters" && this.letters.Count != 0)
            {
                int activity = Random.Range(0, this.letters.Count);

                Debug.Log("Letter Activity:" + this.letters[activity]);
                DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene(this.letters[activity]);

            }
            else if (type == "numbers")
            {
                int activity = Random.Range(0, this.numbers.Count);

                Debug.Log("Numbers Activity:" + this.numbers[activity]);
                DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene(this.numbers[activity]);

            }
            else if (type == "colours")
            {
                Debug.Log("Colours Scene");
            }
            else if (type == "selection")
            {
                DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene(this.mainMenu);
                MainMenuManager.instance.ShowSelection();
            }
            else
            {
                Debug.Log("No Activities For: " + type);
                return;
            }
        }
    }
}