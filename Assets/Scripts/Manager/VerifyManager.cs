namespace Manager
{
    using UnityEngine;
    using UnityEngine.UI;

    public class VerifyManager : MonoBehaviour
    {
        [SerializeField] private string childName;

        [SerializeField] private Text nameTextField;

        private void Awake()
        {
            if(GlobalManager.instance != null)
            {
                this.childName = GlobalManager.instance.childName;
                this.nameTextField.text = this.childName;
            }
            else
            {
                Debug.LogWarning("Global Manager Not Found!");
                this.childName = PlayerPrefs.GetString("name");
                this.nameTextField.text = this.childName;
            }
        }

        public void VerifyName() 
        {
            // Check the name over OCR

            GlobalManager.instance.ChangeScene("selection");
        }

        public void BackHome()
        {
            GlobalManager.instance.ChangeScene("home");
        }
    }
}