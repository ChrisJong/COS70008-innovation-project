using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Manager;

public class DrawManager : MonoBehaviour {
    public GameObject drawPrefab;
    public GameObject trail;

    public List<DrawPoints> drawCollection;

    private Plane planeObj;
    
    [SerializeField] private Vector3 startPos;

    private void Start() {
        this.drawCollection = new List<DrawPoints>();
        this.planeObj = new Plane(Camera.main.transform.forward * 1000, this.transform.position);
    }

    public void ClearLines() {
        if(drawCollection.Count != 0) {
            foreach(DrawPoints line in drawCollection) {
                line.DestoryLine();
            }
        }

        this.drawCollection.Clear();
    }

    private bool IsOnAButton() {
        if (EventSystem.current.IsPointerOverGameObject()) {

            GameObject go = EventSystem.current.currentSelectedGameObject;
            Button button = null;
            InputField inputField = null;

            if (go != null) {
                button = go.GetComponent<Button>();
                inputField = go.GetComponent<InputField>();
            }

            if (button != null) {
                Debug.Log("Button");
                return true;
            } else if(inputField != null) {
                Debug.Log("InputField");
                return true;
            } else {
                return false;
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
                    Vector3[] TrailRecorded = new Vector3[this.trail.GetComponent<TrailRenderer>().positionCount];
                    int numberOfPositions = this.trail.GetComponent<TrailRenderer>().GetPositions(TrailRecorded);

                    this.drawCollection.Add(this.trail.GetComponent<DrawPoints>() as DrawPoints);
                    this.trail.GetComponent<DrawPoints>().AddPoints(TrailRecorded);
                    this.trail = null;

                    if(DecorateManager.instance != null)
                    {
                        DecorateManager.instance.Check(this.drawCollection);
                    }
                }
            }
        }
    }
}
