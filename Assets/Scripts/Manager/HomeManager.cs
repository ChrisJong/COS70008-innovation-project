namespace Manager
{
    using UnityEngine;
    using UnityEngine.UI;

    using Extension;

    public class HomeManager : SingletonMono<HomeManager>
    {
        private string childName = "";

        [SerializeField] private InputField nameInputField;

        public void GetName()
        {
            this.childName = this.nameInputField.text.ToUpper();

            if(GlobalManager.instance != null)
            {
                GlobalManager.instance.childName = this.childName;
            } 
            else
            {
                PlayerPrefs.SetString("name", this.childName);
            }

            GlobalManager.instance.ChangeScene("verify");
        }

        private void Start()
        {
            AudioManager.Instance.PlayMusic();
        }
    }
}