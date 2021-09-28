using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorate : MonoBehaviour
{

    public bool finished = false;

    private float _padding = 0.3f;

    [SerializeField] private GameObject _decorateComplete;
    [SerializeField] private GameObject _decorateOutline;

    [SerializeField] private List<SequencePoint> _points;

    private void Awake()
    {
        if (this._decorateComplete != null)
            this._decorateComplete.SetActive(false);
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
            this._decorateComplete.SetActive(true);
            this._decorateOutline.SetActive(false);
            return true;
        }
        else
        {
            this.finished = false;
            this._decorateComplete.SetActive(false);
            this._decorateOutline.SetActive(true);
            return false;
        }
    }
}
