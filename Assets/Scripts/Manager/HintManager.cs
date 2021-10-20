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

        public bool isEnabled = true;
        [SerializeField] private bool _showingHints = false;

        private float _startHintAt = 3.0f;
        private float _timer = 0.0f;

        private string _currentClip;

        [Space(10), Header("Custom Hint Mode")]
        public bool enableCustomMode = false;
        public string animationName = "";

        [Space(10), Header("Point To Point Mode")]
        public bool pointToPointMode = false;
        public RectTransform startPoint;
        public RectTransform endPoint;
        private float _handSpeed = 500.0f;

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
            if (!this.isEnabled)
                return;

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
                else if (this.enableCustomMode)
                    this.CustomHintMode();
                else
                    this.AnimatorHint();
            }

        }

        public void PassCustomAnimation(string animName)
        {
            this.animationName = animName + "-State";
        }

        private void PointToPointHint()
        {
            if (this._timer >= this._startHintAt)
            {
                if(!this._showingHints)
                    this.EnableHintHand();

                if(Vector2.Distance(this._handTransform.anchoredPosition, this.endPoint.anchoredPosition) <= 0.25f)
                    this._handTransform.anchoredPosition = this.startPoint.anchoredPosition;
                else
                    this._handTransform.anchoredPosition = Vector2.MoveTowards(this._handTransform.anchoredPosition, this.endPoint.anchoredPosition, (this._handSpeed * Time.deltaTime));
            }
            else
                this._timer += Time.deltaTime;
        }

        private void CustomHintMode()
        {
            if (this._showingHints)
                return;

            if (this._timer >= this._startHintAt)
            {
                if (this.animationName == "")
                {
                    Debug.LogError("No Animation To Play");
                    return;
                }

                if (!this._showingHints)
                    this.EnableHint();

                this._showingHints = true;
                this._handAnimator.gameObject.SetActive(true);
                this._handAnimator.enabled = true;
                this._handAnimator.Play(this.animationName);
            }
            else
            {
                this._timer += Time.deltaTime;
            }
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
            this._showingHints = true;
            this.hand.gameObject.SetActive(true);
            this._handTransform.anchoredPosition = this.startPoint.anchoredPosition;
        }

        public void DisableHint()
        {
            if (!this._handAnimator.gameObject.activeSelf)
                return;

            this._handAnimator.StopPlayback();
            this._handAnimator.enabled = false;
            this.hand.gameObject.SetActive(false);
            this._showingHints = false;
            this._timer = 0.0f;
        }

        public void EnableHint()
        {
            this._handAnimator.enabled = true;
            this.hand.gameObject.SetActive(true);
        }
    }
}