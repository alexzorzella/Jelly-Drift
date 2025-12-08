using UnityEngine;

public class PrefabManager : MonoBehaviour {
    public static PrefabManager Instance;
    public GameObject[] cars;
    public GameObject splitUi;
    public GameObject crashParticles;
    public Material ghostMat;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}