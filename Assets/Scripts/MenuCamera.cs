using System;
using UnityEngine;
public class MenuCamera : MonoBehaviour
{
	private void Update()
	{
		if (!CarDisplay.Instance || !CarDisplay.Instance.currentCar)
		{
			return;
		}
		base.transform.RotateAround(this.carDisplay.currentCar.transform.position, Vector3.up, this.rotationSpeed * Time.deltaTime);
		base.transform.LookAt(this.carDisplay.currentCar.transform.position + this.offset);
	}
	public float rotationSpeed;
	public Transform target;
	public Vector3 offset;
	public CarDisplay carDisplay;
}
