namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utils;

    public class DecorateManager : SingletonMono<DecorateManager>
    {
        [Space(10), Header("Main Components")]
        [SerializeField] private bool _completed = false;

        [SerializeField] private Image _letterImage;

        [SerializeField] private Transform _pointsObject;

        [SerializeField] private List<SequencePoint> _sequencePoints;

        [Space(10), Header("Editable")]
        [SerializeField] private Sprite _letterOutlineSprite;
        [SerializeField] private Sprite _letterCompleteSprite;

        [Space(10), Header("Audio")]
        public AudioClip startLetterAudioClip;
        public AudioClip SuccessAudioClip;

        public ParticleSystem successParticleSystem;

        public override void Awake()
        {
            base.Awake();

            this.GetAllPoints();
        }

        public void Start()
        {
            this._letterImage.sprite = this._letterOutlineSprite;

            if (AudioManager.instance != null)
                AudioManager.instance.PlaySoundEffect(startLetterAudioClip);
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

            if (this._letterImage != null)
            {
                this._letterImage.sprite = this._letterCompleteSprite;
            }

            if (DrawingManager.instance != null)
            {
                DrawingManager.instance.CanDraw = false;
                DrawingManager.instance.ClearLines();
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
            if (SuccessAudioClip != null && AudioManager.instance != null)
            {
                Debug.Log("Playing sound effect using audio manager");
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySoundEffect(SuccessAudioClip);
                else
                    Utility.PlayOneShot(this.SuccessAudioClip);
            }
            if(successParticleSystem != null)
            {
                Debug.Log("Playing Particle System");
                successParticleSystem.Play();
            }

        }

        private void GetAllPoints()
        {
            this._sequencePoints = new List<SequencePoint>();

            if(this._pointsObject != null)
            {
                if(this._pointsObject.childCount == 0)
                {
                    Debug.LogWarning("There Are No Points Found!");
                    return;
                }

                foreach(Transform pointObject in this._pointsObject)
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