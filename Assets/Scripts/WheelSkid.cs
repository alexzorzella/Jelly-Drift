using System;
using UnityEngine;
[RequireComponent(typeof(WheelCollider))]
public class WheelSkid : MonoBehaviour
{
	protected void Awake()
	{
		this.wheelCollider = base.GetComponent<WheelCollider>();
		this.lastFixedUpdateTime = Time.time;
	}
	protected void FixedUpdate()
	{
		this.lastFixedUpdateTime = Time.time;
	}
	protected void LateUpdate()
	{
		if (!this.wheelCollider.GetGroundHit(out this.wheelHitInfo))
		{
			this.lastSkid = -1;
			return;
		}
		float num = Mathf.Abs(base.transform.InverseTransformDirection(this.rb.linearVelocity).x);
		float num2 = this.wheelCollider.radius * (6.2831855f * this.wheelCollider.rpm / 60f);
		float num3 = Vector3.Dot(this.rb.linearVelocity, base.transform.forward);
		float num4 = Mathf.Abs(num3 - num2) * 10f;
		num4 = Mathf.Max(0f, num4 * (10f - Mathf.Abs(num3)));
		num += num4;
		if (num >= 0.5f)
		{
			float opacity = Mathf.Clamp01(num / 20f);
			Vector3 pos = this.wheelHitInfo.point + this.rb.linearVelocity * (Time.time - this.lastFixedUpdateTime);
			this.lastSkid = this.skidmarksController.AddSkidMark(pos, this.wheelHitInfo.normal, opacity, this.lastSkid);
			return;
		}
		this.lastSkid = -1;
	}
	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private Skidmarks skidmarksController;
	private WheelCollider wheelCollider;
	private WheelHit wheelHitInfo;
	private const float SKID_FX_SPEED = 0.5f;
	private const float MAX_SKID_INTENSITY = 20f;
	private const float WHEEL_SLIP_MULTIPLIER = 10f;
	private int lastSkid = -1;
	private float lastFixedUpdateTime;
}
