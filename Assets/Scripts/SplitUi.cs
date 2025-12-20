using TMPro;
using UnityEngine;

public class SplitUi : MonoBehaviour {
    readonly float speed = 1f;
    Vector3 desiredScale;

    TextMeshProUGUI text;

    void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        Invoke("DestroySelf", 3f);
        Invoke("StartFade", 1.5f);
        desiredScale = Vector3.one * 1f;
        transform.localScale = Vector3.zero;
    }

    void Update() {
        desiredScale = Vector3.Lerp(desiredScale, Vector3.zero, Time.deltaTime * speed * 0.1f);
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * speed * 7.5f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.up, Time.deltaTime * speed);
    }

    void StartFade() {
        text.CrossFadeAlpha(0f, 1.5f, true);
    }

    public void SetSplit(string t) {
        text.text = t;
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}