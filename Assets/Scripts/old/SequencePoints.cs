using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePoints : MonoBehaviour {
    public bool complete = false;

    public GameObject sequenceObject;
    
    public List<SequencePoint> points;

    public void Awake() {
        this.points = new List<SequencePoint>(this.sequenceObject.GetComponentsInChildren<SequencePoint>());
    }

    public void CheckPoint(List<Vector3> drawpoints, float padding) {
        foreach(SequencePoint point in this.points) {

            if (point.complete == true)
                continue;

            foreach(Vector3 drawpoint in drawpoints) {
                float distance = point.Distance(new Vector2(drawpoint.x, drawpoint.y));
                //Debug.Log("Distance of " + point.gameObject.name + ": " + distance);
                if (distance > padding) {
                    point.complete = false;
                    continue;
                } else {
                    point.complete = true;
                    break;
                }
            }
        }

        this.CheckComplete();
    }

    public bool CheckComplete() {
        int temp = 0;
        foreach(SequencePoint point in this.points) {
            if (point.complete)
                temp++;
        }

        if (temp == this.points.Count) {
            this.complete = true;
            return true;
        } else {
            this.complete = false;
            return false;
        }
    }
}
