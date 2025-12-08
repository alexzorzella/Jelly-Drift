using UnityEngine;

public class MoveImage : MonoBehaviour {
    readonly float speed = 3f;

    void Update() {
        transform.localPosition += new Vector3(speed, 0f, 0f) * Time.deltaTime;
    }
}