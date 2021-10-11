namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;

    public class CompleteWritingManager : SingletonMono<CompleteWritingManager>
    {
        [SerializeField] private bool _completed = false;

        [SerializeField] private Image _letterImage;
        [SerializeField] private Image _pictureImage;

        [SerializeField] private Transform _pointsObject;

        [SerializeField] private List<SequencePoint> _sequencePoints;

        [Space(10), Header("Editable")]
        [SerializeField] private Sprite _letterOutlineSprite;
        [SerializeField] private Sprite _pictureOutlineSprite;
        [SerializeField] private Sprite _pictureCompleteSprite;

        public Text textField;

        public string completedText;

        [Space(10), Header("Audio")]
        public AudioClip SuccessAudioClip;

        public override void Awake()
        {
            base.Awake();

            this.GetAllPoints();
        }

        public void Start()
        {
            this._letterImage.sprite = this._letterOutlineSprite;
            this._pictureImage.sprite = this._pictureOutlineSprite;
        }

        public void Check()
        {
            if (this._sequencePoints.Count == 0 || this._sequencePoints == null)
                return;

            foreach (SequencePoint seqPoint in this._sequencePoints)
            {
                if (!seqPoint.complete)
                    return;
            }

            if (this._pictureImage != null)
            {
                this._pictureImage.sprite = this._pictureCompleteSprite;
            }

            if (textField != null && completedText != null)
            {
                textField.text = completedText;
            }
            onActivityComplete();
        }

        public void MoveToNextScene(string sceneName)
        {
            GlobalManager.instance.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            this._completed = true;
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
            }
        }

        private void GetAllPoints()
        {
            this._sequencePoints = new List<SequencePoint>();

            if (this._pointsObject != null)
            {
                if (this._pointsObject.transform.childCount == 0)
                {
                    Debug.LogWarning("There Are No Points Found!");
                    return;
                }

                foreach (Transform pointObject in this._pointsObject.transform)
                {
                    SequencePoint sqPoint = pointObject.GetComponent<SequencePoint>();
                    if (sqPoint != null)
                        this._sequencePoints.Add(sqPoint);
                }
            }
            else
            {
                Debug.LogWarning("Please Add The Points Object!");
            }
        }
    }
}