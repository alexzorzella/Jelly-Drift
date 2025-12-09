using UnityEngine;

public class PlayerInput : MonoBehaviour {
    Car car;

    public void Initialize(Car car) {
        this.car = car;
    }
    
    void Update() {
        if (car == null || (GameController.Instance && !GameController.Instance.playing)) {
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