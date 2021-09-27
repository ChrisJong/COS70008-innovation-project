using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorate : MonoBehaviour
{

    public bool finished = false;

    private float _padding = 0.3f;

    [SerializeField] private GameObject _decorate;
    [SerializeField] private GameObject _board;

    [SerializeField] private List<SequencePoint> _points;

    private void Awake()
    {
        if (this._decorate != null)
            this._decorate.SetActive(false);
    }

    public void Check(DrawPoints drawpoints)
    {
        foreach (SequencePoint point in this._points)
        {
            foreach (Vector3 drawpoint in drawpoints.points)
            {
                if (point.complete == true)
                    continue;

                float distance = point.Distance(new Vector2(drawpoint.x, drawpoint.y));
                Debug.Log("Distance of " + point.gameObject.name + ": " + distance);
                if (distance > this._padding)
                {
                    point.complete = false;
                    continue;
                }
                else
                {
                    point.complete = true;
                    break;
                }
            }
        }

        this.CheckComplete();

    }

    public bool CheckComplete()
    {
        int temp = 0;
        foreach (SequencePoint point in this._points)
        {
            if (point.complete)
                temp++;
        }

        if (temp == this._points.Count)
        {
            this.finished = true;
            this._decorate.SetActive(true);
            this._board.SetActive(false);
            return true;
        }
        else
        {
            this.finished = false;
            this._decorate.SetActive(false);
            this._board.SetActive(true);
            return false;
        }
    }
}
