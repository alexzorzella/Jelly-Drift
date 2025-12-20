using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public GameObject clearFx;

    bool done;
    public int nr { get; set; }

    void Awake() {
        nr = transform.GetSiblingIndex();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car")) {
            var component = other.gameObject.transform.root.GetComponent<CheckpointUser>();
            if (component) {
                component.CheckPoint(this);
            }
        }
    }
}