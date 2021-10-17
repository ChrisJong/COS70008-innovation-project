namespace Manager
{
    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utils;
    using System.Collections.Generic;

    public class SelectionManager : SingletonMono<SelectionManager>
    {

        [SerializeField] private GameObject selectionView;
        [SerializeField] private GameObject lettersView;
        [SerializeField] private GameObject numbersView;
        [SerializeField] public Queue<string> SceneQueue = new Queue<string>();
        [SerializeField] private Dictionary<string, string> sceneDict = new Dictionary<string, string>()
        {
            { "A", "A_Decorate" },
            { "B", "B_Musiceli" },
            { "C", "C_CompleteWrite" },
            { "D", "D_DraggingLetter" },
            { "E", "E_CompleteWord" },
            { "F", "F_Decorate" },
            { "G", "G_CompleteWrite" },
            { "H", "H_Decorate" },
            { "I", "I_CompleteWord" },
            { "J", "J_Musiceli" },
            { "K", "K_CompleteWrite" },
            { "L", "L_Musiceli" },
            { "M", "M_CompleteWord" },
            { "N", "N_Decorate" },
            { "O", "O_DraggingLetter" },
            { "P", "P_Musiceli" },
            { "Q", "Q_CompleteWord" },
            { "R", "R_Decorate" },
            { "S", "S_CompleteWrite" },
            { "T", "T_Decorate" },
            { "U", "U_Decorate" },
            { "V", "V_DraggingLetter" },
            { "W", "W_CompleteWord" },
            { "X", "X_Decorate" },
            { "Y", "Y_Decorate" },
            { "Z", "Z_CompleteWrite" }
        };

        public override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }

        public void ShowSelection()
        {
            this.selectionView.SetActive(true);
            this.lettersView.SetActive(false);
            this.numbersView.SetActive(false);
        }

        public void ShowLetters()
        {
            SceneQueue.Clear();
            this.selectionView.SetActive(false);
            this.lettersView.SetActive(true);
            this.numbersView.SetActive(false);
        }

        public void ShowNumbers()
        {
            SceneQueue.Clear();
            this.selectionView.SetActive(false);
            this.lettersView.SetActive(false);
            this.numbersView.SetActive(true);
        }

        public void BackHome()
        {
            Utility.ChangeScene("Home");
        }

        public void StartActivity(string sceneName)
        {
            Utility.ChangeScene(sceneName);
        }

        public void StartNameActivity()
        {
            string name = PlayerPrefs.GetString("name");
            if(name != null && name != "")
            {
                SceneQueue.Enqueue("Verify");
                char[] letters = name.ToUpper().ToCharArray();
                foreach(char c in letters)
                {
                    string sceneName = sceneDict[c.ToString()];
                    if(sceneName != null)
                        SceneQueue.Enqueue(sceneName);
                }

            }
            foreach (string sceneName in SceneQueue)
            {
                Debug.Log(sceneName);
            }
            string nextScene = SceneQueue.Dequeue();
            StartActivity(nextScene);
        }
    }
}