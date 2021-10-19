namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;
    
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    using TMPro;

    using Extension;
    using Utils;

    public class FeedingManager : SingletonMono<FeedingManager>
    {
        public enum Fruit
        {
            APPLE = 1,
            ORANGE = 2,
            PEAR = 3
        };

        public enum Animal
        {
            CHICKEN = 1,
            COW = 2,
            DOG = 3,
            PIG = 4,
            SHEEP = 5
        };

        private bool _completed = false;
        private bool _showEnd = false;

        [SerializeField] private float _startingTime = 60.0f;
        private float _timer = 0.0f;

        [SerializeField] private int _correctAnswers = 0;
        private int _amountOfFruit = 0;

        private string _currentAnswer = "";
        private string _tesseractAnswer = "";

        private Animal _currentAnimal = Animal.CHICKEN;
        private Fruit _currentFruit = Fruit.APPLE;

        private GameObject _currentFruitGroup;

        [Space(10), Header("Assets")]
        [SerializeField] private GameObject _checkButton;
        [SerializeField] private GameObject _completedPanel;

        [SerializeField] private Image _animalImage;

        [SerializeField] private TextMeshProUGUI _currentTextText;
        [SerializeField] private TextMeshProUGUI _currentTimerText;
        [SerializeField] private TextMeshProUGUI _currentCountText;
        [SerializeField] private TextMeshProUGUI _completedAnswersText;

        [SerializeField] private List<Sprite> _animalSprites;
        [SerializeField] private List<Sprite> _fruitSprites;

        [SerializeField] private List<GameObject> _fruitGroup;

        [Space(10), Header("Screenshot")]
        [SerializeField] private Camera _screenshotCamera;
        [SerializeField] private RectTransform _outline;
        [SerializeField] private RenderTexture _renderTexture;
        [SerializeField] private Texture2D _screenshotTexture;
        private TesseractDriver _tesseractDriver;

        public override void Awake()
        {
            base.Awake();

            this._tesseractDriver = new TesseractDriver();
        }

        public void Start()
        {
            this.Restart();
        }

        public void Update()
        {
            if (this._completed)
            {
                if (this._showEnd)
                    return;
                else
                    this.ShowEnd();
            } 
            else
            {
                if (this._timer <= 0.0f)
                {
                    this._completed = true;
                    this._timer = 0.0f;
                    this._currentTimerText.text = "0";
                    this.ClearAnswer();
                }

                this._timer -= Time.deltaTime;
                this._currentTimerText.text = Mathf.RoundToInt(this._timer).ToString();
            }
        }

        public void MoveToScene(string sceneName)
        {
            Utility.ChangeScene(sceneName);
        }

        public void Restart()
        {
            if (DrawingManager.instance != null)
                DrawingManager.instance.CanDraw = true;

            this._completed = false;

            this._timer = this._startingTime;

            this._correctAnswers = 0;

            this._currentCountText.text = this._correctAnswers.ToString();

            this._completedPanel.gameObject.SetActive(false);
            this._checkButton.gameObject.SetActive(true);

            this.ClearAnswer();
            this.ChangeAnswer();
        }

        public void CheckAnswer()
        {
            this.ProcessCamera();
            this.ChangeAnswer();
        }

        private  void ClearAnswer()
        {
            if (DrawingManager.instance != null)
                DrawingManager.instance.ClearLines();
        }

        private void ChangeAnswer()
        {
            this._amountOfFruit = Random.Range(1, 3);
            int temp = Random.Range(1, 6);
            if (temp == 6) temp = 5;
            this._currentAnimal = (Animal)temp;
            temp = Random.Range(1, 4);
            if (temp == 4) temp = 3;
            this._currentFruit = (Fruit)temp;

            if (this._currentFruitGroup != null)
                this._currentFruitGroup.SetActive(false);

            this._currentFruitGroup = this._fruitGroup[this._amountOfFruit - 1];
            this._currentFruitGroup.SetActive(true);

            this._currentAnswer = this._amountOfFruit.ToString() + ((this._amountOfFruit <= 1) ? this._currentFruit.ToString() : this._currentFruit.ToString()+"S");
            this._currentTextText.text = this._amountOfFruit.ToString() + " " + ((this._amountOfFruit <= 1) ? this._currentFruit.ToString() : this._currentFruit.ToString() + "S");

            this.ChangeSprites();
            this.ClearAnswer();
        }

        private void ChangeSprites()
        {
            Image[] fruitsImage = this._currentFruitGroup.GetComponentsInChildren<Image>();

            if (fruitsImage.Length == 0)
                return;

            foreach(Image fruitImage in fruitsImage)
            {
                fruitImage.sprite = this._fruitSprites[((int)this._currentFruit) - 1];
            }

            this._animalImage.sprite= this._animalSprites[((int)this._currentAnimal) - 1];
        }

        private void ShowEnd()
        {
            if (DrawingManager.instance != null)
                DrawingManager.instance.CanDraw = false;

            this._showEnd = true;

            this._currentTextText.text = string.Empty;
            this._completedAnswersText.text = this._correctAnswers.ToString();


            this._completedPanel.gameObject.SetActive(true);
            this._checkButton.gameObject.SetActive(false);

            this.ClearAnswer();
        }

        private void ProcessCamera()
        {
            this._renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
            this._renderTexture.name = "feedingRenderTexture";
            this._screenshotCamera.targetTexture = this._renderTexture;
            this._screenshotCamera.Render();

            this._screenshotTexture = this._renderTexture.ToTexture2D();
            this._screenshotTexture.name = "feedingScreenshotTexture2D";

            this._tesseractDriver.Setup(SetupTesseract);
        }

        private void SetupTesseract()
        {
            string error = this._tesseractDriver.GetErrorMessage();

            if (!string.IsNullOrWhiteSpace(error))
            {
                Debug.LogError(error);
                return;
            }

            this._tesseractAnswer = this._tesseractDriver.Recognize(this._screenshotTexture);
            this._tesseractAnswer = this._tesseractAnswer.Replace(" ", string.Empty);

            if (this._currentAnswer.ToUpper().Equals(this._tesseractAnswer.ToUpper()))
            {
                Debug.Log("Correct");
                this._correctAnswers++;
                this._currentCountText.text = this._correctAnswers.ToString();
            }

        }
    }
}