using System;
using UnityEngine;
public class LookAtTarget : MonoBehaviour
{
	private void Start()
	{
		this.cam = base.GetComponent<Camera>();
	}
	private void Update()
	{
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(this.target.position - base.transform.position), Time.deltaTime * 10f);
		this.cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, this.targetFov, Time.deltaTime * 5.5f);
	}
	public Transform target;
	private Camera cam;
	private float targetFov = 15f;
}
