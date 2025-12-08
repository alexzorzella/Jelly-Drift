using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Car car;

    void Update() {
        if (GameController.Instance && !GameController.Instance.playing) {
            return;
        }

        GetPlayerInput();
    }

    void GetPlayerInput() {
        car.steering = Input.GetAxisRaw("Horizontal");
        car.throttle = Input.GetAxis("Vertical");
        car.breaking = Input.GetButton("Breaking");
    }
}