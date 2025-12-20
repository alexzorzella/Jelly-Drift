using UnityEngine;

public class StartLight : MonoBehaviour {
    public Material[] colors;
    public AudioSource audio;
    int c;

    MeshRenderer rend;

    void Start() {
        rend = GetComponent<MeshRenderer>();
        colors = rend.materials;
        SetColor(-1);
        Invoke("NextColor", GameController.Instance.startTime / 3f);
    }

    void NextColor() {
        SetColor(c);
        if (audio) {
            audio.pitch = 1f + c * 0.5f / 3f;
            audio.Play();
        }

        c++;
        if (c < 3) {
            Invoke("NextColor", GameController.Instance.startTime / 3f);
        }
    }

    void SetColor(int c) {
        var array = new Material[colors.Length];
        for (var i = 0; i < array.Length; i++) {
            array[i] = colors[i];
        }

        for (var j = 0; j < array.Length; j++) {
            if (j == c + 1) {
                array[j] = colors[j];
            }
            else {
                array[j] = colors[0];
            }
        }

        rend.materials = array;
    }
}