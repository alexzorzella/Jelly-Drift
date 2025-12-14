using System;
using System.Reflection;
using UnityEngine;

public class MainMenuAtmosphere : MonoBehaviour {
    MultiAudioSource ocean;
    MultiAudioSource gulls;
    
    const float gullsMinDelay = 2F;
    const float gullsMaxDelay = 10F;
    
    float currentGullDelay = 0;
    
    void Start() {
        InitializeAudio();
    }

    void InitializeAudio() {
        ocean = MultiAudioSource.FromResource(gameObject, "oceanside", loop: true);
        ocean.SetVolume(0.5F);
        ocean.PlayRoundRobin();

        // gulls = MultiAudioSource.FromResources(gameObject, "gulls", 2);
    }

    void Update() {
        // GullSounds();
    }

    void ResetGullDelay() {
        currentGullDelay = UnityEngine.Random.Range(gullsMinDelay, gullsMaxDelay);
    }

    void GullSounds() {
        if (currentGullDelay <= 0) {
            gulls.PlayRandom();
            ResetGullDelay();
        } else {
            currentGullDelay -= Time.deltaTime;
        }
    }
}