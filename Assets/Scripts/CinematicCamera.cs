using System;
using UnityEngine;
public class CinematicCamera : MonoBehaviour
{
	private void Update()
	{
		base.transform.RotateAround(this.target.position, Vector3.up, this.rotationSpeed * Time.deltaTime);
		base.transform.LookAt(this.target.position + this.offset);
	}
	public float rotationSpeed;
	public Transform target;
	public Vector3 offset;
}
