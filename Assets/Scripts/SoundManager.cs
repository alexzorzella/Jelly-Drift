using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    static readonly Dictionary<string, MultiAudioSource> sources = new();
    
    static SoundManager _i;
	
    public static SoundManager i {
        get {
            if (_i == null) {
                SoundManager x = Resources.Load<SoundManager>("SoundManager");

                _i = Instantiate(x);
            }
            return _i;
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        AddSource("cycle", "menu", "buy", "unlock", "error");
    }

    public void PlayCycle() {
        PlaySound("cycle");
    }

    public void PlayUnlock() {
        PlaySound("unlock"); // Used to have a delay of 0.1f
    }

    public void PlayError() {
        PlaySound("error");
    }

    public void PlayMoney() {
        PlaySound("buy");
    }

    public void PlayMenuNavigate() {
        PlaySound("menu");
    }
    
    public void PlaySound(string name) {
        if (!sources.ContainsKey(name)) {
            AddSource(name);
        }
        
        sources[name].PlayRoundRobin();
    }

    void AddSource(params string[] soundNames) {
        foreach (string soundName in soundNames) {
            sources.Add(soundName, MultiAudioSource.FromResource(gameObject, soundName));
        }
    }
}