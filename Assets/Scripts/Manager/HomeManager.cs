namespace Manager
{
    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utils;

    public class HomeManager : SingletonMono<HomeManager>
    {
        private string childName = "";

        [SerializeField] private InputField nameInputField;

        public void GetName()
        {
            this.childName = this.nameInputField.text.ToUpper();

            PlayerPrefs.SetString("name", this.childName);

            Utility.ChangeScene("Verify");
        }

    }
}