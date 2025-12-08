using System;
using UnityEngine;
public class CameraController : MonoBehaviour
{
	private void Awake()
	{
		CameraController.Instance = this;
		if (this.target != null)
		{
			this.AssignTarget(this.target);
		}
		this.mainCam = base.GetComponentInChildren<Camera>();
	}
	public void AssignTarget(Transform target)
	{
		MonoBehaviour.print("assinging target");
		this.target = target;
		this.targetRb = target.GetComponent<Rigidbody>();
		this.targetCar = target.GetComponent<Car>();
	}
	private void Update()
	{
		if (!this.target)
		{
			return;
		}
		Vector3 normalized = new Vector3(this.target.forward.x, 0f, this.target.forward.z).normalized;
		Vector3 a = new Vector3(this.targetRb.linearVelocity.x, 0f, this.targetRb.linearVelocity.z).normalized;
		if ((this.targetCar.speed < 5f && this.targetCar.speed > -15f) || SaveState.Instance.cameraMode == 1)
		{
			a = Vector3.zero;
		}
		Vector3 a2 = normalized * 0.2f + a * 0.8f;
		a2.Normalize();
		this.desiredPosition = this.target.position + -a2 * this.distFromTarget + Vector3.up * this.camHeight + this.offset;
		base.transform.position = Vector3.Lerp(base.transform.position, this.desiredPosition, Time.deltaTime * this.moveSpeed);
		float d = this.targetRb.linearVelocity.magnitude * 0.25f;
		Vector3 forward = this.target.position - this.desiredPosition + d * a2 + d * Vector3.down * 0.3f;
		this.desiredLook = Quaternion.LookRotation(forward);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.desiredLook, Time.deltaTime * this.rotationSpeed);
		float b = (float)Mathf.Clamp(70 + (int)(this.targetRb.linearVelocity.magnitude * 0.35f), 70, 85);
		this.fov = Mathf.Lerp(this.fov, b, Time.deltaTime * 5f);
		this.mainCam.fieldOfView = this.fov;
		this.offset = Vector3.Lerp(this.offset, Vector3.zero, Time.deltaTime * this.offsetSpeed);
		if (this.targetCar.acceleration.y > this.shakeThreshold)
		{
			float d2 = (Mathf.Clamp(this.targetCar.acceleration.y, this.shakeThreshold, 50f) - this.shakeThreshold / 2f) * 0.14f;
			this.OffsetCamera(Vector3.down * d2);
		}
	}
	public void OffsetCamera(Vector3 offset)
	{
		if (!this.readyToOffset)
		{
			return;
		}
		this.offset += offset;
		this.readyToOffset = false;
		base.Invoke("GetReady", 0.5f);
		ShakeController.Instance.Shake();
	}
	private void GetReady()
	{
		this.readyToOffset = true;
	}
	public Transform target;
	private Rigidbody targetRb;
	private Car targetCar;
	private Vector3 desiredPosition;
	private Vector3 offset;
	private Quaternion desiredLook;
	public float moveSpeed;
	public float rotationSpeed;
	public float distFromTarget;
	public float camHeight;
	public float offsetSpeed = 1.5f;
	private Camera mainCam;
	public static CameraController Instance;
	private float fov;
	private float shakeThreshold = 16f;
	private bool readyToOffset = true;
}
