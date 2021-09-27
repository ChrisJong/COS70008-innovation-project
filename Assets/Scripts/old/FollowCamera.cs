
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private void Update() {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

}
