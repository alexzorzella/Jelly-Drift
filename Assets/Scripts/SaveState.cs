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
        graphics = SaveManager.i.state.graphics;
        quality = SaveManager.i.state.quality;
        motionBlur = SaveManager.i.state.motionBlur;
        dof = SaveManager.i.state.dof;
        cameraMode = SaveManager.i.state.cameraMode;
        cameraShake = SaveManager.i.state.cameraShake;
        muted = SaveManager.i.state.muted;
        volume = SaveManager.i.state.volume;
        musicVolume = SaveManager.i.state.music;
    }
}