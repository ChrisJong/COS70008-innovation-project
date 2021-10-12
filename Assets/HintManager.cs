namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;
    using PathCreation;

    public class HintManager : SingletonMono<HintManager>
    {
        public GameObject hand;

        public Transform pathsObject;

        [SerializeField] private List<PathCreator> _paths;

        [SerializeField] private bool _showingHints = false;

        [SerializeField] private float _startHintAt = 5.0f;
        [SerializeField] private float _timer = 0.0f;
        [SerializeField] private float _handSpeed = 5.0f;
        float distance 

        public override void Awake()
        {
            base.Awake();

            this.GetAllPaths();
        }

        private void Update()
        {
            if(Input.touchCount == 0 || !Input.GetMouseButtonDown(0))
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