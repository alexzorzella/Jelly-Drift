using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {
    public Car car;

    PXN pxn;

    public void Initialize(Car car) {
        this.car = car;
        
        pause = FindFirstObjectByType<Pause>();
        
        pxn = new PXN();
        
        pxn.Car.Throttle.performed += Throttle;
        pxn.Car.Wheel.performed += Steering;
        pxn.Car.Brake.performed += Brake;
        
        pxn.Car.Reverse.performed += Reverse;
        pxn.Car.First.performed += First;
        pxn.Car.Second.performed += Second;
        pxn.Car.Third.performed += Third;
        pxn.Car.Fourth.performed += Fourth;
        pxn.Car.Fifth.performed += Fifth;

        // pxn.Car.Restart.performed += Restart_BucketBrigade;

        pxn.Enable();
    }

    void Select(InputAction.CallbackContext context) {
        Debug.Log("Select");
    }
    
    void Back(InputAction.CallbackContext context) {
        Debug.Log("Back");
    }

    void Menu(InputAction.CallbackContext context) {
        Debug.Log("Menu");
    }

    void Throttle(InputAction.CallbackContext context) {
        car.throttle = -(context.ReadValue<float>() - 1) / 2;
    }

    const float steeringMultiplier = 1.5F;
    const float exponential = 1;
    
    void Steering(InputAction.CallbackContext context) {
        float steering = context.ReadValue<float>();

        if (steering > 0) {
            steering = Mathf.Pow((1 - steering) * steeringMultiplier, exponential);
        }
        else {
            steering = -Mathf.Pow((1 + steering) * steeringMultiplier,exponential);
        }

        steering = Mathf.Clamp(steering, -1F, 1F);
        
        car.steering = steering;
    }

    void Brake(InputAction.CallbackContext context) {
        car.braking = context.ReadValue<float>() > 0.02F;
    }

    void First(InputAction.CallbackContext context) { car.SetGear(0); }
    void Second(InputAction.CallbackContext context) { car.SetGear(1); }
    void Third(InputAction.CallbackContext context) { car.SetGear(2); }
    void Fourth(InputAction.CallbackContext context) { car.SetGear(3); }
    void Fifth(InputAction.CallbackContext context) { car.SetGear(4); }
    void Reverse(InputAction.CallbackContext context) { car.SetGear(5); }

    Pause pause;
    
    // void Restart_BucketBrigade(InputAction.CallbackContext context) {
    //     pause.RestartGame();
    // }

    private void OnEnable() {
        if (pxn != null) {
            pxn.Enable();
        }
    }

    private void OnDisable() {
        pxn.Disable();
    }
}

// using UnityEngine;
//
// public class PlayerInput : MonoBehaviour {
//     Car car;
//
//     public void Initialize(Car car) {
//         this.car = car;
//     }
//     
//     void Update() {
//         if (car == null || (GameController.Instance && !GameController.Instance.playing)) {
//             return;
//         }
//
//         GetPlayerInput();
//     }
//
//     void GetPlayerInput() {
//         car.steering = Input.GetAxisRaw("Horizontal");
//         car.throttle = Input.GetAxis("Vertical");
//         car.braking = Input.GetButton("Breaking");
//     }
// }