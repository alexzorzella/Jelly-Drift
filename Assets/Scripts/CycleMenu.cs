using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CycleMenu : MonoBehaviour {
    public TextMeshProUGUI[] correspondingText;
    public int startSelect;
    public int backBtn;

    List<ItemCycle> cycles;
    List<TextMeshProUGUI> cycleText;
    bool horizontalDone;
    int selected;
    bool verticalDone;

    void Awake() {
        selected = startSelect;
        cycles = new List<ItemCycle>();
        cycleText = new List<TextMeshProUGUI>();
        for (var i = 0; i < transform.childCount; i++) {
            var component = transform.GetChild(i).GetComponent<ItemCycle>();
            if (component) {
                cycles.Add(component);
                var componentInChildren = component.GetComponentInChildren<TextMeshProUGUI>();
                cycleText.Add(componentInChildren);
                componentInChildren.color = Color.white;
            }
        }

        cycleText[selected].color = Color.black;
    }

    void Start() {
        SaveManager.Instance.state.skins[5][1] = true;
    }

    void Update() {
        PlayerInput();
    }

    void OnEnable() {
        selected = startSelect;
        horizontalDone = true;
        verticalDone = true;
        UpdateSelected();
    }

    void UpdateSelected() {
        foreach (var textMeshProUGUI in cycleText) {
            textMeshProUGUI.color = Color.white;
            if (correspondingText.Length != 0 && !correspondingText[selected].gameObject.CompareTag("Ignore")) {
                correspondingText[selected].color = Color.white;
            }
        }

        cycleText[selected].color = Color.black;
        if (correspondingText.Length != 0 && !correspondingText[selected].gameObject.CompareTag("Ignore")) {
            correspondingText[selected].color = Color.black;
        }
    }

    void PlayerInput() {
        if (UnlockManager.Instance && UnlockManager.Instance.gameObject.activeInHierarchy) {
            return;
        }

        var num = (int)Input.GetAxisRaw("HorizontalMenu");
        var num2 = -(int)Input.GetAxisRaw("VerticalMenu");
        var buttonDown = Input.GetButtonDown("Submit");
        var buttonDown2 = Input.GetButtonDown("Cancel");
        
        if ((num != 0 && !horizontalDone) || buttonDown) {
            if (cycles[selected].activeCycle) {
                cycles[selected].Cycle(num);
                SoundManager.Instance.PlayCycle();
            }
            else {
                SoundManager.Instance.PlayError();
            }
        }

        if (num2 != 0 && !verticalDone) {
            cycleText[selected].color = Color.white;
            if (correspondingText.Length != 0 && !correspondingText[selected].gameObject.CompareTag("Ignore")) {
                correspondingText[selected].color = Color.white;
            }

            selected += num2;
            if (selected >= cycles.Count) {
                selected = 0;
            }
            else if (selected < 0) {
                selected = cycles.Count - 1;
            }

            cycleText[selected].color = Color.black;
            if (correspondingText.Length != 0 && !correspondingText[selected].gameObject.CompareTag("Ignore")) {
                correspondingText[selected].color = Color.black;
            }

            SoundManager.Instance.PlayMenuNavigate();
        }

        if (buttonDown2) {
            cycles[backBtn].Cycle(1);
            SoundManager.Instance.PlayCycle();
        }

        horizontalDone = num != 0;
        verticalDone = num2 != 0;
    }
}