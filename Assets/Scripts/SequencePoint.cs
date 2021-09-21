using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePoint : MonoBehaviour {
    public bool complete = false;

    public RectTransform rectTransform = null;

    public Vector2 position = Vector2.zero;

    public SequencePoints parent = null;

    public void Awake(){
        this.rectTransform = GetComponent<RectTransform>();
        this.position = this.rectTransform.position;
    }

    public float Distance(Vector2 drawpointPosition) {
        return Vector2.Distance(this.position, drawpointPosition);
    }
}
