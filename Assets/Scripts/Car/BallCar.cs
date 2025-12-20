using UnityEngine;

public class BallCar : MonoBehaviour {
    public Transform orientation;
    public Transform car;
    readonly float C_drag = 3.5f;
    readonly float C_rollFriction = 91f;
    readonly float speed = 18000f;
    readonly float steeringPower = 6000f;
    bool breaking;
    float C_breaking = 3000f;
    Vector3 lastVelocity;

    Rigidbody rb;
    float steering;
    float throttle;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        PlayerInput();
    }

    void FixedUpdate() {
        var vector = transform.InverseTransformDirection(rb.linearVelocity);
        var vector2 = transform.InverseTransformDirection((rb.linearVelocity - lastVelocity) / Time.fixedDeltaTime);
        rb.AddTorque(transform.up * steering * steeringPower);
        rb.AddForce(throttle * orientation.forward * speed);
        var a = Vector3.Project(rb.linearVelocity, orientation.right);
        var d = 1.5f;
        rb.AddForce(-a * rb.mass * d);
        lastVelocity = rb.linearVelocity;
        var num = vector2.z * 0.25f;
        var z = vector2.x * 0.5f;
        car.transform.localRotation = Quaternion.Euler(-num, 0f, z);
        var force = -C_drag * vector.z * Mathf.Abs(vector.z) * rb.linearVelocity.normalized;
        rb.AddForce(force);
        var force2 = -C_rollFriction * vector.z * rb.linearVelocity.normalized;
        rb.AddForce(force2);
    }

    void PlayerInput() {
        steering = Input.GetAxisRaw("Horizontal");
        throttle = Input.GetAxis("Vertical");
        breaking = Input.GetButton("Breaking");
    }
}