using System;
using UnityEngine;
public class MobileControls : MonoBehaviour
{
	private void Start()
	{
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			this.car = GameController.Instance.currentCar.GetComponent<Car>();
			UnityEngine.Object.Destroy(this.car.GetComponent<PlayerInput>());
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
	private void Update()
	{
		if (!GameController.Instance.playing)
		{
			return;
		}
		float steering = 0f;
		float num = 0f;
		if (this.left.value > 0)
		{
			steering = -1f;
		}
		if (this.right.value > 0)
		{
			steering = 1f;
		}
		if (this.throttle.value > 0)
		{
			num = 1f;
		}
		if (this.breakPedal.value > 0)
		{
			num = -1f;
		}
		this.car.steering = steering;
		this.car.throttle = num;
	}
	public MyButton left;
	public MyButton right;
	public MyButton throttle;
	public MyButton breakPedal;
	private Car car;
}
