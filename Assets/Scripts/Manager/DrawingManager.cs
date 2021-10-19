namespace Manager
{
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;

    public class DrawingManager : SingletonMono<DrawingManager>
    {
        [SerializeField] private float _lineWidth = 0.65f;

        [SerializeField] private bool _drawingStarted = false;
        [SerializeField] private bool _canDraw = true;
        public bool CanDraw
        {
            get { return this._canDraw; }
            set { this._canDraw = value; }
        }

        [SerializeField] private Vector3 _mousePosCurrent = Vector3.zero;
        [SerializeField] private Vector3 _mousePosPrevious = Vector3.zero;
        
        [SerializeField] private GameObject _drawingPrefab;
        [SerializeField] private GameObject _currentLine;
        [SerializeField] private DrawingAttributes _currentLineAttributes;

        [SerializeField] private List<DrawingAttributes> _drawCollection;

        [SerializeField] private Color _lineColour;

        private void Start()
        {
            this._drawCollection = new List<DrawingAttributes>();
            this.SetColour();
        }

        private void Update()
        {
            if (!this._canDraw)
                return;

            this._mousePosCurrent = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this._mousePosCurrent.z = -5.0f;
            Debug.DrawRay(_mousePosCurrent, Vector3.forward * 100.0f, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(_mousePosCurrent, Vector2.zero, 100.0f);

            if (hit.collider != null)
            {
                if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
                {
                    //Debug.Log("Begin");
                    this._mousePosPrevious = this._mousePosCurrent;
                    this.HandleDrawing(hit.collider);
                    this._drawingStarted = true;
                }
                else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
                {
                    //Debug.Log("Moving");
                    float dist = Mathf.Abs(Vector3.Distance(this._mousePosCurrent, this._mousePosPrevious));
                    if (dist >= 0.1f)
                        this.HandleDrawing(hit.collider);
                }
                else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    //Debug.Log("Up");
                    this.EndDrawing();
                }
            } 
            else if(hit.collider == null && this._currentLine != null)
            {
                this.EndDrawing();
            } 
            else
            {
                this.EndDrawing();
            }
        }

        public void ClearLines()
        {
            if (this._drawCollection.Count == 0 && this._drawCollection != null)
                return;

            if (this._drawCollection == null)
            {
                this._drawCollection = new List<DrawingAttributes>();
                return;
            }

            foreach (DrawingAttributes line in this._drawCollection)
            {
                line.DestoryLine();
            }

            this._drawCollection.Clear();
        }

        public void SetColour(string hexadecimal)
        {
            ColorUtility.TryParseHtmlString(hexadecimal, out this._lineColour);
            this.SetColour();
        }

        private void SetColour()
        {
            this._lineColour = new Color(this._lineColour.r, this._lineColour.g, this._lineColour.b, 255.0f);

            if(this._drawingPrefab != null)
            {
                this._drawingPrefab.GetComponent<LineRenderer>().startColor = this._lineColour;
                this._drawingPrefab.GetComponent<LineRenderer>().endColor = this._lineColour;
                this._drawingPrefab.GetComponent<LineRenderer>().startWidth = this._lineWidth;
                this._drawingPrefab.GetComponent<LineRenderer>().endWidth = this._lineWidth;
            }
        }

        private void HandleDrawing(Collider2D hit)
        {
            if (hit.gameObject.tag == "DrawingBoard" || hit.gameObject.tag == "SequencePoint")
            {
                if (this._currentLine == null)
                {
                    this._currentLine = (GameObject)Instantiate(this._drawingPrefab);
                    this._currentLine.transform.parent = this.transform;
                }

                this._currentLineAttributes = this._currentLine.GetComponent<DrawingAttributes>();
                this._currentLineAttributes.AddPoint(this._mousePosCurrent);
                this._mousePosPrevious = this._mousePosCurrent;

                if(hit.gameObject.tag == "SequencePoint")
                {
                    SequencePoint sequencePoint = hit.gameObject.GetComponent<SequencePoint>();

                    if (sequencePoint.complete)
                        return;

                    sequencePoint.complete = true;
                }
            }
        }

        private void EndDrawing()
        {
            this._drawingStarted = false;

            if (this._currentLine == null)
                return;

            if(this._currentLineAttributes.PointCount == 1)
            {
                this._currentLineAttributes.DestoryLine();
            } 
            else
            {
                this._drawCollection.Add(this._currentLineAttributes);
            }

            this._currentLine = null;
            this._currentLineAttributes = null;

            // Checks and Smoothing if needed.
            if (CompleteWritingManager.instance != null)
                CompleteWritingManager.instance.Check();

            if (DecorateManager.instance != null)
                DecorateManager.instance.Check();
        }

    }
}