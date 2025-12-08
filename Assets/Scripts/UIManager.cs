using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;
    public Transform splitPos;

    void Awake() {
        Instance = this;
    }
}