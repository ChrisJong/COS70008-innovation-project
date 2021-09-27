namespace Manager {

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    using Extension;

    public class MainMenuManager : SingletonMono<MainMenuManager> {

        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject verify;
        [SerializeField] private GameObject selection;
        [SerializeField] private GameObject letters;
        [SerializeField] private GameObject numbers;
        [SerializeField] private GameObject drawManager;

        [SerializeField] private InputField nameInputField;
        [SerializeField] private Text verifyNameInputField;

        [SerializeField] private string childName;

        public override void Init() {
            throw new System.NotImplementedException();
        }

        public void GetName() {
            this.childName = this.nameInputField.text.ToUpper();
            this.verifyNameInputField.text = this.childName;

            this.ShowVerify();
        }

        public void VerifyName() {

        }

        public void ShowMenu() {
            this.drawManager.GetComponent<DrawManager>().ClearLines();

            this.menu.SetActive(true);
            this.verify.SetActive(false);
            this.selection.SetActive(false);
            this.drawManager.SetActive(false);
        }

        public void ShowVerify() {
            this.menu.SetActive(false);
            this.verify.SetActive(true);
            this.selection.SetActive(false);
            this.drawManager.SetActive(true);
        }

        public void ShowSelection() {
            this.drawManager.GetComponent<DrawManager>().ClearLines();
            
            this.menu.SetActive(false);
            this.verify.SetActive(false);
            this.selection.SetActive(true);
            this.letters.SetActive(false);
            this.numbers.SetActive(false);
            this.drawManager.SetActive(false);
        }

        public void ShowLetters() {
            this.menu.SetActive(false);
            this.verify.SetActive(false);
            this.selection.SetActive(false);
            this.letters.SetActive(true);
            this.numbers.SetActive(false);
            this.drawManager.SetActive(false);
        }

        public void ShowNumbers() {
            this.menu.SetActive(false);
            this.verify.SetActive(false);
            this.selection.SetActive(false);
            this.letters.SetActive(false);
            this.numbers.SetActive(true);
            this.drawManager.SetActive(false);
        }

        public void CheckSelection(string type) {
            GlobalManager.instance.ChangeScene(type);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}