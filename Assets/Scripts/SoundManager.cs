using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;
    public AudioClip cycle;
    public AudioClip menu;
    public AudioClip buy;
    public AudioClip unlock;
    public AudioClip error;
    public AudioSource audio;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayCycle() {
        PlaySound(cycle);
    }

    public void PlayUnlock() {
        PlaySoundDelayed(unlock, 0.1f);
    }

    public void PlayError() {
        PlaySound(error);
    }

    public void PlayMoney() {
        PlaySound(buy);
    }

    public void PlayMenuNavigate() {
        PlaySound(menu);
    }

    public void PlaySound(AudioClip c) {
        audio.clip = c;
        audio.Play();
    }

    public void PlaySoundDelayed(AudioClip c, float d) {
        audio.clip = c;
        audio.PlayDelayed(d);
    }
}