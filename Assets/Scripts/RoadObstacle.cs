using System;
using UnityEngine;
public class RoadObstacle : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!this.ready)
		{
			return;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.particles, base.transform.position, this.particles.transform.rotation);
		UnityEngine.Object.Destroy(base.gameObject);
		this.ready = false;
	}
	public GameObject particles;
	private bool ready = true;
}
