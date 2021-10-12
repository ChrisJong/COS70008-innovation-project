namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utils;

    public class CompleteWritingManager : SingletonMono<CompleteWritingManager>
    {
        [Header("Main Components")]
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
        public AudioClip startLetterAudioClip;
        public AudioClip writingCompleteAudioClip;
        public AudioClip SuccessAudioClip;

        [Space(10), Header("Particles")]
        public ParticleSystem successParticleSystem;

        public override void Awake()
        {
            base.Awake();

            this.GetAllPoints();
        }

        public void Start()
        {
            this._letterImage.sprite = this._letterOutlineSprite;
            this._pictureImage.sprite = this._pictureOutlineSprite;

            if (AudioManager.instance != null)
                AudioManager.instance.PlaySoundEffect(this.startLetterAudioClip);
            else
                Utility.PlayOneShot(this.startLetterAudioClip);
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
            Utility.ChangeScene(sceneName);
        }

        private void onActivityComplete()
        {
            this._completed = true;
            if (SuccessAudioClip != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
                    Utility.PlayOneShot(this.writingCompleteAudioClip);
                }
                else
                {
                    Utility.PlayOneShot(this.writingCompleteAudioClip);
                    Utility.PlayOneShot(this.SuccessAudioClip, 0.5f);
                }
            }
            if (successParticleSystem != null)
            {
                Debug.Log("Playing Particle System");
                successParticleSystem.Play();
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