using System;
using UnityEngine;
public class BallCar : MonoBehaviour
{
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}
private void Update()
	{
		this.PlayerInput();
	}
	private void FixedUpdate()
	{
		Vector3 vector = base.transform.InverseTransformDirection(this.rb.linearVelocity);
		Vector3 vector2 = base.transform.InverseTransformDirection((this.rb.linearVelocity - this.lastVelocity) / Time.fixedDeltaTime);
		this.rb.AddTorque(base.transform.up * this.steering * this.steeringPower);
		this.rb.AddForce(this.throttle * this.orientation.forward * this.speed);
		Vector3 a = Vector3.Project(this.rb.linearVelocity, this.orientation.right);
		float d = 1.5f;
		this.rb.AddForce(-a * this.rb.mass * d);
		this.lastVelocity = this.rb.linearVelocity;
		float num = vector2.z * 0.25f;
		float z = vector2.x * 0.5f;
		this.car.transform.localRotation = Quaternion.Euler(-num, 0f, z);
		Vector3 force = -this.C_drag * vector.z * Mathf.Abs(vector.z) * this.rb.linearVelocity.normalized;
		this.rb.AddForce(force);
		Vector3 force2 = -this.C_rollFriction * vector.z * this.rb.linearVelocity.normalized;
		this.rb.AddForce(force2);
	}
	private void PlayerInput()
	{
		this.steering = Input.GetAxisRaw("Horizontal");
		this.throttle = Input.GetAxis("Vertical");
		this.breaking = Input.GetButton("Breaking");
	}
	private Rigidbody rb;
	public Transform orientation;
	public Transform car;
	private float steering;
	private float throttle;
	private bool breaking;
	private float C_drag = 3.5f;
	private float C_rollFriction = 91f;
	private float C_breaking = 3000f;
	private float speed = 18000f;
	private float steeringPower = 6000f;
	private Vector3 lastVelocity;
}
