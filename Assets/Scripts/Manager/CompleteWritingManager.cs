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
        
        [Space(10), Header("Editable")]
        [SerializeField] private Sprite _letterOutlineSprite;
        [SerializeField] private Sprite _pictureOutlineSprite;
        [SerializeField] private Sprite _pictureCompleteSprite;

        [SerializeField] private List<SequencePoint> _sequencePoints;

        public void Start()
        {
            this._letterImage.sprite = this._letterOutlineSprite;
            this._pictureImage.sprite = this._pictureOutlineSprite;
        }

        public void Check()
        {
            if (this._sequencePoints.Count == 0 || this._sequencePoints == null)
                return;

            foreach(SequencePoint seqPoint in this._sequencePoints)
            {
                if (!seqPoint.complete)
                    return;
            }

            if (this._pictureImage != null)
            {
                this._pictureImage.sprite = this._pictureCompleteSprite;
            }

            this._completed = true;
        }
    }
}