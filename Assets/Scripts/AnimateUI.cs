using System;
using UnityEngine;
public class AnimateUI : MonoBehaviour
{
	private void Awake()
	{
		this.defaultScale = base.transform.localScale;
		this.defaultRot = 0f;
		this.desiredScale = this.defaultScale;
	}
	private void Update()
	{
		float d = 1f + (Mathf.PingPong(Time.time * this.scaleSpeed, this.scaleStrength) - this.scaleStrength / 2f);
		float target = Mathf.PingPong(Time.time * this.rotSpeed, this.rotStrength) - this.rotStrength / 2f;
		this.desiredScale = this.defaultScale * d;
		base.transform.localScale = Vector3.SmoothDamp(base.transform.localScale, this.desiredScale, ref this.scaleVel, this.scaleSmooth);
		this.rot = Mathf.SmoothDamp(this.rot, target, ref this.rotVel, this.rotSmooth);
		base.transform.localRotation = Quaternion.Euler(0f, 0f, this.rot);
	}
	private Vector3 defaultScale;
	private float defaultRot;
	private float rotVel;
	public float rotSpeed;
	public float rotStrength;
	public float rotSmooth;
	private Vector3 desiredScale;
	private Vector3 scaleVel;
	public float scaleSpeed;
	public float scaleStrength;
	public float scaleSmooth;
	private float rot;
}
