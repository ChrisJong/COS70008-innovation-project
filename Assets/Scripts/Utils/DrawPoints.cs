using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPoints : MonoBehaviour {

    public Vector3 startPoint = Vector3.zero;
    public Vector3 endPoint = Vector3.zero;

    public List<Vector3> points;

    public bool AddPoints(Vector3[] points) {
        if (points.Length == 0 || points == null)
            return false;
        else {
            this.points = new List<Vector3>(points);

            this.startPoint = points[0];
            this.endPoint = points[points.Length-1];
        }

        return true;
    }

    public bool DestoryLine() {
        Destroy(this.gameObject);
        return true;
    }
}
