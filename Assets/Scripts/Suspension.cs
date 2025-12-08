using System;
using UnityEngine;
public class Suspension : MonoBehaviour
{
	private void Start()
	{
		this.car = base.transform.parent.GetComponent<Car>();
		this.bodyRb = this.car.GetComponent<Rigidbody>();
		this.raycastOffset = this.car.suspensionLength * 0.5f;
		this.smokeEmitting = this.smokeFx.emission;
		this.spinEmitting = this.spinFx.emission;
	}
	private void FixedUpdate()
	{
		this.NewSuspension();
	}
	private void Update()
	{
		this.DebugTraction();
		if (this.rearWheel)
		{
			return;
		}
		this.wheelAngleVelocity = Mathf.Lerp(this.wheelAngleVelocity, this.steeringAngle, this.steerTime * Time.deltaTime);
		base.transform.localRotation = Quaternion.Euler(Vector3.up * this.wheelAngleVelocity);
	}
	private void DebugTraction()
	{
	}
	public bool terrain { get; set; }
	private void NewSuspension()
	{
		this.minLength = this.restLength - this.springTravel;
		this.maxLength = this.restLength + this.springTravel;
		float suspensionLength = this.car.suspensionLength;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, -base.transform.up, out raycastHit, this.maxLength + suspensionLength))
		{
			this.lastLength = this.springLength;
			this.springLength = raycastHit.distance - suspensionLength;
			this.springLength = Mathf.Clamp(this.springLength, this.minLength, this.maxLength);
			this.springVelocity = (this.lastLength - this.springLength) / Time.fixedDeltaTime;
			this.springForce = this.springStiffness * (this.restLength - this.springLength);
			this.damperForce = this.damperStiffness * this.springVelocity;
			Vector3 force = (this.springForce + this.damperForce) * base.transform.up;
			this.bodyRb.AddForceAtPosition(force, raycastHit.point);
			this.terrain = raycastHit.collider.gameObject.CompareTag("Terrain");
			this.hitPos = raycastHit.point;
			this.hitNormal = raycastHit.normal;
			this.hitHeight = raycastHit.distance;
			this.grounded = true;
			return;
		}
		this.grounded = false;
		this.hitHeight = this.car.suspensionLength + this.car.restHeight;
	}
	private void LateUpdate()
	{
		if (!this.showFx)
		{
			return;
		}
		if (this.traction > 0.05f && this.hitPos != Vector3.zero && this.grounded)
		{
			this.smokeEmitting.enabled = true;
			if (Skidmarks.Instance)
			{
				this.lastSkid = Skidmarks.Instance.AddSkidMark(this.hitPos + this.bodyRb.linearVelocity * Time.fixedDeltaTime, this.hitNormal, this.traction * 0.9f, this.lastSkid);
			}
		}
		else
		{
			this.smokeEmitting.enabled = false;
			this.lastSkid = -1;
		}
		if (this.skidSfx)
		{
			float num = 1f;
			if (this.bodyRb.linearVelocity.magnitude < 2f)
			{
				num = 0f;
			}
			this.skidSfx.volume = this.traction * num;
			this.skidSfx.pitch = 0.3f + 0.4f * Mathf.Clamp(this.traction * 0.5f, 0f, 1f);
		}
		if (!this.rearWheel)
		{
			return;
		}
		if (this.traction > 0.15f && this.grounded)
		{
			this.spinEmitting.enabled = true;
			this.spinEmitting.rateOverTime = Mathf.Clamp(this.traction * 60f, 20f, 400f);
			return;
		}
		this.spinEmitting.enabled = false;
	}
	private Car car;
	private Rigidbody bodyRb;
	public Transform wheelObject;
	public bool rearWheel;
	private int lastSkid;
	[HideInInspector]
	public bool skidding;
	[HideInInspector]
	public float grip;
	public bool showFx = true;
	public AudioSource skidSfx;
	public ParticleSystem smokeFx;
	public ParticleSystem spinFx;
	private ParticleSystem.EmissionModule smokeEmitting;
	private ParticleSystem.EmissionModule spinEmitting;
	private float wheelAngleVelocity;
	public float steeringAngle;
	public float traction;
	private float steerTime = 15f;
	public bool spinning;
	public LayerMask whatIsGround;
	private MeshRenderer mesh;
	public Vector3 hitPos;
	public Vector3 hitNormal;
	public float hitHeight;
	public bool grounded;
	public float lastCompression;
	private float raycastOffset;
	private float maxEmission;
	public float restLength;
	public float springTravel;
	public float springStiffness;
	public float damperStiffness;
	private float minLength;
	private float maxLength;
	private float lastLength;
	private float springLength;
	private float springVelocity;
	private float springForce;
	private float damperForce;
}
