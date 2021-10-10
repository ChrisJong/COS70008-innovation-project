namespace Manager
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class VerifyManager : MonoBehaviour
    {
        [SerializeField] private string childName;

        [SerializeField] private Text nameTextField;
        [SerializeField] public Text detectedTextField;
        private TesseractDriver _tesseractDriver;
        private string _text = ""; // Detected text
        private string _error = "";

        public Camera ScreenshotCamera; // Camera which takes screenshot of layer 6
        public Camera MainCamera; // Main Camera needed because it toggle it when taking screenshot
        private Texture2D ScreenshotTexture; // The screenshot text which is input to Tesseract recognise
        public Texture2D HighlightedTexture;
        private RenderTexture RenderTexture;

        public AudioClip SuccessAudioClip;

        private void Start()
        {
            _tesseractDriver = new TesseractDriver();
        }

        private void Update()
        {
            this.detectedTextField.GetComponent<UnityEngine.UI.Text>().text = _text;
        }

        private void Awake()
        {
            if (GlobalManager.instance != null)
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
            //this.GetComponent<Camera2Screenshot>().takeScreenshot();
            //GlobalManager.instance.ChangeScene("selection");
            takeScreenshotAndRecognise();
        }

        public void BackHome()
        {
            GlobalManager.instance.ChangeScene("home");
        }

        private void Recognize()
        {
            _text = "";
            // Tesseract expects a callback
            _tesseractDriver.Setup(OnSetupCompleteRecognize);
        }

        // Called by Tesseract when recognition is done
        private void OnSetupCompleteRecognize()
        {

            string error = _tesseractDriver.GetErrorMessage();
            if (!string.IsNullOrWhiteSpace(error))
            {
                _error = error;
                return;
            };
            _text = _tesseractDriver.Recognize(ScreenshotTexture);
            _text = _text.Replace(" ", "");
            HighlightedTexture = _tesseractDriver.GetHighlightedTexture();

            Debug.Log("Childname: " + childName);
            Debug.Log("_text: " + _text);

            if (childName.ToLower() == _text.ToLower())
            {
                onActivityComplete();

            }
            GlobalManager.instance.ChangeScene("selection");
        }

        private void onActivityComplete()
        {
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
            }
        }

        private void takeScreenshotAndRecognise()
        {
            Debug.Log("Taking screenshot");
            RenderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
            StartCoroutine("processScreen");
        }

        private IEnumerator processScreen()
        {
            ScreenshotCamera.transform.gameObject.SetActive(true);
            ScreenshotCamera.aspect = MainCamera.aspect;
            ScreenshotCamera.fieldOfView = MainCamera.fieldOfView;
            ScreenshotCamera.orthographic = MainCamera.orthographic;
            ScreenshotCamera.orthographicSize = MainCamera.orthographicSize;
            ScreenshotCamera.backgroundColor = MainCamera.backgroundColor;
            ScreenshotCamera.cullingMask = 6;
            int currentCullingMask = MainCamera.cullingMask;
            MainCamera.cullingMask = 1 << 5;
            MainCamera.cullingMask = ~MainCamera.cullingMask;
            //Screenshot.cullingMask =~ Screenshot.cullingMask;
            yield return new WaitForEndOfFrame();
            ScreenshotCamera.targetTexture = RenderTexture;
            ScreenshotCamera.Render();
            ScreenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            ScreenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            ScreenshotTexture.Apply();
            Recognize();

            // Cleanup
            ScreenshotCamera.targetTexture = null;
            MainCamera.cullingMask = currentCullingMask;
            ScreenshotCamera.transform.gameObject.SetActive(false);
            Debug.Log("Coroutine run");
        }
    }
}