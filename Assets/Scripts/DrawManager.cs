using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour {
    public GameObject drawPrefab;
    public GameObject trail;
    private Plane planeObj;
    private Vector3 startPos;
    public Vector3[] TrailRecorded;
    public List<DrawPoints> drawCollection;
    public LetterDecorate letter;

    private void Start() {
        this.drawCollection = new List<DrawPoints>();
        this.planeObj = new Plane(Camera.main.transform.forward * 1000, this.transform.position);
    }

    private bool IsOnAButton() {
        if (EventSystem.current.IsPointerOverGameObject())
        {

            GameObject go = EventSystem.current.currentSelectedGameObject;
            Button button = go.GetComponent<Button>();

            if (button != null)
            {
                Debug.Log("Button");
                return true;
            }
        }

        return false;
    }

    private void Update() {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            if(!this.IsOnAButton()) { 
            /*if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {

                //Debug.Log(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

                if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == "Undo" && UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null) {
                    Debug.Log("Button");
                } else if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null) {
                    Debug.Log("Something else");
                    this.trail = (GameObject)Instantiate(this.drawPrefab, this.transform.position, Quaternion.identity);
                }
            } else {
                this.trail = (GameObject)Instantiate(this.drawPrefab, this.transform.position, Quaternion.identity);
            }*/

            this.trail = (GameObject)Instantiate(this.drawPrefab, this.transform.position, Quaternion.identity);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;

            if(this.planeObj.Raycast(mouseRay, out dist)) {
                this.startPos = mouseRay.GetPoint(dist);
            }
            }
        } else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0)) {

            if (this.trail != null)
            {

                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float dist;

                if (this.planeObj.Raycast(mouseRay, out dist))
                {
                    this.trail.transform.position = mouseRay.GetPoint(dist);
                }
            }
        } else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) {
            if (this.trail != null)
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float dist;

                if (this.planeObj.Raycast(mouseRay, out dist))
                {
                    this.trail.transform.position = mouseRay.GetPoint(dist);
                    this.trail.GetComponent<TrailRenderer>().emitting = false;
                    this.TrailRecorded = new Vector3[this.trail.GetComponent<TrailRenderer>().positionCount];
                    int numberOfPositions = this.trail.GetComponent<TrailRenderer>().GetPositions(this.TrailRecorded);

                    //Debug.Log(numberOfPositions);
                    this.drawCollection.Add(this.trail.GetComponent<DrawPoints>() as DrawPoints);
                    this.trail.GetComponent<DrawPoints>().AddPoints(this.TrailRecorded, letter);
                    this.trail = null;
                }
            }
        }
    }

}
