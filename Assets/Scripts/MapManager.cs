using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager {
    static MapManager _i;

    public static MapManager i {
        get {
            if (_i == null) {
                _i = new MapManager();
            }
            return _i;
        }
    }

    int selectedMapIndex = 0;

    public void CycleSelectedMap(int by) {
        selectedMapIndex = IncrementWithOverflow.Run(selectedMapIndex, maps.Count, by);
    }
    
    public MapData GetSelectedMap() {
        return maps[selectedMapIndex];
    }
    
    readonly List<MapData> maps = new() {
        new MapData("Dusty Desert", new Color(207, 96, 25, 191)),
        new MapData("Sneaky Snow", new Color(0, 109, 255, 191)),
        new MapData("Pink Plains", new Color(255, 32, 84, 191)),
        new MapData("Akina Downhill", new Color(255, 30, 0, 191)),
        new MapData("Flapjack Raceway", new Color(255, 30, 0, 191))
    };

    public MapData GetMapAtIndex(int index) {
        return maps[index];
    }
    
    public int MapCount() {
        return maps.Count;
    }
    
    public int GetStars(int map, float time) {
        var result = 0;
        // if (time <= maps[map].times[2]) {
        //     result = 3;
        // }
        // else if (time <= maps[map].times[1]) {
        //     result = 2;
        // }
        // else if (time <= maps[map].times[0]) {
        //     result = 1;
        // }
        //
        // if (time <= 0f) {
        //     result = 0;
        // }
        
        return result;
    }

    public class MapData {
        string name;
        Color themeColor;
        Sprite image;
        float[] times;
        
        public MapData(string name, Color themeColor) {
            this.name = name;
            this.themeColor = themeColor;

            string imageName = name.Replace(" ", "_").ToLower();
            image = Resources.Load<Sprite>(imageName);

            if (image == null) {
                Debug.LogError($"No image called {imageName} found.");
            }
        }

        public string GetName() {
            return name;
        }

        public Color GetColor() {
            return themeColor;
        }

        public Sprite GetImage() {
            return image;
        }
    }
}