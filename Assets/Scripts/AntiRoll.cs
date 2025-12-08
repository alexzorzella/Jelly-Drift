using System;
using UnityEngine;
public class AntiRoll : MonoBehaviour
{
	private void Awake()
	{
		this.bodyRb = base.GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		this.StabilizerBars();
	}
	private void StabilizerBars()
	{
		float num;
		if (this.right.grounded)
		{
			num = this.right.lastCompression;
		}
		else
		{
			num = 1f;
		}
		float num2;
		if (this.left.grounded)
		{
			num2 = this.left.lastCompression;
		}
		else
		{
			num2 = 1f;
		}
		float num3 = (num2 - num) * this.antiRoll;
		if (this.right.grounded)
		{
			this.bodyRb.AddForceAtPosition(this.right.transform.up * -num3, this.right.transform.position);
		}
		if (this.left.grounded)
		{
			this.bodyRb.AddForceAtPosition(this.left.transform.up * num3, this.left.transform.position);
		}
	}
	public Suspension right;
	public Suspension left;
	public float antiRoll = 5000f;
	private Rigidbody bodyRb;
}
