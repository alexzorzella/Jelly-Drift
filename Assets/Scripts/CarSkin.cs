using System;
using UnityEngine;

public class CarSkin : MonoBehaviour {
    public Renderer[] renderers;
    public Material[] materials;
    public SkinArray[] skinsToChange;

    int currentSkin;

    void Start() {
    }

    public void SetSkin(int n) {
        if (skinsToChange.Length == 0) {
            return;
        }

        print("n: " + n);
        var i = 0;
        while (i < skinsToChange[n].myArray.Length) {
            var num = skinsToChange[n].myArray[i++];
            var num2 = skinsToChange[n].myArray[i++];
            var num3 = skinsToChange[n].myArray[i++];
            var array = renderers[num].materials;
            array[num2] = materials[num3];
            renderers[num].materials = array;
        }
    }

    public string GetSkinName(int n) {
        return materials[n].name;
    }

    [Serializable]
    public class SkinArray {
        public int[] myArray;
    }
}