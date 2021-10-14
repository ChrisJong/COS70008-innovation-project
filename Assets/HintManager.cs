namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    using Extension;
    using PathCreation;

    public class HintManager : SingletonMono<HintManager>
    {
        public GameObject hand;

        public Transform pathsObject;

        [SerializeField] private Image _handImage;
        [SerializeField] private Animator _handAnimator;

        [SerializeField] private List<PathCreator> _paths;

        [SerializeField] private bool _showingHints = false;

        [SerializeField] private float _startHintAt = 5.0f;
        [SerializeField] private float _timer = 0.0f;
        [SerializeField] private float _handSpeed = 5.0f;

        public override void Awake()
        {
            base.Awake();

            this._handAnimator = this.hand.GetComponent<Animator>();
            this._handImage = this.hand.GetComponent<Image>();
            //DisableHint();
            EnableHint();
            //this.GetAllPaths();
        }

        public void Update()
        {
            if(Input.touchCount == 0 || !Input.GetMouseButtonUp(0))
            {
                if(!this._showingHints)
                    this._timer += Time.deltaTime;

                if(this._timer >= this._startHintAt)
                {
                    this._showingHints = true;
                }
                else
                {
                    this._showingHints = false;
                }
            }
            else
            {
                this._showingHints = false;
                this._timer = 0.0f;
            }
        }

        public void EnableHint()
        {
            this._handAnimator.enabled = true;
            this._handAnimator.Play("C-State");
        }

        public void DisableHint()
        {
            this._handAnimator.enabled = false;
        }

        private void GetAllPaths()
        {
            this._paths = new List<PathCreator>();

            if(this.pathsObject != null)
            {
                if(this.pathsObject.childCount == 0)
                {
                    Debug.LogWarning("There are no paths Found!");
                    return;
                }

                foreach(Transform path in this.pathsObject)
                {
                    PathCreator pathCreator = path.GetComponent<PathCreator>();

                    if (pathCreator != null)
                        this._paths.Add(pathCreator);
                }
            }
            else
            {
                Debug.LogWarning("Please Add The Paths Object!");
            }
        }

    }
}