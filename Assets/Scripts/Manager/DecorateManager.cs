namespace Manager {

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;

    public class DecorateManager : SingletonMono<DecorateManager> 
    {
        [SerializeField] private bool _completed = false;

        [SerializeField] private Image _letterImage;

        [Space(10), Header("Editable")]
        [SerializeField] private Sprite _letterOutlineSprite;
        [SerializeField] private Sprite _letterCompleteSprite;

        [SerializeField] private List<SequencePoint> _sequencePoints;

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

            this._completed = true;
        }

        public void BackToSelection()
        {
            GlobalManager.instance.ChangeScene("selection");
        }
    }
}