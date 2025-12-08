using System;
using TMPro;
using UnityEngine;
public class Car : MonoBehaviour
{
	public Rigidbody rb { get; set; }
	public float steering { get; set; }
	public float throttle { get; set; }
	public bool breaking { get; set; }
	public float speed { get; private set; }
	public float steerAngle { get; set; }
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		if (this.autoValues)
		{
			this.suspensionLength = 0.3f;
			this.suspensionForce = 10f * this.rb.mass;
			this.suspensionDamping = 4f * this.rb.mass;
		}
		AntiRoll[] componentsInChildren = base.gameObject.GetComponentsInChildren<AntiRoll>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].antiRoll = this.antiRoll;
		}
		if (this.centerOfMass)
		{
			this.rb.centerOfMass = this.centerOfMass.localPosition;
		}
		this.c = base.GetComponentInChildren<Collider>();
		this.wheelBase = Vector3.Distance(this.wheelPositions[0].transform.position, this.wheelPositions[2].transform.position);
		this.CG = this.c.bounds.center;
		this.cgHeight = this.c.bounds.extents.y + this.suspensionLength;
		this.cgToFrontAxle = Vector3.Distance(this.wheelPositions[0].transform.position + (this.wheelPositions[1].transform.position - this.wheelPositions[0].transform.position) * 0.5f, this.CG);
		this.cgToRearAxle = Vector3.Distance(this.wheelPositions[2].transform.position + (this.wheelPositions[3].transform.position - this.wheelPositions[2].transform.position) * 0.5f, this.CG);
		this.wheelRadius = this.suspensionLength / 2f;
		this.InitWheels();
	}
	private void Update()
	{
		this.MoveWheels();
		this.Audio();
		this.CheckGrounded();
		this.Steering();
	}
	private void FixedUpdate()
	{
		this.Movement();
	}
	private void Audio()
	{
		this.accelerate.volume = Mathf.Lerp(this.accelerate.volume, Mathf.Abs(this.throttle) + Mathf.Abs(this.speed / 80f), Time.deltaTime * 6f);
		this.deaccelerate.volume = Mathf.Lerp(this.deaccelerate.volume, this.speed / 40f - this.throttle * 0.5f, Time.deltaTime * 4f);
		this.accelerate.pitch = Mathf.Lerp(this.accelerate.pitch, 0.65f + Mathf.Clamp(Mathf.Abs(this.speed / 160f), 0f, 1f), Time.deltaTime * 5f);
		if (!this.grounded)
		{
			this.accelerate.pitch = Mathf.Lerp(this.accelerate.pitch, 1.5f, Time.deltaTime * 8f);
		}
		this.deaccelerate.pitch = Mathf.Lerp(this.deaccelerate.pitch, 0.5f + this.speed / 40f, Time.deltaTime * 2f);
	}
	public Vector3 acceleration { get; private set; }
	private void Movement()
	{
		Vector3 vector = this.XZVector(this.rb.linearVelocity);
		Vector3 vector2 = base.transform.InverseTransformDirection(this.XZVector(this.rb.linearVelocity));
		this.acceleration = (this.lastVelocity - vector2) / Time.fixedDeltaTime;
		this.dir = Mathf.Sign(base.transform.InverseTransformDirection(vector).z);
		this.speed = vector.magnitude * 3.6f * this.dir;
		float num = Mathf.Abs(this.rb.angularVelocity.y);
		foreach (Suspension suspension in this.wheelPositions)
		{
			if (suspension.grounded)
			{
				Vector3 vector3 = this.XZVector(this.rb.GetPointVelocity(suspension.hitPos));
				base.transform.InverseTransformDirection(vector3);
				Vector3 a = Vector3.Project(vector3, suspension.transform.right);
				float d = 1f;
				float num2 = 1f;
				if (suspension.terrain)
				{
					num2 = 0.6f;
					d = 0.75f;
				}
				float f = Mathf.Atan2(vector2.x, vector2.z);
				if (this.breaking)
				{
					num2 -= 0.6f;
				}
				float num3 = this.driftThreshold;
				if (num > 1f)
				{
					num3 -= 0.2f;
				}
				bool flag = false;
				if (Mathf.Abs(f) > num3)
				{
					float num4 = Mathf.Clamp(Mathf.Abs(f) * 2.4f - num3, 0f, 1f);
					num2 = Mathf.Clamp(1f - num4, 0.05f, 1f);
					float magnitude = this.rb.linearVelocity.magnitude;
					flag = true;
					if (magnitude < 8f)
					{
						num2 += (8f - magnitude) / 8f;
					}
					if (num < this.yawGripThreshold)
					{
						float num5 = (this.yawGripThreshold - num) / this.yawGripThreshold;
						num2 += num5 * this.yawGripMultiplier;
					}
					if (Mathf.Abs(this.throttle) < 0.3f)
					{
						num2 += 0.1f;
					}
					num2 = Mathf.Clamp(num2, 0f, 1f);
				}
				float d2 = 1f;
				if (flag)
				{
					d2 = this.driftMultiplier;
				}
				if (this.breaking)
				{
					this.rb.AddForceAtPosition(suspension.transform.forward * this.C_breaking * Mathf.Sign(-this.speed) * d, suspension.hitPos);
				}
				this.rb.AddForceAtPosition(suspension.transform.forward * this.throttle * this.engineForce * d2 * d, suspension.hitPos);
				Vector3 a2 = a * this.rb.mass * d * num2;
				this.rb.AddForceAtPosition(-a2, suspension.hitPos);
				this.rb.AddForceAtPosition(suspension.transform.forward * a2.magnitude * 0.25f, suspension.hitPos);
				float num6 = Mathf.Clamp(1f - num2, 0f, 1f);
				if (Mathf.Sign(this.dir) != Mathf.Sign(this.throttle) && this.speed > 2f)
				{
					num6 = Mathf.Clamp(num6 + 0.5f, 0f, 1f);
				}
				suspension.traction = num6;
				Vector3 force = -this.C_drag * vector;
				this.rb.AddForce(force);
				Vector3 force2 = -this.C_rollFriction * vector;
				this.rb.AddForce(force2);
			}
		}
		this.StandStill();
		this.lastVelocity = vector2;
	}
	private void StandStill()
	{
		if (Mathf.Abs(this.speed) >= 1f || !this.grounded || this.throttle != 0f)
		{
			this.rb.linearDamping = 0f;
			return;
		}
		bool flag = true;
		Suspension[] array = this.wheelPositions;
		for (int i = 0; i < array.Length; i++)
		{
			if (Vector3.Angle(array[i].hitNormal, Vector3.up) > 1f)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.rb.linearDamping = (1f - Mathf.Abs(this.speed)) * 30f;
			return;
		}
		this.rb.linearDamping = 0f;
	}
	private void Steering()
	{
		foreach (Suspension suspension in this.wheelPositions)
		{
			if (!suspension.rearWheel)
			{
				suspension.steeringAngle = this.steering * (37f - Mathf.Clamp(this.speed * 0.35f - 2f, 0f, 17f));
				this.steerAngle = suspension.steeringAngle;
			}
		}
	}
	private Vector3 XZVector(Vector3 v)
	{
		return new Vector3(v.x, 0f, v.z);
	}
	private void InitWheels()
	{
		foreach (Suspension suspension in this.wheelPositions)
		{
			suspension.wheelObject = UnityEngine.Object.Instantiate<GameObject>(this.wheel).transform;
			suspension.wheelObject.parent = suspension.transform;
			suspension.wheelObject.transform.localPosition = Vector3.zero;
			suspension.wheelObject.transform.localRotation = Quaternion.identity;
			suspension.wheelObject.localScale = Vector3.one * this.suspensionLength * 2f;
		}
	}
	private void MoveWheels()
	{
		foreach (Suspension suspension in this.wheelPositions)
		{
			float num = this.suspensionLength;
			float hitHeight = suspension.hitHeight;
			float y = Mathf.Lerp(suspension.wheelObject.transform.localPosition.y, -hitHeight + num, Time.deltaTime * 20f);
			float num2 = 0.2f * this.suspensionLength * 2f;
			if (suspension.transform.localPosition.x < 0f)
			{
				num2 = -num2;
			}
			num2 = 0f;
			suspension.wheelObject.transform.localPosition = new Vector3(num2, y, 0f);
			suspension.wheelObject.Rotate(Vector3.right, this.XZVector(this.rb.linearVelocity).magnitude * 1f * this.dir);
			suspension.wheelObject.localScale = Vector3.one * (this.suspensionLength * 2f);
			suspension.transform.localScale = Vector3.one / base.transform.localScale.x;
		}
	}
	private void CheckGrounded()
	{
		this.grounded = false;
		Suspension[] array = this.wheelPositions;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].grounded)
			{
				this.grounded = true;
			}
		}
	}
	[Header("Misc")]
	public Transform centerOfMass;
	public Suspension[] wheelPositions;
	public GameObject wheel;
	public TextMeshProUGUI text;
	private Collider c;
	[Header("Suspension Variables")]
	public bool autoValues;
	public float suspensionLength;
	public float restHeight;
	public float suspensionForce;
	public float suspensionDamping;
	[Header("Car specs")]
	public float engineForce = 5000f;
	public float steerForce = 1f;
	public float antiRoll = 5000f;
	public float stability;
	[Header("Drift specs")]
	public float driftMultiplier = 1f;
	public float driftThreshold = 0.5f;
	private float C_drag = 3.5f;
	private float C_rollFriction = 105f;
	private float C_breaking = 3000f;
	[Header("Audio Sources")]
	public AudioSource accelerate;
	public AudioSource deaccelerate;
	private float dir;
	private Vector3 lastVelocity;
	private bool grounded;
	private Vector3 CG;
	private float cgHeight;
	private float wheelBase;
	private float axleWeightRatioFront = 0.5f;
	private float axleWeightRatioRear = 0.5f;
	private float wheelRadius;
	private float yawRate;
	private float weightTransfer = 0.2f;
	private float cgToRearAxle;
	private float cgToFrontAxle;
	private float tireGrip = 2f;
	private float lockGrip = 0.7f;
	private float cornerStiffnessFront = 5f;
	private float cornerStiffnessRear = 5.2f;
	private float yawGripThreshold = 0.6f;
	private float yawGripMultiplier = 0.15f;
	public bool yes;
}
