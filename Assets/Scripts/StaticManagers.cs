using UnityEngine;

public class StaticManagers : MonoBehaviour {
    public static StaticManagers Instance;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}