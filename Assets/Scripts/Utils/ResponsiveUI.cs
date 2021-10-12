namespace Utils
{
    using UnityEngine;

    public class ResponsiveUI : MonoBehaviour
    {
        public enum ScaleType
        {
            NONE = 0,
            ASPECT_RATIO,
            PERCENTAGE
        };

        public enum StartMode
        {
            NONE = 0,
            AWAKE,
            START,
            UPDATE
        }

        public ScaleType scaleType = ScaleType.ASPECT_RATIO;
        public StartMode startMode = StartMode.AWAKE;

        public float xPosFraction, yPosFraction = 0.5f;
        public float xScaleFraction, yScaleFraction = 0.1f;
        public float aspectRatioFraction = 1.0f;

        public bool scaleUI, translateUI, isEnabled = true;
        public Camera mainCamera;

        private void Awake()
        {
            if (this.mainCamera == null)
                this.mainCamera = Camera.main;

            if (this.isEnabled)
            {
                if (this.startMode == StartMode.AWAKE)
                    SetPositionAndScale();
            }
        }

        private void Start()
        {
            if (this.isEnabled)
            {
                if (this.startMode == StartMode.START)
                    SetPositionAndScale();
            }
        }

        private void Update()
        {
            if (this.isEnabled)
            {
                if (this.startMode == StartMode.UPDATE)
                    SetPositionAndScale();
            }
        }

        private void SetPositionAndScale()
        {
            if (translateUI)
                this.PercentagePositioning();

            if (scaleUI)
            {
                if (this.scaleType == ScaleType.ASPECT_RATIO)
                    this.AspectRatioScaling();
                else if (this.scaleType == ScaleType.PERCENTAGE)
                    this.PercentageScaling();

            }
        }

        private void PercentagePositioning()
        {
            int swidth = Screen.width;
            int sheight = Screen.height;

            Vector3 positionworldvector = this.mainCamera.ScreenToWorldPoint(new Vector2(swidth * this.xPosFraction, sheight * this.yPosFraction)); //screen pixel position to world position
            this.transform.position = new Vector3(positionworldvector.x, positionworldvector.y, transform.position.z); //set new position
        }

        private void PercentageScaling()
        {
            int swidth = Screen.width;
            int sheight = Screen.height;

            Vector3 sccaleworldvector = this.mainCamera.ScreenToWorldPoint(new Vector2(swidth, sheight)); //screen pixel scale to world scale
            this.transform.localScale = new Vector3(sccaleworldvector.x * this.xScaleFraction, sccaleworldvector.y * this.yScaleFraction, transform.localScale.z); //set new scale	
        }

        private void AspectRatioScaling()
        {
            float aspectRatio = this.mainCamera.aspect;
            this.transform.localScale = new Vector3(aspectRatio * this.aspectRatioFraction, aspectRatio * this.aspectRatioFraction, aspectRatio * this.aspectRatioFraction); //set new scale	
        }
    }
}