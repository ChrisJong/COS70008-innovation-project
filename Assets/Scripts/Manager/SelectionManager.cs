namespace Manager
{
    using UnityEngine;
    using UnityEngine.UI;

    using Extension;

    public class SelectionManager : SingletonMono<SelectionManager>
    {

        [SerializeField] private GameObject selectionView;
        [SerializeField] private GameObject lettersView;
        [SerializeField] private GameObject numbersView;

        public void ShowSelection()
        {
            this.selectionView.SetActive(true);
            this.lettersView.SetActive(false);
            this.numbersView.SetActive(false);
        }

        public void ShowLetters()
        {
            this.selectionView.SetActive(false);
            this.lettersView.SetActive(true);
            this.numbersView.SetActive(false);
        }

        public void ShowNumbers()
        {
            this.selectionView.SetActive(false);
            this.lettersView.SetActive(false);
            this.numbersView.SetActive(true);
        }

        public void BackHome()
        {
            GlobalManager.instance.ChangeScene("home");
        }

        public void StartActivity(string sceneName)
        {
            GlobalManager.instance.ChangeScene(sceneName);
        }
    }
}