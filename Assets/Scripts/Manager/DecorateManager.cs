namespace Manager
{

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;

    public class DecorateManager : SingletonMono<DecorateManager>
    {
        [SerializeField] private bool _completed = false;

        [SerializeField] private Image _letterImage;

        [SerializeField] private Transform _pointsObject;

        [SerializeField] private List<SequencePoint> _sequencePoints;

        [Space(10), Header("Editable")]
        [SerializeField] private Sprite _letterOutlineSprite;
        [SerializeField] private Sprite _letterCompleteSprite;

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

        public void BackToSelection()
        {
            GlobalManager.instance.ChangeScene("selection");
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