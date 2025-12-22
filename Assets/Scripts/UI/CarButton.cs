using TMPro;
using UnityEngine;

public class CarButton : MonoBehaviour {
    public enum ButtonState {
        Next,
        BuySkin,
        BuyCar
    }

    public TextMeshProUGUI text;
    public CarCycle carCycle;
    public SkinCycle skinCycle;

    ButtonState state;

    void Awake() {
        SetState(ButtonState.Next);
    }

    public void SetState(ButtonState state) {
        this.state = state;
        if (state == ButtonState.Next) {
            text.text = "Next";
            return;
        }

        text.text = "Buy";
    }

    public void Use() {
        if (state == ButtonState.Next) {
            carCycle.SaveCar();
            return;
        }

        if (state == ButtonState.BuySkin) {
            skinCycle.BuySkin();
            return;
        }

        if (state == ButtonState.BuyCar) {
            carCycle.BuyCar();
        }
    }
}