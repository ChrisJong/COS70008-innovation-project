using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DrawingAttributes : MonoBehaviour
{
    [SerializeField] private int _pointcount;
    public int PointCount { get { return this._pointcount; } }

    [SerializeField] Vector3 _startPoint;
    [SerializeField] Vector3 _endPoint;
    
    [SerializeField] List<Vector3> _points;

    [SerializeField] LineRenderer _lineRenderer;

    public bool AddPoints(Vector3[] points)
    {
        if (points.Length == 0 || points == null)
            return false;
        else
        {
            this._points = new List<Vector3>(points);
            this._startPoint = points[0];
            this._endPoint = points[points.Length-1];
        }

        return true;
    }

    public bool AddPoints(List<Vector3> points)
    {
        if (points.Count == 0 || points == null)
            return false;
        else
        {
            this._points = new List<Vector3>(points);
            this._startPoint = points[0];
            this._endPoint = points[points.Count];
        }

        return true;
    }

    public bool AddPoint(Vector3 point)
    {
        if(this._points.Count == 0 || this._points == null)
        {
            this._points = new List<Vector3>();
            this._points.Add(point);
        } 
        else
        {
            this._points.Add(point);
        }

        this._pointcount++;

        this._lineRenderer.positionCount = this._pointcount;
        this._lineRenderer.SetPosition(this._pointcount - 1, point);

        return true;
    }

    public bool DestoryLine()
    {
        Destroy(this.gameObject);
        return true;
    }
}
