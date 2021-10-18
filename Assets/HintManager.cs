namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using Utils;

    public class HintManager : SingletonMono<HintManager>
    {
        public GameObject hand;

        [SerializeField] private bool _showingHints = false;

        [SerializeField] private float _startHintAt = 5.0f;
        private float _timer = 0.0f;

        private string _currentClip;

        [Space(10), Header("Point To Point")]
        public bool pointToPointMode = false;
        public RectTransform startPoint;
        public RectTransform endPoint;
        [SerializeField] private float _handSpeed = 300.0f;

        private Animator _handAnimator;
        private RectTransform _handTransform;

        public override void Awake()
        {
            base.Awake();

            this._handAnimator = this.hand.GetComponent<Animator>();
            this._handTransform = this.hand.GetComponent<RectTransform>();

            if (this.pointToPointMode)
                this._handTransform.anchoredPosition = this.startPoint.anchoredPosition;

            this.DisableHint();
        }

        public void Update()
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                this._timer = 0.0f;
                this._showingHints = false;
                this.DisableHint();
            }
            else
            {
                if (this.pointToPointMode)
                    this.PointToPointHint();
                else
                    this.AnimatorHint();
            }

        }

        private void PointToPointHint()
        {
            if (this._timer >= this._startHintAt)
            {
                this.EnableHintHand();

                if(Vector2.Distance(this._handTransform.anchoredPosition, this.endPoint.anchoredPosition) <= 0.25f)
                    this._handTransform.anchoredPosition = this.startPoint.anchoredPosition;
                else
                    this._handTransform.anchoredPosition = Vector2.MoveTowards(this._handTransform.anchoredPosition, this.endPoint.anchoredPosition, (this._handSpeed * Time.deltaTime));
            }
            else
                this._timer += Time.deltaTime;
        }

        private void AnimatorHint()
        {
            if (this._showingHints)
                return;

            if (this._timer >= this._startHintAt)
            {
                this._showingHints = true;
                this.EnableHintAnimator(Utility.GetSceneLetterName());
            }
            else
            {
                this._timer += Time.deltaTime;
            }
        }

        private void EnableHintAnimator(string name)
        {
            this._currentClip = name + "-State";
            this._handAnimator.gameObject.SetActive(true);
            this._handAnimator.enabled = true;
            this._handAnimator.Play(this._currentClip);
        }

        private void EnableHintHand()
        {
            this.hand.gameObject.SetActive(true);
        }

        private void DisableHint()
        {
            if (!this._handAnimator.gameObject.activeSelf)
                return;

            this._handAnimator.StopPlayback();
            this._handAnimator.enabled = false;
            this.hand.gameObject.SetActive(false);
        }

    }
}