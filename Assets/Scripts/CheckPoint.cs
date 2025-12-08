using System;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
	public int nr { get; set; }
	private void Awake()
	{
		this.nr = base.transform.GetSiblingIndex();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
		{
			CheckpointUser component = other.gameObject.transform.root.GetComponent<CheckpointUser>();
			if (component)
			{
				component.CheckPoint(this);
			}
		}
	}
	private bool done;
	public GameObject clearFx;
}
