using TMPro;
using UnityEngine;

public class Car : MonoBehaviour {
    [Header("Misc")] public Transform centerOfMass;
    
    public Suspension[] wheelPositions;
    public GameObject wheel;
    public TextMeshProUGUI text;

    bool autoValues;

    CarData carData;
    
    public CarData GetCarData() {
        return carData;
    }
    
    Collider c;
    Vector3 CG;
    float cgHeight;
    float cgToFrontAxle;
    float cgToRearAxle;
    float dir;
    bool grounded;
    Vector3 lastVelocity;

    float wheelBase;
    float wheelRadius;
    float yawRate;
    
    public Rigidbody rb { get; set; }
    public float steering { get; set; }
    public float throttle { get; set; }
    public bool breaking { get; set; }
    public float speed { get; private set; }
    public float steerAngle { get; set; }
    public Vector3 acceleration { get; private set; }

    void Awake() {
        rb = GetComponent<Rigidbody>();

        rb.mass = carData.GetMass();
        rb.linearDamping = carData.GetLinearDamping();
        rb.angularDamping = carData.GetAngularDamping();
        
        // if (autoValues) {
        //     suspensionLength = 0.3f;
        //     suspensionForce = 10f * rb.mass;
        //     suspensionDamping = 4f * rb.mass;
        // }

        var componentsInChildren = gameObject.GetComponentsInChildren<AntiRoll>();
        for (var i = 0; i < componentsInChildren.Length; i++) {
            componentsInChildren[i].antiRoll = carData.GetAntiRoll();
        }

        if (centerOfMass) {
            rb.centerOfMass = centerOfMass.localPosition;
        }

        c = GetComponentInChildren<Collider>();
        wheelBase = Vector3.Distance(wheelPositions[0].transform.position, wheelPositions[2].transform.position);
        CG = c.bounds.center;
        cgHeight = c.bounds.extents.y + carData.GetSuspensionLength();
        cgToFrontAxle =
            Vector3.Distance(
                wheelPositions[0].transform.position +
                (wheelPositions[1].transform.position - wheelPositions[0].transform.position) * 0.5f, CG);
        cgToRearAxle =
            Vector3.Distance(
                wheelPositions[2].transform.position +
                (wheelPositions[3].transform.position - wheelPositions[2].transform.position) * 0.5f, CG);
        wheelRadius = carData.GetSuspensionLength() / 2f;
        InitWheels();
    }

    void Update() {
        MoveWheels();
        Audio();
        CheckGrounded();
        Steering();
    }

    void FixedUpdate() {
        Movement();
    }

    void Audio() {
        // accelerate.volume = Mathf.Lerp(accelerate.volume, Mathf.Abs(throttle) + Mathf.Abs(speed / 80f),
        //     Time.deltaTime * 6f);
        // deaccelerate.volume = Mathf.Lerp(deaccelerate.volume, speed / 40f - throttle * 0.5f, Time.deltaTime * 4f);
        // accelerate.pitch = Mathf.Lerp(accelerate.pitch, 0.65f + Mathf.Clamp(Mathf.Abs(speed / 160f), 0f, 1f),
        //     Time.deltaTime * 5f);
        // if (!grounded) {
        //     accelerate.pitch = Mathf.Lerp(accelerate.pitch, 1.5f, Time.deltaTime * 8f);
        // }
        //
        // deaccelerate.pitch = Mathf.Lerp(deaccelerate.pitch, 0.5f + speed / 40f, Time.deltaTime * 2f);
    }

    void Movement() {
        var vector = XZVector(rb.linearVelocity);
        var vector2 = transform.InverseTransformDirection(XZVector(rb.linearVelocity));
        acceleration = (lastVelocity - vector2) / Time.fixedDeltaTime;
        dir = Mathf.Sign(transform.InverseTransformDirection(vector).z);
        speed = vector.magnitude * 3.6f * dir;
        var num = Mathf.Abs(rb.angularVelocity.y);
        foreach (var suspension in wheelPositions) {
            if (suspension.grounded) {
                var vector3 = XZVector(rb.GetPointVelocity(suspension.hitPos));
                transform.InverseTransformDirection(vector3);
                var a = Vector3.Project(vector3, suspension.transform.right);
                var d = 1f;
                var num2 = 1f;
                if (suspension.terrain) {
                    num2 = 0.6f;
                    d = 0.75f;
                }

                var f = Mathf.Atan2(vector2.x, vector2.z);
                if (breaking) {
                    num2 -= 0.6f;
                }

                var num3 = carData.GetDriftThreshold();
                if (num > 1f) {
                    num3 -= 0.2f;
                }

                var flag = false;
                if (Mathf.Abs(f) > num3) {
                    var num4 = Mathf.Clamp(Mathf.Abs(f) * 2.4f - num3, 0f, 1f);
                    num2 = Mathf.Clamp(1f - num4, 0.05f, 1f);
                    var magnitude = rb.linearVelocity.magnitude;
                    flag = true;
                    if (magnitude < 8f) {
                        num2 += (8f - magnitude) / 8f;
                    }

                    if (num < CarData.yawGripThreshold) {
                        var num5 = (CarData.yawGripThreshold - num) / CarData.yawGripThreshold;
                        num2 += num5 * CarData.yawGripMultiplier;
                    }

                    if (Mathf.Abs(throttle) < 0.3f) {
                        num2 += 0.1f;
                    }

                    num2 = Mathf.Clamp(num2, 0f, 1f);
                }

                var d2 = 1f;
                if (flag) {
                    d2 = carData.GetDriftMultiplier();
                }

                if (breaking) {
                    rb.AddForceAtPosition(suspension.transform.forward * CarData.brakeForce * Mathf.Sign(-speed) * d,
                        suspension.hitPos);
                }

                rb.AddForceAtPosition(suspension.transform.forward * throttle * carData.GetEngineForce() * d2 * d,
                    suspension.hitPos);
                var a2 = a * rb.mass * d * num2;
                rb.AddForceAtPosition(-a2, suspension.hitPos);
                rb.AddForceAtPosition(suspension.transform.forward * a2.magnitude * 0.25f, suspension.hitPos);
                var num6 = Mathf.Clamp(1f - num2, 0f, 1f);
                if (Mathf.Sign(dir) != Mathf.Sign(throttle) && speed > 2f) {
                    num6 = Mathf.Clamp(num6 + 0.5f, 0f, 1f);
                }

                suspension.traction = num6;
                var force = -CarData.dragForce * vector;
                rb.AddForce(force);
                var force2 = -CarData.rollFriction * vector;
                rb.AddForce(force2);
            }
        }

        StandStill();
        lastVelocity = vector2;
    }

    void StandStill() {
        if (Mathf.Abs(speed) >= 1f || !grounded || throttle != 0f) {
            rb.linearDamping = 0f;
            return;
        }

        var flag = true;
        var array = wheelPositions;
        for (var i = 0; i < array.Length; i++) {
            if (Vector3.Angle(array[i].hitNormal, Vector3.up) > 1f) {
                flag = false;
                break;
            }
        }

        if (flag) {
            rb.linearDamping = (1f - Mathf.Abs(speed)) * 30f;
            return;
        }

        rb.linearDamping = 0f;
    }

    void Steering() {
        foreach (var suspension in wheelPositions) {
            if (!suspension.rearWheel) {
                suspension.steeringAngle = steering * (37f - Mathf.Clamp(speed * 0.35f - 2f, 0f, 17f));
                steerAngle = suspension.steeringAngle;
            }
        }
    }

    Vector3 XZVector(Vector3 v) {
        return new Vector3(v.x, 0f, v.z);
    }

    void InitWheels() {
        foreach (var suspension in wheelPositions) {
            suspension.wheelObject = Instantiate(wheel).transform;
            suspension.wheelObject.parent = suspension.transform;
            suspension.wheelObject.transform.localPosition = Vector3.zero;
            suspension.wheelObject.transform.localRotation = Quaternion.identity;
            suspension.wheelObject.localScale = Vector3.one * carData.GetSuspensionLength() * 2f;
        }
    }

    void MoveWheels() {
        foreach (var suspension in wheelPositions) {
            var num = carData.GetSuspensionLength();
            var hitHeight = suspension.hitHeight;
            var y = Mathf.Lerp(suspension.wheelObject.transform.localPosition.y, -hitHeight + num,
                Time.deltaTime * 20f);
            var num2 = 0.2f * carData.GetSuspensionLength() * 2f;
            if (suspension.transform.localPosition.x < 0f) {
                num2 = -num2;
            }

            num2 = 0f;
            suspension.wheelObject.transform.localPosition = new Vector3(num2, y, 0f);
            suspension.wheelObject.Rotate(Vector3.right, XZVector(rb.linearVelocity).magnitude * 1f * dir);
            suspension.wheelObject.localScale = Vector3.one * (carData.GetSuspensionLength() * 2f);
            suspension.transform.localScale = Vector3.one / transform.localScale.x;
        }
    }

    void CheckGrounded() {
        grounded = false;
        var array = wheelPositions;
        for (var i = 0; i < array.Length; i++) {
            if (array[i].grounded) {
                grounded = true;
            }
        }
    }
}