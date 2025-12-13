using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
        // PlayerInput();
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

    void ScrollUp(InputAction.CallbackContext context) {
        Navigate(new Vector2(0, 1));
    }
    
    void ScrollDown(InputAction.CallbackContext context) {
        Navigate(new Vector2(0, -1));
    }

    void Select(InputAction.CallbackContext context) {
            if (cycles[selected].activeCycle) {
                // cycles[selected].Cycle(horizontalInput);
                SoundManager.Instance.PlayCycle();
            }
            else {
                SoundManager.Instance.PlayError();
            }
    }
    
    void Cancel(InputAction.CallbackContext context) {
        cycles[backBtn].Cycle(1);
        SoundManager.Instance.PlayCycle();
    }
    
    void Navigate(Vector2 input) {
        if (input.y != 0 && !verticalDone) {
            cycleText[selected].color = Color.white;
            if (correspondingText.Length != 0 && !correspondingText[selected].gameObject.CompareTag("Ignore")) {
                correspondingText[selected].color = Color.white;
            }

            selected += (int)input.y;
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
    }
    
    void PlayerInput() {
        if (UnlockManager.Instance && UnlockManager.Instance.gameObject.activeInHierarchy) {
            return;
        }

        int horizontalInput = (int)Input.GetAxisRaw("HorizontalMenu");
        int verticalInput = -(int)Input.GetAxisRaw("VerticalMenu");
        bool submit = Input.GetButtonDown("Submit");
        bool cancel = Input.GetButtonDown("Cancel");
        
        if ((horizontalInput != 0 && !horizontalDone) || submit) {
            if (cycles[selected].activeCycle) {
                cycles[selected].Cycle(horizontalInput);
                SoundManager.Instance.PlayCycle();
            }
            else {
                SoundManager.Instance.PlayError();
            }
        }

        if (verticalInput != 0 && !verticalDone) {
            cycleText[selected].color = Color.white;
            if (correspondingText.Length != 0 && !correspondingText[selected].gameObject.CompareTag("Ignore")) {
                correspondingText[selected].color = Color.white;
            }

            selected += verticalInput;
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

        if (cancel) {
            cycles[backBtn].Cycle(1);
            SoundManager.Instance.PlayCycle();
        }

        horizontalDone = horizontalInput != 0;
        verticalDone = verticalInput != 0;
    }
}