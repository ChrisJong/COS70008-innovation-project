using UnityEngine;

public class DrawManager : MonoBehaviour {
    public GameObject drawPrefab;
    public GameObject trail;
    private Plane planeObj;
    private Vector3 startPos;
    const int MAX_POSITIONS = 100;
    public Vector3[] TrailRecorded = new Vector3[MAX_POSITIONS];

    private void Start() {
        this.planeObj = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    private void Update() {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            
            this.trail = (GameObject)Instantiate(this.drawPrefab, this.transform.position, Quaternion.identity);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;

            if(this.planeObj.Raycast(mouseRay, out dist)) {
                this.startPos = mouseRay.GetPoint(dist);
            }
            this.trail.layer = 6;

        } else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0)) {
            
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;

            if (this.planeObj.Raycast(mouseRay, out dist)) {
                this.trail.transform.position = mouseRay.GetPoint(dist);
            }
        } else if(Input.GetMouseButtonUp(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;

            if (this.planeObj.Raycast(mouseRay, out dist))
            {
                this.trail.transform.position = mouseRay.GetPoint(dist);
                this.trail.GetComponent<TrailRenderer>().emitting = false;
                int numberOfPositions = this.trail.GetComponent<TrailRenderer>().GetPositions(TrailRecorded);
                Debug.Log(numberOfPositions);
            }

        }
    }

}
