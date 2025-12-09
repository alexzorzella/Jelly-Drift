using UnityEngine;

public class MusicController : MonoBehaviour {
    public static MusicController Instance;

    AudioSource music;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        music = GetComponent<AudioSource>();
    }

    void Start() {
        if (!Instance) {
            return;
        }

        UpdateMusic(SaveState.Instance.music);
        AudioListener.volume = SaveState.Instance.volume / 10f;
    }

    void Update() {
    }

    public void UpdateMusic(float f) {
        music.volume = f / 10f;
    }
}