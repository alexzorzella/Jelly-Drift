using System;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public static MapManager Instance;
    public MapInformation[] maps;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public int GetStars(int map, float time) {
        var result = 0;
        if (time <= maps[map].times[2]) {
            result = 3;
        }
        else if (time <= maps[map].times[1]) {
            result = 2;
        }
        else if (time <= maps[map].times[0]) {
            result = 1;
        }

        if (time <= 0f) {
            result = 0;
        }

        return result;
    }

    [Serializable]
    public class MapInformation {
        public int index;
        public string name;
        public Sprite image;
        public Color themeColor;
        public float[] times;
    }
}