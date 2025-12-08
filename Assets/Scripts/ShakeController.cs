using System;
using MilkShake;
using UnityEngine;
public class ShakeController : MonoBehaviour
{
	private void Awake()
	{
		ShakeController.Instance = this;
	}
	private void Start()
	{
		this.shaker = CameraController.Instance.transform.GetComponentInChildren<Shaker>();
		this.shakeInstance = this.shaker.Shake(this.preset, null);
		this.shakeInstance.StrengthScale = 0f;
	}
	public void Shake()
	{
		this.shaker.Shake(this.crashShake, null);
	}
	private void FixedUpdate()
	{
		if (!this.car)
		{
			return;
		}
		float magnitude = this.car.acceleration.magnitude;
		float num = 0f;
		foreach (Suspension suspension in this.car.wheelPositions)
		{
			if (suspension.traction > num)
			{
				num = suspension.traction;
			}
		}
		if (this.car.speed < 2f)
		{
			num = 0f;
		}
		this.shakeInstance.StrengthScale = Mathf.Clamp(num * 0.5f, 0f, 1f);
	}
	public Car car;
	private Shaker shaker;
	public ShakePreset preset;
	public ShakePreset crashShake;
	private ShakeInstance shakeInstance;
	public static ShakeController Instance;
}
