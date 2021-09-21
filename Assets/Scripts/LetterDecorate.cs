using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterDecorate : MonoBehaviour {

    public GameObject decorate;
    public float padding = 0.3f;

    public SequencePoints sequance;

    //public List<GameObject> sequence01;
    //public List<GameObject> sequence02;

    //public bool sequence01Complete = false;
    //public bool sequence02Complete = false;

    //public Dictionary<int, List<GameObject>> sequence;

    private void Awake() {
        if(this.decorate != null)
            this.decorate.SetActive(false);
        this.sequance = this.gameObject.GetComponent<SequencePoints>();
    }

    public void CheckSequence(DrawPoints drawnline) {
        this.sequance.CheckPoint(drawnline.pointList, this.padding);

        if (this.sequance.complete == true)
        {
            this.decorate.SetActive(true);
        }
    }

    /*public void CheckSequence(DrawPoints drawnline) {
        if (sequence01Complete == false) {
            foreach (GameObject go in this.sequence01) {
                RectTransform rect = go.GetComponent<RectTransform>();

                foreach (Vector3 drawpoint in drawnline.pointList) {
                    float distance = Vector2.Distance(new Vector2(drawpoint.x, drawpoint.y), new Vector2(rect.position.x, rect.position.y));
                    Debug.Log("Rect Point - " + rect.name + ": " + rect.position);
                    Debug.Log("Drwapoint: " + drawpoint);
                    Debug.Log("Distance: " + distance);

                    if (distance > padding) {
                        this.sequence01Complete = false;
                    } else {
                        this.sequence01Complete = true;
                        break;
                    }
                }
            }
        }

        if (sequence02Complete == false) {
            foreach (GameObject go in this.sequence02) {
                RectTransform rect = go.GetComponent<RectTransform>();

                foreach (Vector3 drawpoint in drawnline.pointList) {
                    float distance = Vector2.Distance(new Vector2(drawpoint.x, drawpoint.y), new Vector2(rect.position.x, rect.position.y));
                    Debug.Log("Rect Point - " + rect.name + ": " + rect.position);
                    Debug.Log("Drwapoint: " + drawpoint);
                    Debug.Log("Distance: " + distance);

                    if (distance > padding) {
                        this.sequence02Complete = false;
                    } else {
                        this.sequence02Complete = true;
                        break;
                    }
                }
            }
        }
    }*/
}
