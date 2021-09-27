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
    public List<DrawPoints> drawCollection;
    public LetterDecorate letter;

    private void Start() {
        this.drawCollection = new List<DrawPoints>();
        this.planeObj = new Plane(Camera.main.transform.forward * 10, this.transform.position);
        this.DrawPlane(this.transform.position, Camera.main.transform.forward * 10);
    }

    public void ClearLines() {
        if(drawCollection.Count != 0) {
            foreach(DrawPoints line in drawCollection) {
                line.DestoryLine();
            }
        }

        this.drawCollection.Clear();
    }

    private void DrawPlane(Vector3 position, Vector3 normal) {
        Vector3 v3;

        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude;

        var corner0 = position + v3;
        var corner2 = position - v3;
        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;
        Debug.Log(corner0);
        Debug.Log(corner2);
        Debug.Log(corner1);
        Debug.Log(corner3);
        Debug.DrawLine(corner0, corner2, Color.green, 2000.0f, false);
        Debug.DrawLine(corner1, corner3, Color.green, 2000.0f, false);
        Debug.DrawLine(corner0, corner1, Color.green, 2000.0f, false);
        Debug.DrawLine(corner1, corner2, Color.green, 2000.0f, false);
        Debug.DrawLine(corner2, corner3, Color.green, 2000.0f, false);
        Debug.DrawLine(corner3, corner0, Color.green, 2000.0f, false);
        Debug.DrawRay(position, normal, Color.red, 2000.0f, false);

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

                    //Debug.Log(numberOfPositions);
                    this.drawCollection.Add(this.trail.GetComponent<DrawPoints>() as DrawPoints);
                    this.trail.GetComponent<DrawPoints>().AddPoints(TrailRecorded, letter);
                    this.trail = null;
                }
            }

        }
    }
}
