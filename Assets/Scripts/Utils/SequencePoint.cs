using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequencePoint : MonoBehaviour {

    public bool complete = false;
    
    public Vector2 position = Vector2.zero;

    [SerializeField] private Collider2D _collider;

    private void Awake(){
        this.position = this.transform.position;
        this._collider = this.gameObject.GetComponent<Collider2D>();
    }

    public float Distance(Vector2 drawpointPosition) {
        return Vector2.Distance(this.position, drawpointPosition);
    }
}
