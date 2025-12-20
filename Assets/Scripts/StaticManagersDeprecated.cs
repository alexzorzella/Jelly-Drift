using UnityEngine;

public class StaticManagersDeprecated : MonoBehaviour {
    public static StaticManagersDeprecated Instance;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}