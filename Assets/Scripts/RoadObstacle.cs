using UnityEngine;

public class RoadObstacle : MonoBehaviour {
    public GameObject particles;
    bool ready = true;

    void OnTriggerEnter(Collider other) {
        if (!ready) {
            return;
        }

        Instantiate(particles, transform.position, particles.transform.rotation);
        Destroy(gameObject);
        ready = false;
    }
}