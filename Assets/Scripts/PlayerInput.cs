using System;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
	private void GetPlayerInput()
	{
		this.car.steering = Input.GetAxisRaw("Horizontal");
		this.car.throttle = Input.GetAxis("Vertical");
		this.car.breaking = Input.GetButton("Breaking");
	}
	private void Update()
	{
		if (GameController.Instance && !GameController.Instance.playing)
		{
			return;
		}
		this.GetPlayerInput();
	}
	public Car car;
}
