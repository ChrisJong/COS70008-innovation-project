using UnityEngine;

public class FollowCamera : MonoBehaviour {

    Vector3 mousePos = Vector3.zero;

    private void Update() {
        mousePos = Input.mousePosition;
        mousePos.z = 9.0f;
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

}