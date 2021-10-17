namespace Manager
{
    using System;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    using Utils;

    public class VerifyManager : MonoBehaviour
    {
        [SerializeField] private string childName;

        [SerializeField] private TextMeshProUGUI nameTextField;
        [SerializeField] public Text detectedTextField;
        private TesseractDriver _tesseractDriver;
        private string _text = ""; // Detected text
        private string _error = "";
        private bool _completed = false;
        private bool _attempted = false;

        public Camera ScreenshotCamera; // Camera which takes screenshot of layer 6
        public Camera MainCamera; // Main Camera needed because it toggle it when taking screenshot
        private Texture2D ScreenshotTexture; // The screenshot text which is input to Tesseract recognise
        public Texture2D HighlightedTexture;
        private RenderTexture RenderTexture;

        [Space(10), Header("Audio")]
        public AudioClip SuccessAudioClip;
        public AudioClip FailureAudioClip;

        [Space(10), Header("Particles")]
        public ParticleSystem successParticleSystem;

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
            this.childName = PlayerPrefs.GetString("name");
            if (this.childName.Length == 0 || this.childName == "")
                this.childName = "ERROR";

            this.nameTextField.text = this.childName;
        }

        public void Check()
        {
            // When completed activity or there was an error with tesseract
            if (_completed || _error != "" || _attempted)
            {
                string sceneName = "Selection";
                Debug.Log("SelectionManager.instance: " + SelectionManager.instance);
                Debug.Log(SelectionManager.instance.SceneQueue.ToArray().ToString());
                if (SelectionManager.instance != null)
                {
                    Debug.Log(SelectionManager.instance.SceneQueue.ToArray().ToString());
                    if (SelectionManager.instance.SceneQueue.Count > 0)
                    {
                        sceneName = SelectionManager.instance.SceneQueue.Dequeue();
                        Debug.Log("Next scene name came from Queue: " + sceneName);
                    }
                }
                Utility.ChangeScene(sceneName);
                return;
            }
            _attempted = true; // have attempted this activity once
            // Check the name over OCR
            takeScreenshotAndRecognise();
        }

        public void BackHome()
        {
            Utility.ChangeScene("Home");
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

            if (childName.ToLower() == _text.ToLower() || _text.ToLower().Contains(childName.ToLower()))
            {
                onActivityComplete();

            }else
            {
                if(FailureAudioClip != null)
                {
                    if (AudioManager.instance != null)
                    {
                        Debug.Log("Playing sound effect using audio manager");
                        AudioManager.instance.PlaySoundEffect(FailureAudioClip);
                    }
                    else
                    {
                        Debug.Log("Playing sound effect using utility");
                        Utility.PlayOneShot(this.FailureAudioClip);
                    }  
                }
            }
        }

        private void onActivityComplete()
        {
            this._completed = true;
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
                else
                    Utility.PlayOneShot(this.SuccessAudioClip, 0.5f);
            }
            if (successParticleSystem != null)
            {
                Debug.Log("Playing Particle System");
                successParticleSystem.Play();
            }
        }

        public void MoveToNextScene(string sceneName)
        {
            Utility.ChangeScene(sceneName);
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