using UnityEngine;

public class SaveState : MonoBehaviour {
    public static SaveState Instance;
    public int quality { get; set; }
    public int dof { get; set; }
    public int motionBlur { get; set; }
    public int cameraMode { get; set; }
    public int cameraShake { get; set; }
    public int muted { get; set; }
    public int volume { get; set; }
    public int musicVolume { get; set; }
    public int graphics { get; set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start() {
        if (!Instance) {
            return;
        }

        LoadSettings();
    }

    void LoadSettings() {
        graphics = SaveManager.Instance.state.graphics;
        quality = SaveManager.Instance.state.quality;
        motionBlur = SaveManager.Instance.state.motionBlur;
        dof = SaveManager.Instance.state.dof;
        cameraMode = SaveManager.Instance.state.cameraMode;
        cameraShake = SaveManager.Instance.state.cameraShake;
        muted = SaveManager.Instance.state.muted;
        volume = SaveManager.Instance.state.volume;
        musicVolume = SaveManager.Instance.state.music;
    }
}